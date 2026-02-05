using AntoineFoucault.Utilities;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Cattac.Interactables
{
    public class DanceMinigameCheeseThrow : MonoBehaviour
    {
        [SerializeField] private Dancefloor _dancefloor;
        [SerializeField] private GameObject _cheesePrefab;
        [SerializeField] private Animator[] _cheeseSources;
        [SerializeField] private Transform _center;
        [SerializeField] private Transform _cheeseParent;
        [SerializeField, MinMaxSlider(0f,40f)] private Vector2 _centerOffset;
        [SerializeField, MinMaxSlider(0.01f,10f)] private Vector2 _spawnIntervalStart;
        [SerializeField, MinMaxSlider(0.01f,10f)] private Vector2 _spawnIntervalEnd;
        [SerializeField] private float _cheeseMoveTime;
        [SerializeField] private Ease _cheeseMoveEase;
        [SerializeField] private float _totalMinigameTime;
        [SerializeField] private GameObject[] _minigameEndActivate;
        [SerializeField] private GameObject[] _minigameEndDeactivate;

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
                SpawnCheese();
            }
        }

        private void SpawnCheese()
        {
            var spawnInterval = Vector2.Lerp(_spawnIntervalStart, _spawnIntervalEnd,
                _totalMinigameTimer / _totalMinigameTime);
            _spawnTimer = UnityEngine.Random.Range(spawnInterval.x, spawnInterval.y);
            var source = _cheeseSources.GetRandomItem();
            var cheese = Instantiate(_cheesePrefab, source.transform.position, source.transform.rotation);
            var direction = _center.position - source.transform.position;
            var centerOffset = UnityEngine.Random.Range(_centerOffset.x, _centerOffset.y);
            source.Play("Throw");
            cheese.transform.DOMove(_center.position - direction.normalized * centerOffset, _cheeseMoveTime).SetEase(_cheeseMoveEase).OnComplete(() => cheese.transform.SetParent(_cheeseParent, true));
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