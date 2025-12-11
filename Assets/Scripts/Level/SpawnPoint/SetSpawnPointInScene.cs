using UnityEngine;

namespace Cattac.Level.SpawnPoint
{
    /// <summary>
    /// Set the current spawn point index
    /// </summary>
    public class SetSpawnPointInScene : MonoBehaviour
    {
        [SerializeField] private int _spawnPointIndex;

        public void SetSpawnPoint()
        {
            SceneSpawnPointTeleporter.CurrentSpawnPointIndex = _spawnPointIndex;
        }
    }
}