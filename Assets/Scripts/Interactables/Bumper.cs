using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Cattac.Interactables
{
    public class Bumper : MonoBehaviour
    {
        [SerializeField] float _initialForce = 10f; // The initial force applied to the rigidbody
        [SerializeField] float _forceDecayRate = 1f; // Rate at which the force decreases over time
        [SerializeField] int _framesOfForce = 10; // Number of frames the force will be applied
        [SerializeField] private ForceMode _forceMode;

        [Header("Feedback")] [SerializeField] private Transform _visual;
        [SerializeField] private float _punchAmount = 1f;
        [SerializeField] private float _punchTime;
	[SerializeField] private AudioSource _boingSFX;

        //private HashSet<Rigidbody> _movedRigidbodies = new HashSet<Rigidbody>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Rigidbody rb) == false) return;

            StartCoroutine(ApplyForceWithDecay(rb));
            _visual.DOComplete();
            _visual.DOPunchScale(_punchAmount * Vector3.one, _punchTime);
	    _boingSFX.volume = Random.Range(0.7f, 1.0f);
	    _boingSFX.pitch = Random.Range(0.8f, 1.2f);
	    _boingSFX.Play();
        }

        private IEnumerator ApplyForceWithDecay(Rigidbody rb)
        {
            float currentForce = _initialForce;
            var direction = rb.transform.position - transform.position;
            direction.Normalize();

            // Apply force over several frames with decay
            for (int i = 0; i < _framesOfForce; i++)
            {
                if (currentForce <= 0) break;

                rb.AddForce(direction * currentForce, _forceMode);
                currentForce -= _forceDecayRate;

                // Wait until the next frame
                yield return null;
            }
        }
    }
}