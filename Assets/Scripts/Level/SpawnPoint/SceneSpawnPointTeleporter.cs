using UnityEngine;

namespace Cattac.Level.SpawnPoint
{
    /// <summary>
    /// Teleports the player to the associated spawn point
    /// </summary>
    public class SceneSpawnPointTeleporter : MonoBehaviour
    {
        public static int CurrentSpawnPointIndex = -1;
        
        [SerializeField] private int _spawnPointIndex;
        [SerializeField] private Transform _spawnPoint;

        private void Start()
        {
            if (CurrentSpawnPointIndex == _spawnPointIndex)
            {
                CurrentSpawnPointIndex = -1;
                MainGame.Instance.PlayerController.transform.position = _spawnPoint.position;
            }
        }
    }
}