using FeedbacksEditor;
using UnityEngine;

namespace Cattac.Interactables
{
    public class TimedPressurePlateGroupTickingFeedback : MonoBehaviour
    {
        [SerializeField] private TimedPressurePlateGroup _group;
        [SerializeField] private float _startIntervalle = .9f;
        [SerializeField] private float _endIntervalle = .2f;
        [SerializeField] private AnimationCurve _lerpCurve;
        [SerializeField] private GameEvent _tickEvent;

        private bool _isTicking = false;
        private float _timer;

        private void Start()
        {
            foreach (var pressurePlate in _group.PressurePlates)
            {
                pressurePlate.OnTimerReset.AddListener(OnTriggerEnterEvent);
                pressurePlate.OnTimerEnd.AddListener(OnTriggerExitEvent);
                pressurePlate.OnDeactived.AddListener(OnDeactivated);
            }
        }


        private void OnTriggerEnterEvent()
        {
            _isTicking = true;
            _timer = 0;
        }
        
        private void OnTriggerExitEvent()
        {
            _isTicking =  false;
            _timer = 0;
        }
        private void OnDeactivated()
        {
            _isTicking = false;
            foreach (var pressurePlate in _group.PressurePlates)
            {
                pressurePlate.OnTimerReset.RemoveListener(OnTriggerEnterEvent);
                pressurePlate.OnTimerEnd.RemoveListener(OnTriggerExitEvent);
                pressurePlate.OnDeactived.RemoveListener(OnDeactivated);
            }
            Destroy(this);
        }
        
        private void Update()
        {
            if (_isTicking == false) return;
            
            _timer += Time.deltaTime;
            
            float remainingTime = 0f;
            foreach (var pressurePlate in _group.PressurePlates)
            {
                var time = pressurePlate.RemainingTime;
                if (time < 0.99f) remainingTime = time;
            }

            if (_timer >= Mathf.Lerp(_startIntervalle, _endIntervalle, _lerpCurve.Evaluate(remainingTime)))
            {
                GameEventsManager.PlayEvent(_tickEvent, gameObject);
                _timer = 0;
            }
        }
    }
}