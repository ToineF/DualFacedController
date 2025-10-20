using FeedbacksEditor;
using TMPro;
using UnityEngine;

namespace Cattac.Interactables
{
    public class BallZoneFeedbacks : MonoBehaviour
    {
        [SerializeField] private BallDropZone _dropZone;
        [SerializeField] private TMP_Text _textMesh;
        [SerializeField] private GameEvent _onEnterEvent;
        [SerializeField] private GameEvent _onExitEvent;
        [SerializeField] private GameEvent _onConditionMetEvent;

        private void Start()
        {
            _dropZone.OnBallAdded.AddListener(OnTriggerEnterEvent);
            _dropZone.OnBallRemoved.AddListener(OnTriggerExitEvent);
            _dropZone.OnConditionMet.AddListener(OnConditionMet);
            UpdateText();
        }

        private void OnTriggerEnterEvent()
        {
            if (_onEnterEvent) GameEventsManager.PlayEvent(_onEnterEvent, gameObject);
            UpdateText();
        }
        
        private void OnTriggerExitEvent()
        {
            if (_onExitEvent) GameEventsManager.PlayEvent(_onExitEvent, gameObject);
            UpdateText();
        }

        private void OnConditionMet()
        {
            if (_onConditionMetEvent) GameEventsManager.PlayEvent(_onConditionMetEvent, gameObject);
        }
        
        private void UpdateText()
        {
            if (_textMesh) _textMesh.text = _dropZone.RemainingBallAmount.ToString();
        }
    }
}