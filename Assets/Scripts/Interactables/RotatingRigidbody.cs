using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotatingRigidbody : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Vector3 _vector3Up;

    void Update()
    {
        _rb.angularVelocity = _vector3Up * _turnSpeed;
    }
}