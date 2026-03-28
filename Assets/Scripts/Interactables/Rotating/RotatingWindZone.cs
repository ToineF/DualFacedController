using UnityEngine;

namespace Cattac.Interactables
{
    public class RotatingWindZone : MonoBehaviour
    {
        public float TurnSpeed => _turnSpeed;
        
        [SerializeField] private float _turnSpeed = 10f;
        [SerializeField] private float _inwardMultipler = 1f;
        [SerializeField] private Vector3 _vector3Up = Vector3.up;
        [SerializeField] private float _maxRadius = 0.5f;

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(out Rigidbody rb)) return;

            Vector3 offset = transform.position - other.transform.position;
            float radius = offset.magnitude;

            radius = Mathf.Max(radius, _maxRadius); // Avoid division by 0

            // Tangential direction
            Vector3 tangent = Vector3.Cross(offset, _vector3Up).normalized;

            // Centripetal force
            float centripetalStrength = (_turnSpeed * _turnSpeed) / radius;
            Vector3 inwardForce = offset.normalized * centripetalStrength * _inwardMultipler;

            Vector3 force = tangent * _turnSpeed+ inwardForce;

            rb.AddForce(force, ForceMode.Force);
        }
    }
}