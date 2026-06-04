using AntoineFoucault.Utilities;
using UnityEngine;

namespace Cattac.Interactables
{
    public class DanceMinigameBallLauncher : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private DanceMinigameButtonsPress _minigame;
        [SerializeField] private Rigidbody _ballPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _center;
        
        [Header("Parameters")]
        [SerializeField] private int _ballsToSpawn;
        [SerializeField] private float _throwForce;
        [SerializeField] private float _randomForce;
        [SerializeField] private int _firstRoundToSpawn = 1;
        [SerializeField] private bool _spawnOnLose;

        private int _roundCount = 0;
        
        private void Start()
        {
            _minigame.OnWinRound += AddBalls;
            if (_spawnOnLose) _minigame.OnLoseRound += AddBalls;
        }

        private void OnDestroy()
        {
            _minigame.OnWinRound -= AddBalls;
            if (_spawnOnLose) _minigame.OnLoseRound -= AddBalls;
        }

        private void AddBalls()
        {
            _roundCount++;
            if (_roundCount < _firstRoundToSpawn) return;
            
            for (int i = 0; i < _ballsToSpawn; i++)
            {
                var spawnPoint = _spawnPoints.GetRandomItem();
                Rigidbody rb = Instantiate(_ballPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                rb.AddForce((_center.position - spawnPoint.transform.position).normalized * _throwForce + Random.insideUnitSphere * _randomForce);
            }
        }
    }
}