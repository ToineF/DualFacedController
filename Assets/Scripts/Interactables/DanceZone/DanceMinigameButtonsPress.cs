using System.Collections;
using AntoineFoucault.Utilities;
using FeedbacksEditor;
using NaughtyAttributes;
using UnityEngine;

namespace Cattac.Interactables
{
    public class DanceMinigameButtonsPress : MonoBehaviour
    {
        [SerializeField] private Dancefloor _dancefloor;
        [SerializeField] private PressurePlate[] _pressurePlates;
        [SerializeField] private GameObject[] _spotlights;

        [SerializeField, MinMaxSlider(0.01f, 10f)] private Vector2 _spawnInterval;

        [SerializeField] private float _waitTimeBetweenRounds;

        [SerializeField] private MeshRenderer[] _winHighlights;
        [SerializeField] private MeshRenderer[] _loseHighlights;
        [SerializeField] private Material _winHighlightMaterial;
        [SerializeField] private GameEvent _highlightPressurePlatesEvent;

        [Header("Minigame End")] [SerializeField]
        private int _maxWinCount;

        [SerializeField] private GameObject[] _minigameEndActivate;
        [SerializeField] private GameObject[] _minigameEndDeactivate;

        private float _spawnTimer;
        private bool _canUpdate;

        private PressurePlate[] _currentPressurePlates;
        private int _enteredPressurePlatesCount;

        private int _winCount;
        private int _loseCount;

        private void Awake()
        {
            _dancefloor.OnPartyStart += OnPartyStart;
        }

        private void OnPartyStart()
        {
            _canUpdate = true;
            _currentPressurePlates = new PressurePlate[2];
            _spotlights.SetAllActive(true);
            HighlightPressurePlates();
        }

        private void Update()
        {
            if (_canUpdate == false) return;

            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer < 0)
            {
                EndRound(false);
            }
        }

        private void EndRound(bool win)
        {
            if (win)
            {
                _winCount++;
                _winHighlights[_winCount - 1].material = _winHighlightMaterial;
                if (_winCount >= _maxWinCount) MinigameEnd();
            }
            else
            {
                _loseCount++;
                _loseHighlights[_loseCount - 1].material = _winHighlightMaterial;
                if (_loseCount >= _maxWinCount) MinigameEnd();
            }

            StartCoroutine(WaitBeforeNextRound());
        }

        private IEnumerator WaitBeforeNextRound()
        {
            _spotlights.SetAllActive(false);

            foreach (var pressurePlate in _currentPressurePlates)
            {
                if (pressurePlate == null) continue;
                pressurePlate.OnTriggerEnterEvent.RemoveListener(OnPressurePlateEnter);
                pressurePlate.OnTriggerExitEvent.RemoveListener(OnPressurePlateExit);
            }
            
            yield return new WaitForSeconds(_waitTimeBetweenRounds);
            
            HighlightPressurePlates();
        }

        private void HighlightPressurePlates()
        {
            _spawnTimer = UnityEngine.Random.Range(_spawnInterval.x, _spawnInterval.y);

            PressurePlate plate1 = null;
            PressurePlate plate2 = null;
            while (plate1 == null || plate2 == null)
            {
                var newPlate = _pressurePlates.GetRandomItem();
                if (_currentPressurePlates[0] == newPlate || _currentPressurePlates[1] == newPlate || (plate1 != null && plate1 == newPlate)) continue;
                if (plate1 == null) plate1 = newPlate;
                else if  (plate2 == null) plate2 = newPlate;
            }
            
            _currentPressurePlates[0] = plate1;
            _currentPressurePlates[1] = plate2;
            _spotlights.SetAllActive(true);
            _spotlights[0].transform.position = new Vector3(_currentPressurePlates[0].transform.position.x, _spotlights[0].transform.position.y, _currentPressurePlates[0].transform.position.z);
            _spotlights[1].transform.position = new Vector3(_currentPressurePlates[1].transform.position.x, _spotlights[1].transform.position.y, _currentPressurePlates[1].transform.position.z);
            GameEventsManager.PlayEvent(_highlightPressurePlatesEvent, gameObject);

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
                EndRound(true);
            }
        }

        private void MinigameEnd()
        {
            _canUpdate = false;
            _minigameEndActivate.SetAllActive(true);
            _spotlights.SetAllActive(false);
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