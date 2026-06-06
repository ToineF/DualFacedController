using System;
using System.Collections;
using AntoineFoucault.Utilities;
using Cattac.Character;
using FeedbacksEditor;
using NaughtyAttributes;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Cattac.Interactables
{
    public class DanceMinigameButtonsPress : MonoBehaviour
    {
        public System.Action OnWinRound;
        public System.Action OnLoseRound;

        [Header("References")] [SerializeField]
        private Dancefloor _dancefloor;

        [SerializeField] private DanceMinigameScorePanel _scorePanel;
        [SerializeField] private PressurePlate[] _pressurePlates;
        [SerializeField] private GameObject[] _fakePressurePlates;
        [SerializeField] private GameObject[] _spotlights;
        [SerializeField] private GameObject _cameraZoom;
        [SerializeField] private MeshRenderer _timerRenderer;
        [SerializeField] private DanceTimerFeedback _timerFeedback;

        [Header("Parameters / Start")] [SerializeField]
        private int _roundsCount = 7;

        [SerializeField] private float _startGameWaitTime;
        [SerializeField] private bool _startTimerAfterFirstPoint;

        [Header("LD")]
        [SerializeField] private Transform _workshopsParent;
        [SerializeField] private DanceWorkshop[] _danceWorkshopsPrefabs;
        [SerializeField] private CharacterManager _characterManager;
        [SerializeField] private float _minigameDeactivationDelay = 0.6f;
        [SerializeField] private ParticleSystem _vfxTransition;

        [Header("Parameters / Round")] [SerializeField, MinMaxSlider(0.01f, 10f)]
        private Vector2 _spawnInterval;

        [SerializeField] private float _waitTimeBetweenRounds;

        [Header("Parameters / Camera Zoom")] [SerializeField]
        private bool _useCameraZoomEveryTime = false;

        [SerializeField] private float _beforeCameraZoomTime;
        [SerializeField] private float _beforeHighlightTime;
        [SerializeField] private float _zoomTime;

        [Header("Minigame End")] [SerializeField]
        private GameObject[] _minigameEndActivate;

        [SerializeField] private GameObject[] _minigameEndDeactivate;
        [SerializeField] private GameObject[] _minigameWinActivate;
        [SerializeField] private GameObject[] _minigameWinDeactivate;
        [SerializeField] private GameObject[] _minigameLoseActivate;
        [SerializeField] private GameObject[] _minigamePerfectActivate;

        [Header("Feedbacks")] [SerializeField] private GameObject _feedbacksParent;
        [SerializeField] private GameEvent _highlightPressurePlatesEvent;
        [SerializeField] private GameEvent _roundWinImmediate;
        [SerializeField] private GameEvent _roundLoseImmediate;

        private float _spawnTimer;
        private float _currentMaxSpawnTimer;
        private bool _canUpdate;
        private bool _hasWon;

        private PressurePlate[] _currentPressurePlates;
        private int _enteredPressurePlatesCount;
        private DanceWorkshop[] _danceWorkshops;
        private DanceWorkshop _currentWorkshop;

        private int _winCount;
        private int _loseCount;

        private void Awake()
        {
            _dancefloor.OnPartyStart += OnPartyStart;
            ChangeTimerColor(0);
            _danceWorkshops = new DanceWorkshop[_danceWorkshopsPrefabs.Length];
            for (int i = 0; i < _danceWorkshops.Length; i++)
            {
                _danceWorkshops[i] = Instantiate(_danceWorkshopsPrefabs[i], _workshopsParent);
                _danceWorkshops[i].gameObject.SetActive(false);
            }
            _danceWorkshops.Shuffle();
            Array.Sort(_danceWorkshops, (a,b) => a.Priority - b.Priority);
        }

        private void OnPartyStart()
        {
            if (_startTimerAfterFirstPoint == false) _canUpdate = true;
            _currentPressurePlates = new PressurePlate[2];
            _scorePanel.Appear(true);
            _spawnTimer = float.MaxValue;
            _spotlights.SetAllActive(false);

            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(_startGameWaitTime);

            HighlightPressurePlates();
        }

        private void Update()
        {
            if (_canUpdate == false) return;

            _spawnTimer -= Time.deltaTime;
            var currentValue = _spawnTimer / _currentMaxSpawnTimer;
            ChangeTimerColor(Mathf.Min(1f, currentValue));
            _timerFeedback.UpdateFeedback(1 - currentValue);

            if (_spawnTimer < 0)
            {
                StartCoroutine(EndRound(false));
            }
        }

        private IEnumerator EndRound(bool win)
        {
            // Unsubscribe first
            _spawnTimer = float.MaxValue;
            foreach (var pressurePlate in _currentPressurePlates)
            {
                if (pressurePlate == null) continue;
                pressurePlate.OnTriggerEnterEvent.RemoveListener(OnPressurePlateEnter);
                pressurePlate.OnTriggerExitEvent.RemoveListener(OnPressurePlateExit);
            }
            GameEventsManager.PlayEvent(win ? _roundWinImmediate : _roundLoseImmediate, _feedbacksParent);

            yield return new WaitForSeconds(_minigameDeactivationDelay);

            // Hides buttons
            _spotlights.SetAllActive(false);
            _pressurePlates.SetAllActive(false);
            _fakePressurePlates.SetAllActive(true);
            if (_currentWorkshop != null)
            {
                _vfxTransition.Play();
                _currentWorkshop.gameObject.SetActive(false);
            }


            var firstTime = _winCount == 0 && _loseCount == 0;
            var lastTime = _winCount + _loseCount + 1 >= _roundsCount;

            // if (firstTime || lastTime || _useCameraZoomEveryTime)
            // {
            //     yield return new WaitForSeconds(_beforeCameraZoomTime);
            //     _cameraZoom.SetActive(true);
            //     yield return new WaitForSeconds(_beforeHighlightTime);
            // }

            SetHighlight(win);

            if (_canUpdate == false)
            {
                yield return new WaitForSeconds(_beforeHighlightTime);
                _timerRenderer.gameObject.SetActive(true);
            }

            // if (firstTime || lastTime || _useCameraZoomEveryTime)
            // {
            //     yield return new WaitForSeconds(_zoomTime);
            //     _cameraZoom.SetActive(false); // if ((win && lastTime) == false) 
            // }

            CheckWin();

            yield return new WaitForSeconds(_waitTimeBetweenRounds);

            _canUpdate = true;
            HighlightPressurePlates();
        }

        private void SetHighlight(bool win)
        {
            if (win)
            {
                _winCount++;
                _scorePanel.SetHighlight(true, _loseCount + _winCount - 1);
                OnWinRound?.Invoke();
            }
            else
            {
                _loseCount++;
                _scorePanel.SetHighlight(false, _winCount + _loseCount - 1);
                OnLoseRound?.Invoke();
            }
        }

        private void CheckWin()
        {
            if (_winCount + _loseCount >= _roundsCount) MinigameEnd(_winCount >= _loseCount);
        }

        private void HighlightPressurePlates()
        {
            var roundCount = _winCount + _loseCount;
            _currentWorkshop = _danceWorkshops[roundCount];
            _currentWorkshop.gameObject.SetActive(true);
            _vfxTransition.Play();

            if (!Mathf.Approximately(_currentWorkshop.TimeToComplete, -1))
                _currentMaxSpawnTimer = _currentWorkshop.TimeToComplete;
            else
                _currentMaxSpawnTimer = UnityEngine.Random.Range(_spawnInterval.x, _spawnInterval.y);
            _spawnTimer = _currentMaxSpawnTimer;


            if (_currentWorkshop.PressurePlatesToPress.Length < _currentPressurePlates.Length)
            {
                // Select random Pressure Plate
                PressurePlate plate1 = null;
                PressurePlate plate2 = null;
                while (plate1 == null || plate2 == null)
                {
                    var newPlate = _pressurePlates.GetRandomItem();
                    if (_currentPressurePlates[0] == newPlate || _currentPressurePlates[1] == newPlate ||
                        (plate1 != null && plate1 == newPlate)) continue;
                    if (plate1 == null) plate1 = newPlate;
                    else if (plate2 == null) plate2 = newPlate;
                }

                _currentPressurePlates[0] = plate1;
                _currentPressurePlates[1] = plate2;
            }
            else
            {
                // Get determined Pressure Plate
                _currentPressurePlates[0] = _currentWorkshop.PressurePlatesToPress[0];
                _currentPressurePlates[1] = _currentWorkshop.PressurePlatesToPress[1];
            }

            _currentPressurePlates[0].gameObject.SetActive(true);
            _currentPressurePlates[1].gameObject.SetActive(true);


            _spotlights.SetAllActive(true);
            _spotlights[0].transform.position = new Vector3(_currentPressurePlates[0].transform.position.x,
                _spotlights[0].transform.position.y, _currentPressurePlates[0].transform.position.z);
            _spotlights[1].transform.position = new Vector3(_currentPressurePlates[1].transform.position.x,
                _spotlights[1].transform.position.y, _currentPressurePlates[1].transform.position.z);
            GameEventsManager.PlayEvent(_highlightPressurePlatesEvent, _feedbacksParent);

            // for (int i = 0; i < _pressurePlates.Length; i++)
            // {
            //     var isHighlighted = _pressurePlates[i] == plate1 || _pressurePlates[i] == plate2;
            //
            //     _pressurePlates[i].gameObject.SetActive(isHighlighted);
            //     _fakePressurePlates[i].SetActive(isHighlighted == false);
            // }

            _enteredPressurePlatesCount = 0;
            foreach (var pressurePlate in _currentPressurePlates)
            {
                if (pressurePlate.IsPressed) _enteredPressurePlatesCount++;
            }

            foreach (var pressurePlate in _currentPressurePlates)
            {
                pressurePlate.OnTriggerEnterEvent.AddListener(OnPressurePlateEnter);
                pressurePlate.OnTriggerExitEvent.AddListener(OnPressurePlateExit);
            }
            
            // Teleport player
            if (_characterManager != null) _characterManager.TeleportPlayer(_currentWorkshop.PlayerStartPoint.position, false);
        }

        private void OnPressurePlateEnter()
        {
            _enteredPressurePlatesCount++;
            CheckCount();
        }

        private void OnPressurePlateExit()
        {
            _enteredPressurePlatesCount--;
            CheckCount();
        }

        private void CheckCount()
        {
            if (_hasWon) return;

            if (_enteredPressurePlatesCount == 2)
            {
                StartCoroutine(EndRound(true));
            }
        }

        private void ChangeTimerColor(float index)
        {
            _timerRenderer.sharedMaterial.SetFloat("_Progress", index);
        }

        private void MinigameEnd(bool win)
        {
            _hasWon = true;
            _canUpdate = false;
            StopAllCoroutines();
            _spotlights.SetAllActive(false);
            _currentWorkshop.gameObject.SetActive(false);
            ChangeTimerColor(1);
            _minigameEndActivate.SetAllActive(true);
            if (win)
            {
                _minigameWinActivate.SetAllActive(true);
                _minigameWinDeactivate.SetAllActive(false);
                if (_winCount >= _roundsCount) _minigamePerfectActivate.SetAllActive(true);
                _scorePanel.Appear(false);
            }
            else
            {
                _minigameLoseActivate.SetAllActive(true);
            }

            _minigameEndDeactivate.SetAllActive(false);

            foreach (var pressurePlate in _currentPressurePlates)
            {
                if (pressurePlate == null) continue;
                pressurePlate.OnTriggerEnterEvent.RemoveListener(OnPressurePlateEnter);
                pressurePlate.OnTriggerExitEvent.RemoveListener(OnPressurePlateExit);
            }

            Debug.Log("End minigame");
        }
    }
}