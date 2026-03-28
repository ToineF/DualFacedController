using UnityEngine;

namespace Cattac.Interactables
{
    [RequireComponent(typeof(Rigidbody))]
    public class RotatingRigidbody : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private Vector3 _vector3Up;
        //[SerializeField] private ForceMode _forceMode;

        /*private void FixedUpdate()
        {
            _rb.AddTorque(_vector3Up * _turnSpeed, _forceMode);
        }*/
        
        // void FixedUpdate()
        // {
        //     _rb.angularVelocity = _vector3Up * _turnSpeed;
        // }
        
        void FixedUpdate()
        {
            _rb.MoveRotation(
                _rb.rotation * Quaternion.Euler(_vector3Up * (_turnSpeed * Time.fixedDeltaTime))
            );
        }
    }
}