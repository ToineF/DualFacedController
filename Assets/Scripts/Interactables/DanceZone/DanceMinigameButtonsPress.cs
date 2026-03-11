using System.Collections;
using AntoineFoucault.Utilities;
using FeedbacksEditor;
using FMOD;
using NaughtyAttributes;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Cattac.Interactables
{
    public class DanceMinigameButtonsPress : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Dancefloor _dancefloor;
        [SerializeField] private DanceMinigameScorePanel _scorePanel;
        [SerializeField] private PressurePlate[] _pressurePlates;
        [SerializeField] private GameObject[] _spotlights;
        [SerializeField] private GameObject _cameraZoom;
        [SerializeField] private MeshRenderer _timerRenderer;

        [Header("Parameters / Start")]
        [SerializeField] private int _maxWinCount;
        [SerializeField] private float _startGameWaitTime;
        [Header("Parameters / Round")]
        [SerializeField, MinMaxSlider(0.01f, 10f)] private Vector2 _spawnInterval;
        [SerializeField] private float _waitTimeBetweenRounds;
        [Header("Parameters / Camera Zoom")]
        [SerializeField] private bool _useCameraZoomEveryTime = false;
        [SerializeField] private float _beforeCameraZoomTime;
        [SerializeField] private float _beforeHighlightTime;
        [SerializeField] private float _zoomTime;
        
        [Header("Minigame End")]
        [SerializeField] private GameObject[] _minigameEndActivate;
        [SerializeField] private GameObject[] _minigameEndDeactivate;
        [SerializeField] private GameObject[] _minigameWinActivate;
        [SerializeField] private GameObject[] _minigameWinDeactivate;
        
        [Header("Feedbacks")]
        [SerializeField] private GameObject _feedbacksParent;
        [SerializeField] private GameEvent _highlightPressurePlatesEvent;
        [SerializeField] private GameEvent _roundWinImmediate;
        [SerializeField] private GameEvent _roundLoseImmediate;

        private float _spawnTimer;
        private float _currentMaxSpawnTimer;
        private bool _canUpdate;

        private PressurePlate[] _currentPressurePlates;
        private int _enteredPressurePlatesCount;

        private int _winCount;
        private int _loseCount;

        private void Awake()
        {
            _dancefloor.OnPartyStart += OnPartyStart;
            ChangeTimerColor(0);
        }

        private void OnPartyStart()
        {
            _canUpdate = true;
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
            ChangeTimerColor(Mathf.Min(1f, _spawnTimer / _currentMaxSpawnTimer));

            if (_spawnTimer < 0)
            {
                StartCoroutine(EndRound(false));
            }
        }
        
        private IEnumerator EndRound(bool win)
        {
            // Unsubscribe first
            _spawnTimer = float.MaxValue;
            _spotlights.SetAllActive(false);
            foreach (var pressurePlate in _currentPressurePlates)
            {
                if (pressurePlate == null) continue;
                pressurePlate.OnTriggerEnterEvent.RemoveListener(OnPressurePlateEnter);
                pressurePlate.OnTriggerExitEvent.RemoveListener(OnPressurePlateExit);
            }
            
            GameEventsManager.PlayEvent(win ? _roundWinImmediate : _roundLoseImmediate, _feedbacksParent);

            var firstTime = _winCount == 0 && _loseCount == 0;
            var lastTime = win ? _winCount + 1 >= _maxWinCount : _loseCount + 1 >= _maxWinCount;
            
            if (firstTime || lastTime || _useCameraZoomEveryTime)
            {
                yield return new WaitForSeconds(_beforeCameraZoomTime);
                _cameraZoom.SetActive(true);
                yield return new WaitForSeconds(_beforeHighlightTime);
            }
            
            SetHighlight(win);

            if (firstTime || lastTime || _useCameraZoomEveryTime)
            {
                yield return new WaitForSeconds(_zoomTime);
                _cameraZoom.SetActive(false);
            }

            yield return new WaitForSeconds(_waitTimeBetweenRounds);
            if (_canUpdate) HighlightPressurePlates();
        }

        private void SetHighlight(bool win)
        {
            if (win)
            {
                _winCount++;
                _scorePanel.SetHighlight(true, _winCount - 1);
                if (_winCount >= _maxWinCount) MinigameEnd(true);
            }
            else
            {
                _loseCount++;
                _scorePanel.SetHighlight(false, _loseCount - 1);
                if (_loseCount >= _maxWinCount) MinigameEnd(false);
            }
        }

        private void HighlightPressurePlates()
        {
            _currentMaxSpawnTimer = UnityEngine.Random.Range(_spawnInterval.x, _spawnInterval.y);
            _spawnTimer = _currentMaxSpawnTimer;

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
            _spotlights.SetAllActive(true);
            _spotlights[0].transform.position = new Vector3(_currentPressurePlates[0].transform.position.x,
                _spotlights[0].transform.position.y, _currentPressurePlates[0].transform.position.z);
            _spotlights[1].transform.position = new Vector3(_currentPressurePlates[1].transform.position.x,
                _spotlights[1].transform.position.y, _currentPressurePlates[1].transform.position.z);
            GameEventsManager.PlayEvent(_highlightPressurePlatesEvent, _feedbacksParent);

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
            if (_canUpdate == false) return;

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
            _canUpdate = false;
            _spotlights.SetAllActive(false);
            ChangeTimerColor(1);
            _minigameEndActivate.SetAllActive(true);
            if (win)
            {
                _minigameWinActivate.SetAllActive(true);
                _minigameWinDeactivate.SetAllActive(false);
                _scorePanel.Appear(false);
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