using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace Cattac.Character.Visuals
{
    /// <summary>
    /// Represents the visual of a unit based on its direction.
    /// Handles all the animations and visual rotation.
    /// </summary>
    public class HeadAnimator : MonoBehaviour
    {
        [SerializeField] private CharacterHead _characterHead;

        [SerializeField] private Animator _animator;
        [SerializeField] private bool _isOrientationInverted;
        [SerializeField] private float _lookAtLerp;

        private Transform _parent;
        private Vector3 _offset;
        private Camera _camera;

        private void Start()
        {
            _offset = transform.localPosition;
            _parent = transform.parent;
            transform.SetParent(null);
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            RotateDirection();
            UpdateAnimation();
            transform.position = _parent.transform.position + _offset;
        }

        private void UpdateAnimation()
        {
            _animator.SetBool("IsWalking", _characterHead.InputDirection.sqrMagnitude > 0.1f);
            _animator.SetBool("IsGrabbing", _characterHead.IsGrabbing);
        }

        private void RotateDirection()
        {
            Vector3 moveDirection = _characterHead.InputDirection;
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
            moveDirection = _camera.transform.forward * moveDirection.z + _camera.transform.right * moveDirection.x;
            moveDirection.y = 0;

            int orientation = _isOrientationInverted ? -1 : 1;

            Vector3 point = transform.position - moveDirection * orientation;
            Vector3 direction = point - transform.position;
            if (direction.magnitude < 0.001f) return;
            
            Quaternion toRotation = Quaternion.LookRotation(direction, transform.up);
            transform.localRotation =
                Quaternion.Lerp(transform.localRotation, toRotation, _lookAtLerp * Time.deltaTime);
        }
    }
}