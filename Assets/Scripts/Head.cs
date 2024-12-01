using UnityEngine;

public class Head : MonoBehaviour
{
    public Vector2 Direction => _direction;
    public bool IsGrabbing { get; private set; }
    public bool IsSeparated { get; private set; }

    [field: Header("Head Properties")]
    [field: SerializeField]
    public Rigidbody Rigidbody { get; set; }

    [field: SerializeField] public Rigidbody[] BodyParts { get; set; }
    [SerializeField] private float _speed;
    [SerializeField, Range(0, 1)] private float _turnLerp;

    [Header("Ground Detection")] [SerializeField]
    private float _additionalGravity;

    [SerializeField] private float _groundDetectionDistance;
    [SerializeField] private LayerMask _groundLayer;

    public ForceMode ForceMode;
    //public float forceTobODYPARTS = 1f;
    //public int forceiterations = 1;

    [SerializeField] private Body _body;

    [Header("Input Properties")]
    [SerializeField] private bool _isLeftHead;

    private Vector2 _direction;
    private RaycastHit[] _groundHits;

    private void Start()
    {
        _groundHits = new RaycastHit[2];
    }

    private void Update()
    {
        CheckMovements();
        CheckGrab();
        CheckSeparation();
    }
    
    private void CheckMovements()
    {
        var targetDirection = _isLeftHead ? UserInput.Instance.LeftMoveInput : UserInput.Instance.RightMoveInput;
        _direction = Vector3.Lerp(_direction, targetDirection, _turnLerp);
    }
    private void CheckGrab()
    {
        IsGrabbing = _isLeftHead ? UserInput.Instance.LeftGrabInput : UserInput.Instance.RightGrabInput;

        Rigidbody.isKinematic = IsGrabbing;
    }

    private void CheckSeparation()
    {
        if ((_isLeftHead ? UserInput.Instance.LeftSeparationInput : UserInput.Instance.RightSeparationInput) == false) return;

        IsSeparated = !IsSeparated;

        if (_isLeftHead)
            _body.Head = IsSeparated ? null : this;
        else
            _body.Tail = IsSeparated ? null : this;
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
        Rigidbody.AddForce(new Vector3(_direction.x, 0, _direction.y) * _speed, ForceMode);
        if (IsGrounded(transform.position) == false) Rigidbody.AddForce(Vector3.down * _additionalGravity, ForceMode);
    }

    [Header("Snake")]
    public float offset;
    public float period;
    public AnimationCurve forceCurve;
    public float amplitude;
    public float segmentOffset;
    private float T;

    private void MoveSnake()
    {
        T += Time.deltaTime;
        /*for (int i = 0; i < BodyParts.Length; i++)
        {
            var s = Mathf.Sin((T + offset) / period) * amplitude;
            var forceMultiplier = forceCurve.Evaluate((float)i / BodyParts.Length);
            BodyParts[i].AddForce(transform.right * (s * forceMultiplier), ForceMode.VelocityChange);
            offset += segmentOffset;
        }    */
    }


    private bool IsGrounded(Vector3 position)
    {
        return Physics.RaycastNonAlloc(position, Vector3.down, _groundHits, _groundDetectionDistance,
            _groundLayer) > 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * _groundDetectionDistance);
    }
}