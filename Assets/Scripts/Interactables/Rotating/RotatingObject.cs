using UnityEngine;

namespace Cattac.Interactables
{
    public class RotatingObject : MonoBehaviour
    {
        [SerializeField] private float _turnSpeed;
        [SerializeField] private Vector3 _vector3Up;

        void FixedUpdate()
        {
            transform.Rotate(_vector3Up * _turnSpeed);
        }
    }
}