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
        
        [SerializeField, MinMaxSlider(0.01f,10f)] private Vector2 _spawnIntervalStart;
        [SerializeField, MinMaxSlider(0.01f,10f)] private Vector2 _spawnIntervalEnd;
        [SerializeField] private float _totalMinigameTime;
        [SerializeField] private GameObject[] _minigameEndActivate;
        [SerializeField] private GameObject[] _minigameEndDeactivate;
        [SerializeField] private GameEvent _highlightPressurePlatesEvent;

        private float _spawnTimer;
        private float _totalMinigameTimer;
        private bool _canUpdate;

        private void Awake()
        {
            _dancefloor.OnPartyStart += OnPartyStart;
        }

        private void OnPartyStart()
        {
            _canUpdate = true;
            _spotlights.SetAllActive(true);
        }

        private void Update()
        {
            if (_canUpdate == false) return;
            
            _spawnTimer -= Time.deltaTime;
            _totalMinigameTimer += Time.deltaTime;
            
            if (_totalMinigameTimer >= _totalMinigameTime)
            {
                MinigameEnd();
            }
            
            if (_spawnTimer < 0)
            {
                HighlightPressurePlates();
            }
        }

        private void HighlightPressurePlates()
        {
            var spawnInterval = Vector2.Lerp(_spawnIntervalStart, _spawnIntervalEnd,
                _totalMinigameTimer / _totalMinigameTime);
            _spawnTimer = UnityEngine.Random.Range(spawnInterval.x, spawnInterval.y);
            
            var pressurePlate = _pressurePlates.GetRandomItem();
            var pressurePlate2 = _pressurePlates.GetRandomItemExcluding(_pressurePlates.ToList().IndexOf(pressurePlate));
            _spotlights[0].transform.position = new Vector3(pressurePlate.transform.position.x, _spotlights[0].transform.position.y, pressurePlate.transform.position.z);
            _spotlights[1].transform.position = new Vector3(pressurePlate2.transform.position.x, _spotlights[1].transform.position.y, pressurePlate2.transform.position.z);
            GameEventsManager.PlayEvent(_highlightPressurePlatesEvent, gameObject);
        }

        private void MinigameEnd()
        {
            _canUpdate = false;
            foreach (var go in _minigameEndActivate)
            {
                go.SetActive(true);
            }
            foreach (var go in _minigameEndDeactivate)
            {
                go.SetActive(false);
            }
            Debug.Log("End minigame");
        }
    }
}