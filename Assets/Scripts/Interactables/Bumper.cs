using DG.Tweening;
using System.Collections;
using FeedbacksEditor;
using UnityEngine;

namespace Cattac.Interactables
{
    public class Bumper : MonoBehaviour
    {
        [SerializeField] float _initialForce = 10f; // The initial force applied to the rigidbody
        [SerializeField] float _forceDecayRate = 1f; // Rate at which the force decreases over time
        [SerializeField] int _framesOfForce = 10; // Number of frames the force will be applied
        [SerializeField] private ForceMode _forceMode;
        [SerializeField] private bool _ignoreY;
        [SerializeField, Range(0,1)] private float _additionalYForce;

        [Header("Feedback")] [SerializeField] private Transform _visual;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _punchAmount = 1f;
        [SerializeField] private float _punchTime;
	    [SerializeField] private GameEvent _boingFeedback;

        [SerializeField] private float _timeBetweenBumps = 0.2f;
        
        private float _currentTimeBetweenBumps;
        //private HashSet<Rigidbody> _movedRigidbodies = new HashSet<Rigidbody>();

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log(other.name);
            if (other.TryGetComponent(out Rigidbody rb) == false) return;

            StartCoroutine(ApplyForceWithDecay(rb));
            if (_currentTimeBetweenBumps < 0)
            {
                _currentTimeBetweenBumps = _timeBetweenBumps;
                if (_animator != null) _animator.SetTrigger("Bump");
                if (_visual != null)
                {
                    _visual.DOComplete();
                    _visual.DOPunchScale(_punchAmount * Vector3.one, _punchTime);
                }
                if (_boingFeedback != null) GameEventsManager.PlayEvent(_boingFeedback, gameObject);
            }
        }

        private IEnumerator ApplyForceWithDecay(Rigidbody rb)
        {
            float currentForce = _initialForce;
            var direction = rb.transform.position - transform.position;
            if (_ignoreY) direction.y = 0;
            direction.Normalize();
            direction.y += _additionalYForce;

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

        private void Update()
        {
            _currentTimeBetweenBumps -= Time.deltaTime;
        }
    }
}