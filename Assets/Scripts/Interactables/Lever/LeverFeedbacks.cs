using FeedbacksEditor;
using UnityEngine;

namespace Cattac.Interactables
{
    public class LeverFeedbacks : MonoBehaviour
    {
        [SerializeField] private Lever _lever;
        [SerializeField] private GameEvent _onLeftEvent;
        [SerializeField] private GameEvent _onRightEvent;

        private void Start()
        {
            _lever.OnLeverLeft.AddListener(OnLeft);
            _lever.OnLeverRight.AddListener(OnRight);
        }

        private void OnDestroy()
        {
            _lever.OnLeverLeft.RemoveListener(OnLeft);
            _lever.OnLeverRight.RemoveListener(OnRight);
        }

        private void OnLeft()
        {
            if (_onLeftEvent != null) GameEventsManager.PlayEvent(_onLeftEvent, _lever.gameObject);
        }

        private void OnRight()
        {
            if (_onRightEvent != null) GameEventsManager.PlayEvent(_onRightEvent, _lever.gameObject);
        }
    }
}