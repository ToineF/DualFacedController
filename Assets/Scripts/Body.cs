using UnityEngine;

public class Body : MonoBehaviour
{
    [field:Header("References")]
    [field: SerializeField] public Rigidbody[] BodyParts { get; set; }
    [field: SerializeField] public Head Head { get; set; }
    [field: SerializeField] public Head Tail { get; set; }
    [field: SerializeField] public LineRenderer[] Lines { get; set; }

    [Header("Parameters")]
    [SerializeField] private int _forceIterations = 1;
    [SerializeField] private float _followSpeed;
    //[SerializeField] private float _startBodySpeed;
    //[SerializeField] private float _endBodySpeed;
    [SerializeField, Range(0, 1)] private float _followLerp;
    [SerializeField] private ForceMode _forceMode;
    [SerializeField] private bool _useY = true;
    [SerializeField] private float _additionalGravity;

    [Header("Ground Detection")]
    [SerializeField] private float _groundDetectionDistance;
    [SerializeField] private LayerMask _groundLayer;

    private RaycastHit[] _groundHits;

    private void Start()
    {
        _groundHits = new RaycastHit[2];
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _forceIterations; i++)
        {
            MoveHead();
            MoveTail();
        }
        UpdateLines();
    }

    private void MoveHead()
    {
        for (int i = 0; i < BodyParts.Length + 1; i++)
        {
            var lastBodyPart = (i == 0) ? Head.Rigidbody : BodyParts[i - 1];
            var bodyPart = (i == BodyParts.Length) ? Tail.Rigidbody : BodyParts[i];
            
            MoveBodyPart(Head, lastBodyPart, bodyPart, i / (BodyParts.Length + 1));
        }
    }

    private void MoveTail()
    {
        for (int i = BodyParts.Length; i >= 0; i--)
        {
            var lastBodyPart = (i == BodyParts.Length) ? Tail.Rigidbody : BodyParts[i];
            var bodyPart = (i == 0) ? Head.Rigidbody : BodyParts[i-1];

            MoveBodyPart(Tail, lastBodyPart, bodyPart, 1 - (i / (BodyParts.Length + 1)));
        }
    }

    /*private void dd(Head extremity, Rigidbody lastBodyPart, Rigidbody bodyPart, float damper)
    {
        var targetDirection = (lastBodyPart.position - bodyPart.position).normalized;
        targetDirection = Vector3.Lerp(extremity.Direction, targetDirection, _followLerp);
        var speed = Mathf.Lerp(_startBodySpeed, _endBodySpeed, damper);
        var force = new Vector3(targetDirection.x, 0, targetDirection.y) * speed;
        bodyPart.AddForce(force, _forceMode);
    }*/

    private void MoveBodyPart(Head extremity, Rigidbody lastBodyPart, Rigidbody bodyPart, float damper)
    {
        var offset = lastBodyPart.position - bodyPart.position;
        var targetDirection = offset.normalized;

        //if (offset.sqrMagnitude < _targetDistance) break;
        var force = offset.sqrMagnitude * _followSpeed * new Vector3(targetDirection.x, _useY ? targetDirection.y : 0, targetDirection.z);
        bodyPart.AddForce(force, _forceMode);
        //bodyPart.AddForce(Mathf.Sin(i + T) * Vector3.Cross(force.normalized * amplitude, Vector3.up), _forceMode);
        if (IsGrounded(bodyPart.transform.position) == false) bodyPart.AddForce(Vector3.down * _additionalGravity, _forceMode);
    }

    private bool IsGrounded(Vector3 position)
    {
        return Physics.RaycastNonAlloc(position, Vector3.down, _groundHits, _groundDetectionDistance, _groundLayer) > 0;
    }

    private void UpdateLines()
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            Lines[i].SetPosition(0, (i == 0) ? Head.transform.position : BodyParts[i-1].position);
            Lines[i].SetPosition(1, (i == Lines.Length - 1) ? Tail.transform.position : BodyParts[i].position);
        }
    }
}