using UnityEngine;

namespace AntoineFoucault.Utilities.Rigidbody
{
    public class RigidbodyRotator : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Rigidbody _reference;
        [SerializeField] private float _multiplier = 1f;

        private void FixedUpdate()
        {
            _reference.MoveRotation(_reference.rotation * Quaternion.Euler(0, _reference.linearVelocity.x * _multiplier, 0));
        }
    }
}