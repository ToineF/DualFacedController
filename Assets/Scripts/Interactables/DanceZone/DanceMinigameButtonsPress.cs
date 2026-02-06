using System.Collections.Generic;
using System.Linq;
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

        [SerializeField, MinMaxSlider(0.01f, 10f)]
        private Vector2 _spawnIntervalStart;

        [SerializeField, MinMaxSlider(0.01f, 10f)]
        private Vector2 _spawnIntervalEnd;

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

            HighlightPressurePlates();
        }

        private void HighlightPressurePlates()
        {
            var spawnInterval = _spawnIntervalStart; //Vector2.Lerp(_spawnIntervalStart, _spawnIntervalEnd, _totalMinigameTimer / _totalMinigameTime);
            _spawnTimer = UnityEngine.Random.Range(spawnInterval.x, spawnInterval.y);

            foreach (var pressurePlate in _currentPressurePlates)
            {
                if (pressurePlate == null) continue;
                pressurePlate.OnTriggerEnterEvent.RemoveListener(OnPressurePlateEnter);
                pressurePlate.OnTriggerExitEvent.RemoveListener(OnPressurePlateExit);
            }

            _currentPressurePlates[0] = _pressurePlates.GetRandomItem();
            _currentPressurePlates[1] =
                _pressurePlates.GetRandomItemExcluding(_pressurePlates.ToList().IndexOf(_currentPressurePlates[0]));
            _spotlights[0].transform.position = new Vector3(_currentPressurePlates[0].transform.position.x,
                _spotlights[0].transform.position.y, _currentPressurePlates[0].transform.position.z);
            _spotlights[1].transform.position = new Vector3(_currentPressurePlates[1].transform.position.x,
                _spotlights[1].transform.position.y, _currentPressurePlates[1].transform.position.z);
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

            Debug.Log("End minigame");
        }
    }
}