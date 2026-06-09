using System;
using UnityEngine;


namespace Cattac.Character.Visuals
{
    /// <summary>
    /// Represents the visual of a unit based on its direction.
    /// Handles all the animations and visual rotation.
    /// </summary>
    public class HeadAnimator : MonoBehaviour
    {
        [SerializeField] private CharacterHead _characterHead;
        [SerializeField] private BodyPartGravity _bodyPartGravity;
        [SerializeField] private Transform _neighbourBodyPart;
        [SerializeField] private Vector3 _positionOffset;

        [SerializeField] private Animator _animator;
        [SerializeField] private bool _isOrientationInverted;
        [SerializeField] private float _lookAtLerp;
        
        [Header("Audio")]
        [SerializeField] private AudioSource _walkAudioSource;
        [SerializeField] private float _walkAudioVolume = 1f;
        [SerializeField] private float _walkAudioVolumeLerp = 1f;

        private Transform _parent;
        private Vector3 _startOffset;
        private Camera _camera;
        
        private const string _isWalkingParameter = "IsWalking";
        private const string _isGroundedParameter = "IsGrounded";
        private const string _moveSpeedParameter = "MoveSpeed";
        private const string _isSqueakingParameter = "IsSqueaking";

        private void Start()
        {
            _startOffset = transform.localPosition;
            _parent = transform.parent;
            transform.SetParent(null);
            _camera = Camera.main;
            AudioManager.Instance.VolumeManager.AddSFXSource(_walkAudioSource);
        }

        private void FixedUpdate()
        {
            transform.position = _parent.transform.position + _startOffset + _positionOffset;
        }

        private void LateUpdate()
        {
            RotateDirection();
            UpdateAnimation();
           
            transform.GetChild(0).gameObject.SetActive(_parent.gameObject.activeInHierarchy); // Visual becomes inactive when parent is inactive
        }

        private void UpdateAnimation()
        {
            if (_characterHead == null) return;
            _animator.SetBool(_isWalkingParameter, _characterHead.InputDirection.sqrMagnitude > 0.1f);
            _walkAudioSource.mute = _characterHead.InputDirection.sqrMagnitude <= 0.1f;
            // var targetVolume = (_characterHead.InputDirection.sqrMagnitude > 0.1f) ? _walkAudioVolume : 0;
            // _walkAudioSource.volume = Mathf.Lerp(_walkAudioSource.volume, targetVolume, Time.deltaTime * _walkAudioVolumeLerp);
            _animator.SetBool(_isSqueakingParameter, _characterHead.IsGrabbing);
            var moveSpeed = Mathf.Clamp01(new Vector3(_characterHead.CurrentRigidbody.linearVelocity.x,  0, _characterHead.CurrentRigidbody.linearVelocity.z).sqrMagnitude/100);
            _animator.SetFloat(_moveSpeedParameter, moveSpeed < 0.01f ? 0 : moveSpeed);
            _animator.SetBool(_isGroundedParameter, _bodyPartGravity.IsGrounded);
        }

        private void RotateDirection()
        {
            if (_characterHead != null && _characterHead.IsSeparated)
            {
                Vector3 moveDirection = _characterHead.NormalizedDirection;
                moveDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
                moveDirection = _camera.transform.forward * moveDirection.z + _camera.transform.right * moveDirection.x;
                moveDirection.y = 0;

                int orientation = _isOrientationInverted ? -1 : 1;

                Vector3 point = transform.position - moveDirection * orientation;
                Vector3 direction = point - transform.position;
                if (direction.sqrMagnitude < 0.00001f) return;
            
                Quaternion toRotation = Quaternion.LookRotation(direction, transform.up);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, toRotation, _lookAtLerp * Time.deltaTime);
            }
            else
            {
                var neighbourPosition = _neighbourBodyPart.position;
                neighbourPosition.y = 0;
                var position = transform.position;
                position.y = 0;
                Quaternion lookRotation = Quaternion.LookRotation((neighbourPosition - position).normalized);
                float t = 1f - Mathf.Exp(-_lookAtLerp * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, t);
            }
        }
    }
}