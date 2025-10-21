using FeedbacksEditor;
using UnityEngine;

namespace Cattac.Interactables
{
    public class TimedPressurePlateFeedbacks : MonoBehaviour
    {
        [SerializeField] private TimedPressurePlate _timedPressurePlate;
        [SerializeField] private GameEvent _onEnterEvent;
        [SerializeField] private GameEvent _onExitEvent;
        [SerializeField] private GameEvent _onDeactivatedEvent;

        private void Start()
        {
            _timedPressurePlate.OnTimerReset.AddListener(OnTriggerEnterEvent);
            _timedPressurePlate.OnTimerEnd.AddListener(OnTriggerExitEvent);
            _timedPressurePlate.OnDeactived.AddListener(OnDeactivatedEvent);
        }

        private void OnTriggerEnterEvent()
        {
            if (_onEnterEvent) GameEventsManager.PlayEvent(_onEnterEvent, gameObject);
        }
        
        private void OnTriggerExitEvent()
        {
            if (_onExitEvent) GameEventsManager.PlayEvent(_onExitEvent, gameObject);
        }
        
        private void OnDeactivatedEvent()
        {
            if (_onDeactivatedEvent) GameEventsManager.PlayEvent(_onDeactivatedEvent, gameObject);
        }
    }
}