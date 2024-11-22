using UnityEngine;

public class Head : MonoBehaviour
{
    public Vector2 Direction => _direction;

    [field: Header("Head Properties")]
    [field: SerializeField]
    public Rigidbody Rigidbody { get; set; }

    [field: SerializeField] public Rigidbody[] BodyParts { get; set; }
    [SerializeField] private float _speed;
    [SerializeField] private float _endBodySpeed;
    [SerializeField, Range(0, 1)] private float _turnLerp;
    [SerializeField, Range(0, 1)] private float _followLerp;
    [SerializeField] private float _targetDistance;
    [SerializeField] private float forceIterations;

    [Header("Ground Detection")] [SerializeField]
    private float _additionalGravity;

    [SerializeField] private float _groundDetectionDistance;
    [SerializeField] private LayerMask _groundLayer;

    public ForceMode ForceMode;
    //public float forceTobODYPARTS = 1f;
    //public int forceiterations = 1;

    [Header("Input Properties")] [SerializeField]
    private KeyCode _rightKey;

    [SerializeField] private KeyCode _leftKey;
    [SerializeField] private KeyCode _upKey;
    [SerializeField] private KeyCode _downKey;

    private Vector2 _direction;
    private RaycastHit[] _groundHits;

    private void Start()
    {
        _groundHits = new RaycastHit[2];
    }

    private void Update()
    {
        var targetDirection = Vector3.zero;
        if (Input.GetKey(_rightKey)) targetDirection.x++;
        if (Input.GetKey(_leftKey)) targetDirection.x--;
        if (Input.GetKey(_upKey)) targetDirection.y++;
        if (Input.GetKey(_downKey)) targetDirection.y--;
        targetDirection.Normalize();
        _direction = Vector3.Lerp(_direction, targetDirection, _turnLerp);
    }

    private void FixedUpdate()
    {
        MoveSelf();
        //MoveBodyParts();
        ApproachBodyParts();
        MoveSnake();
        UpdateLines();
    }

    private void MoveSelf()
    {
        Rigidbody.AddForce(new Vector3(_direction.x, 0, _direction.y) * _speed, ForceMode);
        if (IsGrounded(transform.position) == false) Rigidbody.AddForce(Vector3.down * _additionalGravity, ForceMode);
    }

    private void MoveBodyParts()
    {
        for (int i = 0; i < BodyParts.Length; i++)
        {
            if (i == 0) continue;
            var targetDirection = (BodyParts[i - 1].position - BodyParts[i].position).normalized;
            //Vector3.Lerp(BodyParts[i].position, BodyParts[i - 1].position + distanceOffset, _followLerp);
            targetDirection = Vector3.Lerp(_direction, targetDirection, _followLerp);
            var speed = Mathf.Lerp(_speed, _endBodySpeed, i / (BodyParts.Length - 1));
            var force = new Vector3(targetDirection.x, 0, targetDirection.y) * speed;
            BodyParts[i].AddForce(force, ForceMode);
            //BodyParts[i].AddForce(distanceOffset * forceToBodyParts);
        }
    }

    private void ApproachBodyParts()
    {
        //for (int k = 0; k < forceIterations; k++)
        //{
            for (int i = 0; i < BodyParts.Length; i++)
            {
                if (i == 0) continue;
                var offset = (BodyParts[i - 1].position - BodyParts[i].position);
                var targetDirection = offset.normalized;
                //Vector3.Lerp(BodyParts[i].position, BodyParts[i - 1].position + distanceOffset, _followLerp);
                //targetDirection = Vector3.Lerp(_direction, targetDirection, _followLerp);
                //var speed = Mathf.Lerp(_speed, _endBodySpeed, i / (BodyParts.Length - 1));
                for (int k = 0; k < forceIterations; k++)
                {
                    if (offset.sqrMagnitude < _targetDistance) break;
                    var force = offset.sqrMagnitude * new Vector3(targetDirection.x, 0, targetDirection.z);
                    BodyParts[i].AddForce(force, ForceMode);
                    BodyParts[i].AddForce(Mathf.Sin(i + T) * Vector3.Cross(force.normalized * amplitude, Vector3.up) , ForceMode);
                }
                if (IsGrounded(BodyParts[i].transform.position) == false) BodyParts[i].AddForce(Vector3.down * _additionalGravity, ForceMode);
                //BodyParts[i].AddForce(distanceOffset * forceToBodyParts);
            }
        //}
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
    
    [Header("Lines")]
    public LineRenderer[] Lines;
    
    private void UpdateLines()
    {
        for (int i = 0; i < BodyParts.Length-1; i++)
        {
            Lines[i].SetPosition(0, BodyParts[i].position);
            Lines[i].SetPosition(1, BodyParts[i+1].position);
        }
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