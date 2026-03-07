using System.Collections;
using System.Collections.Generic;
using Cattac.Character.Multiplayer;
using Cattac.Cutscenes;
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

        [Header("Feedbacks/Enter Zone")] [SerializeField]
        private GameObject _enterZoneCamera;

        [SerializeField] private string _playZoneAnimation;

        [Header("Feedbacks/Mice Appear")]
        [SerializeField] private float _waitBeforeAll = 2f;
        [SerializeField] private float _waitBeforeMiceAppear = 1f;

        [SerializeField] private float _waitTimeBetweenFalls = 0.5f;
        [SerializeField] private CollectiblesUI _collectiblesUI;
        [SerializeField] private GameEvent _mouseLandFeedback;
        [SerializeField] private GameEvent _waveStartFeedback;
        [SerializeField] private float _waitTimeBeforeWave = 0.5f;
        [SerializeField] private string _waveBoolName;
        [SerializeField] private float _waveTime = 3f;
        [SerializeField] private float _jumpDuration = .8f;
        [SerializeField] private Ease _jumpEase;
        [SerializeField] private float _timeBetweenJumps = .8f;

        [Header("Feedbacks/Cheese Appear")]
        [SerializeField] private GameObject _cheesePrefab;
        [SerializeField] private Transform _cheeseParent;
        [SerializeField] private float _chesseSpawnDelay = .025f;
        
        [Header("Feedbacks/End")] [SerializeField]
        private float _waitTransitionDuration;


        private List<SavedMouseMesh> _miceMeshes = new();

        public void Celebrate()
        {
            StartCoroutine(MakeAllMiceAppear());
        }

        private IEnumerator MakeAllMiceAppear()
        {
            yield return new WaitForSeconds(_waitBeforeAll);
            
            var saveSystem = MainGame.Instance.CollectiblesSaveSystem;
            var mice = MainGame.Instance.LevelCollectiblesData.Mice;

            if (_miceSlots.Length != mice.Count) Debug.LogError($"The number of MiceSlots ({_miceSlots.Length}) isn't equal to the number of mice in the level ({mice.Count})!");

            // Change camera and limit player play zone
            _enterZoneCamera.SetActive(true);
            _playerZoneAnimator.Play(_playZoneAnimation);
            //MainGame.Instance.PlayersManager.Inputs.SetInput(InputType.CUTSCENE);


            // Make every saved mouse appear on screen
            _collectiblesUI.UpdateMouseUI(-1, true);
            yield return new WaitForSeconds(_waitBeforeMiceAppear);
            for (int i = 0; i < mice.Count; i++)
            {
                yield return new WaitForSeconds(_waitTimeBetweenFalls);
                var data = mice[i];
                if (saveSystem.IsMouseUnlocked(data))
                {
                    var newMouse = Instantiate(data.Mesh, _miceSlots[i].position, Quaternion.identity);
                    _miceMeshes.Add(newMouse);
                    GameEventsManager.PlayEvent(_mouseLandFeedback, newMouse.gameObject);
                }

                _miceSlots[i].DOPunchScale(Vector3.one * .3f, .4f);
            }

            yield return new WaitForSeconds(_waitTimeBeforeWave);
            
            // Make all mice wave
            foreach (var mouse in _miceMeshes)
            {
                mouse.Animator.SetBool(_waveBoolName, true);
            }

            GameEventsManager.PlayEvent(_waveStartFeedback, gameObject);

            yield return new WaitForSeconds(_waveTime);
            // _launchCamera.SetActive(true);
            // yield return new WaitForSeconds(_afterLaunchCameraWaitTime);
            //
            //
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

            for (int i = 0; i < MainGame.Instance.CollectiblesManager.CheeseCollectiblesManager.Cheeses; i++)
            {
                Instantiate(_cheesePrefab, _cheeseParent);
                yield return new WaitForSeconds(_chesseSpawnDelay);
            }

            yield return new WaitForSeconds(_waitTransitionDuration);

            // Return to hub
            _sceneSwitch.SwitchScene();
        }
    }
}