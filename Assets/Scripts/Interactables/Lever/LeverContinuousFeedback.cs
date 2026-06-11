using FeedbacksEditor;
using UnityEngine;

namespace Cattac.Interactables
{
    public class LeverContinuousFeedback : MonoBehaviour
    {
        [SerializeField] private Rigidbody _leverRigidbody;
        [SerializeField] private float _minAngularVelocity;
        [SerializeField] private GameEvent _leverPullFeedback;
        [SerializeField] private float _timeBetweenFeedbacks;

        private float _feedbacksTimer;
        
        private void Update()
        {
            if (_leverRigidbody.angularVelocity.magnitude <= _minAngularVelocity)
            {
                _feedbacksTimer = 0;
                return;
            }
            
            _feedbacksTimer += Time.deltaTime;

            if (_feedbacksTimer >= _timeBetweenFeedbacks)
            {
                _feedbacksTimer = 0;
                GameEventsManager.PlayEvent(_leverPullFeedback, _leverRigidbody.gameObject);
            }
        }
    }
}