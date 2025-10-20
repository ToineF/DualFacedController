using FeedbacksEditor;
using UnityEngine;

namespace Cattac.Interactables
{
    public class PressurePlateFeedbacks : MonoBehaviour
    {
        [SerializeField] private PressurePlate _pressurePlate;
        [SerializeField] private GameEvent _onEnterEvent;
        [SerializeField] private GameEvent _onExitEvent;

        private void Start()
        {
            _pressurePlate.OnTriggerEnterEvent.AddListener(OnTriggerEnterEvent);
            _pressurePlate.OnTriggerExitEvent.AddListener(OnTriggerExitEvent);
        }

        private void OnTriggerEnterEvent()
        {
            if (_onEnterEvent) GameEventsManager.PlayEvent(_onEnterEvent, gameObject);
        }
        
        private void OnTriggerExitEvent()
        {
            if (_onExitEvent) GameEventsManager.PlayEvent(_onExitEvent, gameObject);
        }
    }
}