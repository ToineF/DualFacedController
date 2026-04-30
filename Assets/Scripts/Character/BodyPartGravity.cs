using UnityEngine;

namespace Cattac.Character
{
    public class BodyPartGravity : MonoBehaviour
    {
        public bool IsGrounded { get; private set; }

        [Header("References")] [SerializeField]
        private CharacterHeadData _data;

        [SerializeField] private Rigidbody _rb;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private ConstantForce _constantForce;
        [SerializeField] private CharacterBody _body;

        [Header("Parameters")]
        [SerializeField] private float _gravity;
        [SerializeField] private float _pushForce;
        [SerializeField] private Rigidbody[] _rbOthers;

        private RaycastHit _lastGroundHit;

        private void FixedUpdate()
        {
            GetGroundNormal();

            // _constantForce.enabled = _isGrounded == false;
            if (IsGrounded || _rbOthers.Length <= 0) return;

            _rbOthers[0].AddForce(Vector3.down * _gravity, ForceMode.Force);

            for (var i = 1; i < _rbOthers.Length; i++)
            {
                var direction = (_rbOthers[i - 1].position - _rbOthers[i].position).normalized;
                _rbOthers[i].AddForce(direction * _pushForce, ForceMode.Force);
            }
        }

        private void GetGroundNormal()
        {
            IsGrounded = Physics.SphereCast(transform.position + Vector3.up * _data.GroundDetectionUpOffset, _collider.radius, Vector3.down, out _lastGroundHit, _data.GroundDetectionDistance, _data.GroundLayer, QueryTriggerInteraction.Ignore);
            //CurrentRigidbody.AddForce(Vector3.ProjectOnPlane(Vector3.down, _lastGroundHit.normal) * _data.AdditionalGravity, ForceMode.Impulse);
        }
    }
}