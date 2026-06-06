using FeedbacksEditor;
using UnityEngine;

namespace Cattac.Interactables
{
    public class DanceTimerFeedback : MonoBehaviour
    {
        [SerializeField] private float _startIntervalle = .9f;
        [SerializeField] private float _endIntervalle = .2f;
        [SerializeField] private GameEvent _tickEvent;

        private float _timer;
        
        public void UpdateFeedback(float currentValue)
        {
            if (currentValue < 0 || currentValue > 1) return;
            
            _timer += Time.deltaTime;
            if (_timer >= Mathf.Lerp(_startIntervalle, _endIntervalle, currentValue))
            {
                GameEventsManager.PlayEvent(_tickEvent, gameObject);
                _timer = 0;
            }
        }
    }
}