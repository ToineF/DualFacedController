using System;
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
    [SerializeField] private float _followStrength;
    [SerializeField] private float _damper;
    [SerializeField] private float _restDistance;
    [SerializeField, Range(0, 1)] private float _followLerp;
    [SerializeField] private ForceMode _forceMode;
    [SerializeField] private bool _useY = true;
    [SerializeField] private float _maxMagnitude;

    [Header("Ground Detection")]
    [SerializeField] private float _additionalGravity;
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
            var lastBodyPart = (i == 0) ? Head?.Rigidbody : BodyParts[i - 1];
            var bodyPart = (i == BodyParts.Length) ? Tail?.Rigidbody : BodyParts[i];
            if (lastBodyPart == null || bodyPart == null) continue;
            
            MoveBodyPart(Head, lastBodyPart, bodyPart, i / (BodyParts.Length + 1));
        }
    }

    private void MoveTail()
    {
        for (int i = BodyParts.Length; i >= 0; i--)
        {
            var lastBodyPart = (i == BodyParts.Length) ? Tail?.Rigidbody : BodyParts[i];
            var bodyPart = (i == 0) ? Head?.Rigidbody : BodyParts[i-1];
            if (lastBodyPart == null || bodyPart == null) continue;

            MoveBodyPart(Tail, lastBodyPart, bodyPart, 1 - i / (BodyParts.Length + 1));
        }
    }

    private void MoveBodyPart(Head extremity, Rigidbody lastBodyPart, Rigidbody bodyPart, float damperOverBody)
    {
        var offset = lastBodyPart.position - bodyPart.position;
        var targetDirection = offset.normalized;

        var offset2 = (_restDistance - offset.sqrMagnitude) * targetDirection;
        var force = -offset2 * _followStrength;
        var damper = bodyPart.velocity * _damper;
        var totalForce = force - damper;
        bodyPart.AddForce(Vector3.ClampMagnitude(totalForce, _maxMagnitude), _forceMode);
        //bodyPart.AddForce(Mathf.Sin(i + T) * Vector3.Cross(force.normalized * amplitude, Vector3.up), _forceMode);
        if (IsGrounded(bodyPart.transform.position) == false) bodyPart.AddForce(Vector3.down * _additionalGravity, _forceMode);
    }

    [SerializeField] private int _bodyPartsGizmos = 8;
    /*private void OnDrawGizmos()
    {
        if (Application.isPlaying == false) return;
        
        for (int i = _bodyPartsGizmos; i <= _bodyPartsGizmos; i++)
        {
            var lastBodyPart = (i == 0) ? Head?.Rigidbody : BodyParts[i - 1];
            var bodyPart = (i == BodyParts.Length) ? Tail?.Rigidbody : BodyParts[i];
            if (lastBodyPart == null || bodyPart == null) continue;
            
            var offset = lastBodyPart.position - bodyPart.position;
            var targetDirection = offset.normalized;

            var offset2 = (_restDistance - offset.sqrMagnitude) * targetDirection;
            var force = offset2 * _followStrength;
            var damper = bodyPart.velocity * _damper;
            
            // Force
            GizmoExtensions.DrawArrow(bodyPart.position, force, Color.green);
            
            // Offset
            GizmoExtensions.DrawArrow(bodyPart.position, offset2, Color.blue);
            
            // Damping
            GizmoExtensions.DrawArrow(bodyPart.position, damper, Color.magenta);
        }
    }*/

    private bool IsGrounded(Vector3 position)
    {
        return Physics.RaycastNonAlloc(position, Vector3.down, _groundHits, _groundDetectionDistance, _groundLayer, QueryTriggerInteraction.Ignore) > 0;
    }

    private void UpdateLines()
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            var lastBodyPart = (i == 0) ? Head?.transform.position : BodyParts[i - 1].position;
            var bodyPart = (i == BodyParts.Length) ? Tail?.transform.position : BodyParts[i].position;
            if (lastBodyPart == null || bodyPart == null) continue;

            Lines[i].SetPosition(0, lastBodyPart.Value);
            Lines[i].SetPosition(1, bodyPart.Value);
        }
    }
}