using UnityEngine;

namespace Cattac.Interactables
{
    public class Rope : FakeParent
    {
        [Header("Rope params")] [SerializeField]
        private float _forceMultiplier = 1f;

        [SerializeField] private float _ungrabForce = 1f;
        [SerializeField] private ForceMode _ungrabForceMode;

        protected override void AddForceInternal(Vector3 force)
        {
            _selfRigidbody.AddForce(force * _forceMultiplier, ForceMode.Impulse);
        }

        protected override void OnUngrabInternal(Rigidbody rb)
        {
            rb.AddForce(Vector3.up * _ungrabForce, _ungrabForceMode);
        }
    }
}
