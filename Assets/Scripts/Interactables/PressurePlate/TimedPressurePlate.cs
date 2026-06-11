using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class TimedPressurePlate : MonoBehaviour
    {
        public UnityEvent OnTimerReset;
        public UnityEvent OnTimerEnd => _onTimerEnd;
        public UnityEvent OnDeactived => _onDeactivated;
        public bool IsTicking => _isTicking;

        public float RemainingTime => (_deactivateTimer - _timer)/Mathf.Max(_deactivateTimer, 0.0001f);
        
        [SerializeField] private float _deactivateTimer;
        [SerializeField] private PressurePlate _pressurePlate;
        [SerializeField] private UnityEvent _onTimerEnd;
        [SerializeField] private UnityEvent _onDeactivated;
        [SerializeField] private ActivateSwitch _activateSwitch;

        private float _timer;
        private bool _isTicking = false;

        private void Awake()
        {
            _pressurePlate.OnTriggerEnterEvent.AddListener(ResetTimerSelf);
        }

        private void ResetTimerSelf()
        {
            if (_isTicking) return;
            
            _activateSwitch.Switch(1);
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
                _timer = 0;
                if (_pressurePlate.IsPressed) ResetTimerSelf();
                _activateSwitch.Switch(0);
                _onTimerEnd?.Invoke();
            }
        }

        public void Deactivate(bool playEvent = true)
        {
            _isTicking = false;
            _timer = _deactivateTimer;
            _activateSwitch.Switch(2);
            _pressurePlate.OnTriggerEnterEvent.RemoveListener(ResetTimerSelf);
            _pressurePlate.OnTriggerExitEvent.RemoveListener(ResetTimerSelf);
            if (playEvent) _onDeactivated?.Invoke();
        }
    }
}