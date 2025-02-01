using UnityEngine;

namespace Cattac.Interactables
{
    public class Catapult : MonoBehaviour
    {
        [SerializeField] private Rigidbody _catapultBody;
        [SerializeField] private Vector3 _torque;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _catapultBody.AddTorque(_torque, ForceMode.Impulse);
            }
        }
    }
}