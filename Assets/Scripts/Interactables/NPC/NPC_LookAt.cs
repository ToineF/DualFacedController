using System;
using UnityEngine;

namespace Cattac.Interactables.NPC
{
    public class NPC_LookAt : MonoBehaviour
    {
        public GameObject Target { get; set; }

        [SerializeField] private float _lookAtLerp;

        private Vector3 _lastPosition;

        private void Start()
        {
            _lastPosition = transform.position + transform.forward;
        }

        private void Update()
        {
            UpdateLook();
        }

        private void UpdateLook()
        {
            if (Target != null) _lastPosition = Target.transform.position;

            //transform.LookAt(_lastPosition);
            //Vector3 direction = _lastPosition - transform.position;
            //Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
            //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, _lookAtLerp);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _lookAtLerp * Time.deltaTime);
            var rotation = Quaternion.LookRotation(_lastPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _lookAtLerp);
        }
    }
}