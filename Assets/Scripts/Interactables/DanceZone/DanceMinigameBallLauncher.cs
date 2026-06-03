using AntoineFoucault.Utilities;
using UnityEngine;

namespace Cattac.Interactables
{
    public class DanceMinigameBallLauncher : MonoBehaviour
    {
        [SerializeField] private DanceMinigameButtonsPress _minigame;
        [SerializeField] private Rigidbody _ballPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _center;
        [SerializeField] private int _ballsToSpawn;
        [SerializeField] private float _throwForce;
        [SerializeField] private float _randomForce;
        [SerializeField] private int _firstRoundToSpawn = 1;

        private int _winCount = 0;
        
        private void Start()
        {
            _minigame.OnWinRound += OnWinRound;
        }

        private void OnDestroy()
        {
            _minigame.OnWinRound -= OnWinRound;
        }

        private void OnWinRound()
        {
            _winCount++;
            if (_winCount < _firstRoundToSpawn) return;
            
            for (int i = 0; i < _ballsToSpawn; i++)
            {
                var spawnPoint = _spawnPoints.GetRandomItem();
                Rigidbody rb = Instantiate(_ballPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                rb.AddForce((_center.position - spawnPoint.transform.position).normalized * _throwForce + Random.insideUnitSphere * _randomForce);
            }
        }
    }
}