using System;
using System.Threading.Tasks;
using Cattac.Interactables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cattac.Character
{
    public class CharacterHead : MonoBehaviour
    {
        public Action OnSeparate { get; set; }
        public Action OnConnect { get; set; }

        public Vector2 Direction => _direction;
        public Vector2 LastDirection => _lastDirection;

        public IGrabbable CurrentGrabbable
        {
            get => _currentGrabbable;
            set => _currentGrabbable = value;
        }

        public bool IsGrabbing { get; private set; }
        public bool IsSeparated { get; private set; }
        
        public Rigidbody CurrentRigidbody
        {
            get
            {
                var newRigidbody = IsSeparated ? SeparatedRigidbody : TogetherRigidbody;
                transform.SetParent(newRigidbody.transform);
                if (IsSeparated == false) SeparatedRigidbody.position = TogetherRigidbody.position;
                transform.localPosition = Vector3.zero;
                return newRigidbody;
            }
        }

        [field: Header("Head Properties")]
        [field: SerializeField] public Rigidbody TogetherRigidbody { get; set; }
        [field: SerializeField] public Rigidbody SeparatedRigidbody { get; set; }
        [field: SerializeField] public CapsuleCollider Collider { get; set; }
        [SerializeField] private CharacterHeadData _data;

        [Header("Input Properties")] [SerializeField]
        private bool _isLeftHead;

        [Header("Ability")]
        [SerializeField] private Ability.Ability _ability;

        [Header("Feedbacks")]
        [SerializeField] private AudioSource _squeakNoise;

        private float _snakeTimer;

        private Vector2 _direction;
        private Vector2 _lastDirection;
        private RaycastHit _lastGroundHit;

        private IGrabbable _currentGrabbable;

        private float _jumpTimer;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
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
            _direction = Vector3.Lerp(_direction, targetDirection, _data.TurnLerp);
            if (_direction.magnitude > 0.1f) _lastDirection = _direction;

        }

        private void CheckGrab()
        {
            IsGrabbing = _isLeftHead ? UserInput.Instance.LeftGrabInput : UserInput.Instance.RightGrabInput;
            //Rigidbody.isKinematic = IsGrabbing;

            var isJumping = _isLeftHead
                ? UserInput.Instance.LeftGrabInputReleased
                : UserInput.Instance.RightGrabInputReleased;

            //if (isJumping) Rigidbody.AddForce(Vector3.up * _heightForce, ForceMode);
            // Update timer
            _jumpTimer -= Time.deltaTime;
            if (_jumpTimer <= 0 && isJumping) ApplyForceWithDecay(CurrentRigidbody);

            if (isJumping)
            {
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
            var colliders = Physics.OverlapSphere(transform.position, _data.GrabRadius, _data.GrabLayerMask);
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

            float currentForce = _data.JumpForce;
            _jumpTimer = _data.MinJumpsInterval;

            // Apply force over several frames with decay
            for (int i = 0; i < _data.JumpTime; i++)
            {
                if (currentForce <= 0) break;

                rb.AddForce(Vector3.up * currentForce, ForceMode.Impulse);
                currentForce -= _data.JumpDecrease;

                // Wait until the next frame
                await Task.Delay(1);
            }
        }

        private void CheckSeparation()
        {
            if ((_isLeftHead ? UserInput.Instance.LeftSeparationInput : UserInput.Instance.RightSeparationInput) ==
                false) return;
            ToggleSeparation();
        }

        private void CheckAbility()
        {
            if (_ability == null) return;
            if ((_isLeftHead ? UserInput.Instance.LeftSeparationInput : UserInput.Instance.RightSeparationInput) ==
                false) return;

            _ability.UseAbility(this);
        }

        private void ToggleSeparation()
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
        }

        private void FixedUpdate()
        {
            MoveSelf();
            MoveSnake();
        }

        private void MoveSelf()
        {
            var force = new Vector3(_direction.x, 0, _direction.y) * _data.Speed;
            force = _camera.transform.forward * force.z + _camera.transform.right * force.x;
            force = Vector3.ProjectOnPlane(force, _lastGroundHit.normal);
            CurrentRigidbody.AddForce(force, ForceMode.Impulse);
            ApplyGrabbableForce(force);
            ApplyGravity();
        }

        private void ApplyGrabbableForce(Vector3 force)
        {
            _currentGrabbable?.AddForce(force);
        }

        private void ApplyGravity()
        {
            //if (Physics.Raycast(transform.position, Vector3.down, out hit, _groundDetectionDistance, _groundLayer))
            //AntoineFoucault.Utilities.ColliderExtensions.GetCapsulePoints(Collider, out Vector3 p1, out Vector3 p2);
            if (Physics.SphereCast(transform.position, Collider.radius, Vector3.down, out _lastGroundHit,
                    _data.GroundDetectionDistance, _data.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundHeight = _lastGroundHit.point.y;
                float currentHeight = transform.position.y;

                // Calculate the difference from the target height
                float distanceToTarget = (groundHeight + _data.RestPositionFromGround) - currentHeight;

                // Apply spring force to float the character
                Vector3 force = distanceToTarget * _data.AdditionalGravity * Vector3.up;

                // Apply damping force to gradually reduce the force
                Vector3 velocity = Vector3.up * CurrentRigidbody.velocity.y;
                force -= velocity * _data.GravityDamper;

                // Apply the force to the Rigidbody
                CurrentRigidbody.AddForce(force, ForceMode.Impulse);
            }
        }

        private void MoveSnake()
        {
            _snakeTimer += Time.deltaTime;
            var s = Mathf.Sin((_snakeTimer + _data.SnakeOffset) / _data.SnakeFrequency) * _data.SnakeAmplitude;
            var directionVector = Vector3.Cross(new Vector3(_direction.x, 0, _direction.y), Vector3.down);
            CurrentRigidbody.AddForce(directionVector * s, ForceMode.Impulse);
        }

/*#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, _grabRadius);
    }
#endif*/
    }
}