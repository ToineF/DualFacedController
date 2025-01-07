using UnityEngine;


public class NPC_Seeker : MonoBehaviour
{
    public Head Target { get; private set; }

    [Header("References")]
    [SerializeField] private NPC_LookAt _lookAt;

    [Header("Seek Parameters")]
    [SerializeField] private float _maxSeekAngle;
    [SerializeField] private float _seekRadius;
    [SerializeField] private float _seekDistance;
    [SerializeField] private LayerMask _seekLayer;
    [SerializeField] private LayerMask _obstaclesLayer;

    private void Update()
    {
        Target = GetTarget();
        _lookAt.Target = Target?.gameObject;
    }

    private Head GetTarget()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _seekRadius, transform.forward, _seekDistance, _seekLayer, QueryTriggerInteraction.Ignore);

        if (hits.Length <= 0) return null;

        float currentMinDistance = float.PositiveInfinity;
        Head target = null;

        foreach (var hit in hits)
        {
            if (hit.collider == null || hit.collider.TryGetComponent(out Head head) == false) continue;

            var distanceToHit = head.transform.position - transform.position;
            if (Physics.Raycast(transform.position, distanceToHit, distanceToHit.magnitude, _obstaclesLayer)) continue;

            float currentAngle = Vector3.Angle(transform.forward, distanceToHit.normalized);
            float distanceMagnitude = distanceToHit.magnitude;

            if (_maxSeekAngle > currentAngle && currentMinDistance > distanceMagnitude)
            {
                currentMinDistance = distanceMagnitude;
                target = head;
            }
        }

        return target;
    }

    private void OnDrawGizmos()
    {
        AntoineFoucault.Utilities.GizmoExtensions.DrawSphereCast(transform.position, _seekRadius, transform.forward, _seekDistance, Color.green);
    }
}