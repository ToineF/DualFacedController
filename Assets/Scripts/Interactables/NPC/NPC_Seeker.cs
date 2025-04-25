using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables.NPC
{
    public class NPC_Seeker : MonoBehaviour
    {
        public IDetectable Target { get; private set; }

        [Header("References")] [SerializeField]
        private NPC_LookAt _lookAt;

        [Header("Seek Parameters")] [SerializeField]
        private float _maxSeekAngle;

        [SerializeField] private float _seekRadius;
        [SerializeField] private float _seekDistance;
        [SerializeField] private LayerMask _seekLayer;
        [SerializeField] private LayerMask _obstaclesLayer;

        private void Update()
        {
            Target = GetTarget();
            _lookAt.Target = Target?.gameObject;
            UpdateInternal();
        }

        protected virtual void UpdateInternal() { }

        private IDetectable GetTarget()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, _seekRadius, transform.forward, _seekDistance,
                _seekLayer, QueryTriggerInteraction.Ignore);

            if (hits.Length <= 0) return null;

            float currentMinDistance = float.PositiveInfinity;
            IDetectable target = null;

            foreach (var hit in hits)
            {
                if (hit.collider == null || hit.collider.TryGetComponent(out IDetectable head) == false) continue;

                var distanceToHit = head.gameObject.transform.position - transform.position;
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

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            AntoineFoucault.Utilities.GizmoExtensions.DrawSphereCast(transform.position, _seekRadius, transform.forward,
                _seekDistance, Color.green);
        }
#endif
    }
}