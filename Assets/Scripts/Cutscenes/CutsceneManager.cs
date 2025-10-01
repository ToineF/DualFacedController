using UnityEngine;
using UnityEngine.Playables;
using Cattac.Character;
using Cattac.Character.Multiplayer;

namespace Cattac.Cutscenes
{
    public class CutsceneManager : MonoBehaviour
    {
        public static CutsceneManager Instance;

        [SerializeField] private CutsceneWaitForTimeManager _waitForTimeManager;

        private CharacterHead _character;

        // Skip
        private float _skipCutsceneTime;
        private bool _isSkippingCutscene;

        private Cutscene _currentInteractionSequence;
        private int _currentSequenceIndex;

        private void Awake()
        {
            Instance = this;
        }

        public void StartNewSequence(Cutscene cutscene, CharacterHead character)
        {
            _currentInteractionSequence = cutscene;

            if (_currentInteractionSequence.StopPlayerMovements)
            {
                MainGame.Instance.PlayersManager.SetInput(InputType.CUTSCENE);
            }

            _character = character;

            GoToSequenceAt(0);
        }

        private void GoToSequenceAt(int index)
        {
            if (!_currentInteractionSequence) return;

            _currentSequenceIndex = Mathf.Clamp(index, 0, _currentInteractionSequence.SequenceElements.List.Count - 1);

            if (index < 0 || index > _currentInteractionSequence.SequenceElements.List.Count - 1) EndSequence();
            else ReadSequenceElement();
        }

        private void GoToNextSequenceElement()
        {
            GoToSequenceAt(_currentSequenceIndex + 1);
        }

        private void EndSequence()
        {
            MainGame.Instance.PlayersManager.SetInput(InputType.CUTSCENE_RESUME);
        }

        private void ReadSequenceElement()
        {
            CutsceneElement interactionElement = _currentInteractionSequence.SequenceElements.List[_currentSequenceIndex];

            if (interactionElement is CutsceneWaitForTime waitForTime)
            {
                StartWaitForTime(waitForTime);
            }

            if (interactionElement is CutsceneEvent function)
            {
                StartPlayEvent(function);
            }

            if (interactionElement is CutsceneTimeline timeline)
            {
                StartTimeline(timeline);
            }
        }

        private void StartWaitForTime(CutsceneWaitForTime waitForTime)
        {
            _waitForTimeManager.StartTimer(waitForTime.WaitTime);
            _waitForTimeManager.OnTimerEnd += EndWaitForTime;
        }

        private void EndWaitForTime()
        {
            _waitForTimeManager.OnTimerEnd -= EndWaitForTime;
            GoToNextSequenceElement();
        }

        private void StartPlayEvent(CutsceneEvent function)
        {
            function.Event?.Invoke();
            GoToNextSequenceElement();
        }

        private void StartTimeline(CutsceneTimeline timeline)
        {
            timeline.Director.Play();
            timeline.Director.stopped += EndTimeline;
        }

        private void EndTimeline(PlayableDirector director)
        {
            director.stopped -= EndTimeline;
            GoToNextSequenceElement();
        }

        #region Skip

        /*
        public void SetSkipCutscene(bool isSkipping)
        {
            _isSkippingCutscene = isSkipping;
        }

        private void Update()
        {
            if (!_isSkippingCutscene)
            {
                _skipCutsceneTime = 0;
                return;
            }

            _skipCutsceneTime += Time.deltaTime;

            if (_skipCutsceneTime > Manager.Data.CutsceneData.CutsceneSkipTime)
            {
                SkipCutscene();
            }
        }

        private void SkipCutscene()
        {
            _skipCutsceneTime = 0;
            _isSkippingCutscene = false;
            ForceEndCutscene();
            foreach (var skipEvent in _currentInteractionSequence.OnSkipEvents)
            {
                skipEvent?.Invoke();
            }
        }

        private void ForceEndCutscene()
        {
            // Wait for time
            _waitForTimeManager.OnTimerEnd -= EndWaitForTime;

            // Dialogue
            _dialogueManager.OnDialogueEnd -= EndDialogue;
            HideDialogueWindow();

            // End Sequence
            CurrentSequenceIndex = _currentInteractionSequence.SequenceElements.List.Count;
        }
        */

        #endregion
    }
}