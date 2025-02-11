using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables
{
    public class FakeParent : MonoBehaviour, IGrabbable
    {
        [SerializeField] protected Rigidbody _selfRigidbody;
        [SerializeField] protected Rigidbody _rigidbodyToApproach;
        [SerializeField] private float _force;
        [SerializeField] private int _forceIterations;
        [SerializeField] private bool _isParent = true;
        [SerializeField] private ForceMode ForceMode;

        public void OnGrab(CharacterHead characterHead)
        {
            _rigidbodyToApproach = characterHead.CurrentRigidbody;
        }

        public void OnUngrab(CharacterHead characterHead)
        {
            _rigidbodyToApproach = null;
            _selfRigidbody.linearVelocity = Vector3.zero;
            _selfRigidbody.angularVelocity = Vector3.zero;
            OnUngrabInternal(characterHead.CurrentRigidbody);
        }

        protected virtual void OnUngrabInternal(Rigidbody rb)
        {

        }

        private void FixedUpdate()
        {
            Follow();
            FixedUpdateInternal();
        }

        private void Follow()
        {
            if (_rigidbodyToApproach == null) return;

            var offset = (transform.position - _rigidbodyToApproach.position);
            var targetDirection = offset.normalized;
            for (int k = 0; k < _forceIterations; k++)
            {
                var force = _force * offset.sqrMagnitude * targetDirection;
                var rb = _isParent ? _rigidbodyToApproach : _selfRigidbody;
                if (_isParent == false) force *= -1;
                rb.AddForce(force, ForceMode);
            }
        }

        protected virtual void FixedUpdateInternal()
        {

        }

        public void AddForce(Vector3 force)
        {
            AddForceInternal(force);
        }

        protected virtual void AddForceInternal(Vector3 force)
        {
            _selfRigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}