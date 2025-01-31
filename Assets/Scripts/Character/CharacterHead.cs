using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CharacterHead : MonoBehaviour
{
    public Action OnSeparate {get; set;}
    public Action OnConnect { get; set; }

    public Vector2 Direction => _direction;
    public Vector2 LastDirection => _lastDirection;
    public IGrabbable CurrentGrabbable { get => _currentGrabbable; set => _currentGrabbable = value; }
    public bool IsGrabbing { get; private set; }
    public bool IsSeparated { get; private set; }

    [field: Header("Head Properties")]
    [field: SerializeField] public Rigidbody Rigidbody { get; set; }
    [field: SerializeField] public CapsuleCollider Collider { get; set; }

    [SerializeField] private float _speed;
    [SerializeField, Range(0, 1)] private float _turnLerp;

    [Header("Ground Detection")]
    [SerializeField] private float _additionalGravity;
    [SerializeField] private float _gravityDamper;
    [SerializeField] private float _restPositionFromGround;

    [SerializeField] private float _groundDetectionDistance;
    [SerializeField] private LayerMask _groundLayer;


    public ForceMode ForceMode;

    [FormerlySerializedAs("_body")] [SerializeField] private CharacterBody characterBody;

    [Header("Grab")]
    [SerializeField] private float _grabRadius;
    [SerializeField] private LayerMask _grabLayerMask;
    [SerializeField] private AudioSource _squeakNoise;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _jumpDecrease;
    [SerializeField] private float _minJumpsInterval;

    [Header("Input Properties")]
    [SerializeField] private bool _isLeftHead;

    [Header("Ability")]
    [SerializeField] private Ability _ability;
    
    [Header("Snake")]
    [SerializeField] private float _snakeOffset;
    [SerializeField] private float _snakeFrequency;
    [SerializeField] private float _snakeAmplitude;
    private float _snakeTimer;

    private Vector2 _direction;
    private Vector2 _lastDirection;
    private RaycastHit[] _groundHits;

    private IGrabbable _currentGrabbable;

    private float _jumpTimer;

    private void Start()
    {
        _groundHits = new RaycastHit[2];
    }

    private void Update()
    {
        CheckMovements();
        CheckGrab();
        CheckAbility();
    }
    
    private void CheckMovements()
    {
        var targetDirection = _isLeftHead ? UserInput.Instance.LeftMoveInput : UserInput.Instance.RightMoveInput;
        _direction = Vector3.Lerp(_direction, targetDirection, _turnLerp);
        if (_direction.magnitude > 0.1f) _lastDirection = _direction;

    }
    private void CheckGrab()
    {
        IsGrabbing = _isLeftHead ? UserInput.Instance.LeftGrabInput : UserInput.Instance.RightGrabInput;
        //Rigidbody.isKinematic = IsGrabbing;
        
        var isJumping = _isLeftHead ? UserInput.Instance.LeftGrabInputReleased : UserInput.Instance.RightGrabInputReleased;

        //if (isJumping) Rigidbody.AddForce(Vector3.up * _heightForce, ForceMode);
        // Update timer
        _jumpTimer -= Time.deltaTime;
        if (_jumpTimer <= 0 && isJumping) ApplyForceWithDecay(Rigidbody);

        if (isJumping) {
            if (_currentGrabbable == null) 
                Grab();
            else 
            {
                _currentGrabbable.OnUngrab(this);
                _currentGrabbable = null;
            }
        }
    }

    private void Grab()
    {
        var colliders = Physics.OverlapSphere(transform.position, _grabRadius, _grabLayerMask);
        if (colliders.Length < 1) return;
        var minDistance = (transform.position - colliders[0].transform.position).sqrMagnitude;
        var closestCollider = colliders[0];
        if (colliders.Length > 1)
        {
            for (int i = 1; i < colliders.Length; i++)
            {
                var currentDistance = (transform.position - colliders[i].transform.position).sqrMagnitude;
                if (currentDistance < minDistance)
                {
                    closestCollider = colliders[i];
                    minDistance = currentDistance;
                }
            }
        }

        if (closestCollider.TryGetComponent(out IGrabbable grabbable))
        {
            _currentGrabbable = grabbable;
            _currentGrabbable.OnGrab(this);
        }
    }

    private async void ApplyForceWithDecay(Rigidbody rb)
    {
        _squeakNoise.volume = Random.Range(0.7f, 1f);
        _squeakNoise.pitch = Random.Range(0.9f, 1.1f);
        _squeakNoise.Play();

        float currentForce = _jumpForce;
        _jumpTimer = _minJumpsInterval;

        // Apply force over several frames with decay
        for (int i = 0; i < _jumpTime; i++)
        {
            if (currentForce <= 0) break;

            rb.AddForce(Vector3.up * currentForce, ForceMode);
            currentForce -= _jumpDecrease;

            // Wait until the next frame
            await Task.Delay(1);
        }
    }

    private void CheckSeparation()
    {
        if ((_isLeftHead ? UserInput.Instance.LeftSeparationInput : UserInput.Instance.RightSeparationInput) == false) return;
        ToggleSeparation();
    }

    private void CheckAbility()
    {
        if (_ability == null) return;
        if ((_isLeftHead ? UserInput.Instance.LeftSeparationInput : UserInput.Instance.RightSeparationInput) == false) return;
        
        _ability.UseAbility(this);
    }

    public void ToggleSeparation()
    {
        IsSeparated = !IsSeparated;
        OnSeparation();
    }
    
    public void SetSeparation(bool isSeparated)
    {
        IsSeparated = isSeparated;
        OnSeparation();
    }

    private void OnSeparation()
    {
        if (IsSeparated) OnSeparate?.Invoke();
        else OnConnect?.Invoke();
        
        characterBody.ToggleJointSeparation(IsSeparated, _isLeftHead);
    }
    private void FixedUpdate()
    {
        MoveSelf();
        //MoveBodyParts();
        //ApproachBodyParts();
        MoveSnake();
    }

    private void MoveSelf()
    {
        var force = new Vector3(_direction.x, 0, _direction.y) * _speed;
        Rigidbody.AddForce(force, ForceMode);
        ApplyGrabbableForce(force);
        ApplyGravity();
    }

    private void ApplyGrabbableForce(Vector3 force)
    {
        _currentGrabbable?.AddForce(force);
    }

    private void ApplyGravity()
    {
        RaycastHit hit;
        //if (Physics.Raycast(transform.position, Vector3.down, out hit, _groundDetectionDistance, _groundLayer))
        //AntoineFoucault.Utilities.ColliderExtensions.GetCapsulePoints(Collider, out Vector3 p1, out Vector3 p2);
        if (Physics.SphereCast(transform.position, Collider.radius, Vector3.down, out hit, _groundDetectionDistance, _groundLayer, QueryTriggerInteraction.Ignore))
        {
            float groundHeight = hit.point.y;
            float currentHeight = transform.position.y;

            // Calculate the difference from the target height
            float distanceToTarget = (groundHeight + _restPositionFromGround) - currentHeight;

            // Apply spring force to float the character
            Vector3 force = distanceToTarget * _additionalGravity * Vector3.up;

            // Apply damping force to gradually reduce the force
            Vector3 velocity = Vector3.up * Rigidbody.velocity.y;
            force -= velocity * _gravityDamper;

            // Apply the force to the Rigidbody
            Rigidbody.AddForce(force, ForceMode);
        }
    }
    private void MoveSnake()
    {
        _snakeTimer += Time.deltaTime;
        var s = Mathf.Sin((_snakeTimer + _snakeOffset) / _snakeFrequency) * _snakeAmplitude;
        var directionVector = Vector3.Cross(new Vector3(_direction.x, 0, _direction.y), Vector3.down);
        Rigidbody.AddForce(directionVector * s, ForceMode);
    }

    private bool IsGrounded(Vector3 position)
    {
        return Physics.RaycastNonAlloc(position, Vector3.down, _groundHits, _groundDetectionDistance,
            _groundLayer) > 0;
    }

/*#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, _grabRadius);
    }
#endif*/
}