using FeedbacksEditor;
using UnityEngine;

namespace Cattac.Interactables
{
    public class TimedPressurePlateTickingFeedback : MonoBehaviour
    {
        [SerializeField] private TimedPressurePlate _timedPressurePlate;
        [SerializeField] private float _startIntervalle = .9f;
        [SerializeField] private float _endIntervalle = .2f;
        [SerializeField] private GameEvent _tickEvent;

        private bool _isTicking = false;
        private float _timer;

        private void Start()
        {
            _timedPressurePlate.OnTimerReset.AddListener(OnTriggerEnterEvent);
            _timedPressurePlate.OnTimerEnd.AddListener(OnTriggerExitEvent);
            _timedPressurePlate.OnDeactived.AddListener(() => Destroy(this));
        }

        private void OnTriggerEnterEvent()
        {
            _isTicking = true;
        }
        
        private void OnTriggerExitEvent()
        {
            _isTicking =  false;
        }

        private void Update()
        {
            if (_isTicking == false) return;
            
            _timer += Time.deltaTime;
            if (_timer >= Mathf.Lerp(_startIntervalle, _endIntervalle, _timedPressurePlate.RemainingTime))
            {
                GameEventsManager.PlayEvent(_tickEvent, gameObject);
                _timer = 0;
            }
        }
    }
}