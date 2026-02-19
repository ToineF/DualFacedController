using UnityEngine;

namespace Cattac.Interactables.NPC
{
    public class NPC_LookAtPlayer : MonoBehaviour
    {
        [SerializeField] private float _lookAtLerp;

        private GameObject _target;
        private Vector3 _lastPosition;

        private void Start()
        {
            _lastPosition = transform.position + transform.forward;
            _target = MainGame.Instance.PlayerController;
        }

        private void Update()
        {
            UpdateLook();
        }

        private void UpdateLook()
        {
            if (_target != null) _lastPosition = _target.transform.position;

            //transform.LookAt(_lastPosition);
            //Vector3 direction = _lastPosition - transform.position;
            //Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
            //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, _lookAtLerp);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _lookAtLerp * Time.deltaTime);
            var rotation = Quaternion.LookRotation((_lastPosition - transform.position).normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _lookAtLerp);
        }
    }
}