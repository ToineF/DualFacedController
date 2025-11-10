using UnityEngine;


namespace Cattac.Character.Visuals
{
    /// <summary>
    /// Represents the visual of a unit based on its direction.
    /// Handles all the animations and visual rotation.
    /// </summary>
    public class HeadAnimator : MonoBehaviour
    {
        public CharacterHead CharacterHead => _characterHead;
        [SerializeField] private CharacterHead _characterHead;
        [SerializeField] private Transform _neighbourBodyPart;

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
            transform.GetChild(0).gameObject.SetActive(_parent.gameObject.activeInHierarchy); // Visual becomes inactive when parent is inactive
        }

        private void UpdateAnimation()
        {
            _animator.SetBool("IsWalking", _characterHead.InputDirection.sqrMagnitude > 0.1f);
            _animator.SetBool("IsGrabbing", _characterHead.IsGrabbing);
        }

        private void RotateDirection()
        {
            if (_characterHead.IsSeparated)
            {
                Vector3 moveDirection = _characterHead.NormalizedDirection;
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
            else
            {
                var neighbourPosition = _neighbourBodyPart.position;
                neighbourPosition.y = 0;
                var position = transform.position;
                position.y = 0;
                Quaternion _lookRotation = Quaternion.LookRotation((neighbourPosition - position).normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * _lookAtLerp);
            }
        }
    }
}