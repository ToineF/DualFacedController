using System.Collections;
using System.Collections.Generic;
using Cattac.Interactables.MouseCollection;
using DG.Tweening;
using FeedbacksEditor;
using MaskTransitions;
using UnityEngine;

namespace Cattac.Collectibles.EndLevel
{
    /// <summary>
    /// Handles the end of the level celebration
    /// </summary>
    public class EndLevelCelebration : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Transform[] _miceSlots;

        [SerializeField] private GameObject _canon;
        [SerializeField] private Transform _canonTransform;
        [SerializeField] private Animator _canonAnimator;
        [SerializeField] private SceneSwitch _sceneSwitch;
        [SerializeField] private Animator _playerZoneAnimator;

        [Header("Feedbacks")] [SerializeField] private GameObject _enterZoneCamera;
        [SerializeField] private string _playZoneAnimation;
        [SerializeField] private float _waitTimeBetweenFalls = 0.5f;
        [SerializeField] private GameEvent _mouseLandFeedback;
        [SerializeField] private GameEvent _waveStartFeedback;
        [SerializeField] private string _waveBoolName;
        [SerializeField] private float _waveTime = 3f;
        [SerializeField] private float _jumpDuration = .8f;
        [SerializeField] private Ease _jumpEase;
        [SerializeField] private float _timeBetweenJumps = .8f;
        [SerializeField] private string _rollTriggerName;
        [SerializeField] private Vector3 _canonScaleAmount;
        [SerializeField] private float _canonScaleDuration = .5f;
        [SerializeField] private Ease _canonScaleEase;
        [SerializeField] private GameEvent _canonEnterFeedback;
        [SerializeField] private float _afterLaunchCameraWaitTime = 2f;
        [SerializeField] private GameObject _launchCamera;
        [SerializeField] private float _beforeLaunchWaitTime = 1f;
        [SerializeField] private string _canonLaunchTriggerName;
        [SerializeField] private GameEvent _canonLaunchFeedback;
        [SerializeField] private Transform _smokeFeedback;
        [SerializeField] private Transform _smokeFeedbackEnd;
        [SerializeField] private float _smokeDuration;
        [SerializeField] private GameEvent _smokeEndEvent;
        [SerializeField] private float _waitTransitionDuration;

        private List<SavedMouseMesh> _miceMeshes = new();

        public void Celebrate()
        {
            StartCoroutine(MakeAllMiceAppear());
        }

        private IEnumerator MakeAllMiceAppear()
        {
            var saveSystem = MainGame.Instance.CollectiblesSaveSystem;
            var mice = MainGame.Instance.LevelCollectiblesData.Mice;

            if (_miceSlots.Length != mice.Count)
                Debug.LogError(
                    $"The number of MiceSlots ({_miceSlots.Length}) isn't equal to the number of mice in the level ({mice.Count})!");

            // Make every saved mouse appear on screen
            _enterZoneCamera.SetActive(true);
            _playerZoneAnimator.Play(_playZoneAnimation);
            for (int i = 0; i < mice.Count; i++)
            {
                var data = mice[i].Data;
                if (saveSystem.IsMouseUnlocked(data))
                {
                    var newMouse = Instantiate(data.Mesh, _miceSlots[i].position, Quaternion.identity);
                    _miceMeshes.Add(newMouse);
                    GameEventsManager.PlayEvent(_mouseLandFeedback, newMouse.gameObject);
                    yield return new WaitForSeconds(_waitTimeBetweenFalls);
                }
            }

            // Make all mice wave
            foreach (var mouse in _miceMeshes)
            {
                mouse.Animator.SetBool(_waveBoolName, true);
            }

            GameEventsManager.PlayEvent(_waveStartFeedback, gameObject);

            yield return new WaitForSeconds(_waveTime);
            _launchCamera.SetActive(true);
            yield return new WaitForSeconds(_afterLaunchCameraWaitTime);


            // Turn every mouse towards the canon
            Vector3 canonPosition = _canonTransform.position;
            foreach (var mouse in _miceMeshes)
            {
                var direction = new Vector3(canonPosition.x, mouse.transform.position.y, canonPosition.z) -
                                mouse.transform.position;
                mouse.Animator.SetBool(_waveBoolName, false);
                mouse.transform.DOLookAt(mouse.transform.position - direction, _jumpDuration).SetEase(_jumpEase);
            }

            yield return new WaitForSeconds(_jumpDuration);


            // Each mouse jumps into the canon
            foreach (var mouse in _miceMeshes)
            {
                mouse.Animator.SetTrigger(_rollTriggerName);
                mouse.transform.SetParent(_canonTransform, true);
                mouse.transform.DOLocalMove(Vector3.zero, _jumpDuration).SetEase(_jumpEase);
                //mouse.transform.DOLocalRotate(Vector3.zero, _jumpDuration).SetEase(_jumpEase);
                StartCoroutine(MouseJumpInCanon(mouse));

                yield return new WaitForSeconds(_timeBetweenJumps);
            }


            yield return new WaitForSeconds(_beforeLaunchWaitTime);

            // Launches the mice in the sky
            _canonAnimator.SetTrigger(_canonLaunchTriggerName);
            GameEventsManager.PlayEvent(_canonLaunchFeedback, _canon);
            _smokeFeedback.DOMove(_smokeFeedbackEnd.position, _smokeDuration);
            yield return new WaitForSeconds(_smokeDuration);
            GameEventsManager.PlayEvent(_smokeEndEvent, _smokeFeedbackEnd.gameObject);

            yield return new WaitForSeconds(_waitTransitionDuration);

            // Return to hub
            _sceneSwitch.SwitchScene();
        }

        private IEnumerator MouseJumpInCanon(SavedMouseMesh mouse)
        {
            yield return new WaitForSeconds(_jumpDuration);

            mouse.gameObject.SetActive(false);
            _canon.transform.DOPunchScale(_canonScaleAmount, _canonScaleDuration).SetEase(_canonScaleEase);
            GameEventsManager.PlayEvent(_canonEnterFeedback, _canon);
        }
    }
}