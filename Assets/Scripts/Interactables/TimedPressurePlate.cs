using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class TimedPressurePlate : MonoBehaviour
    {
        public UnityEvent OnTimerReset;
        public bool IsTicking => _isTicking;
        
        [SerializeField] private float _deactivateTimer;
        [SerializeField] private PressurePlate _pressurePlate;
        [SerializeField] private UnityEvent _onTimerEnd;
        [SerializeField] private UnityEvent _onDeactivated;

        private float _timer;
        private bool _isTicking = false;

        private void Awake()
        {
            _pressurePlate.OnTriggerEnterEvent.AddListener(ResetTimerSelf);
        }

        private void ResetTimerSelf()
        {
            if (_isTicking) return;
            
            ResetTimer();
            OnTimerReset?.Invoke();
        }

        public void ResetTimer()
        {
            _isTicking = true;
            _timer = _deactivateTimer;
        }

        private void Update()
        {
            if (_isTicking == false) return;
            
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _isTicking = false;
                _onTimerEnd?.Invoke();
            }
        }

        public void Deactivate()
        {
            _isTicking = false;
            _timer = _deactivateTimer;
            _pressurePlate.OnTriggerEnterEvent.RemoveListener(ResetTimerSelf);
            _pressurePlate.OnTriggerExitEvent.RemoveListener(ResetTimerSelf);
            _onDeactivated?.Invoke();
        }
    }
}