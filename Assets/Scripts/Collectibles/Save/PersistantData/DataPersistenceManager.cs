using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cattac.Collectibles.Save
{
    /// <summary>
    /// Generic save system manager
    /// </summary>
    public class DataPersistenceManager : MonoBehaviour
    {
        private static DataPersistenceManager _instance;

        private static Scene _currentScene;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += Load;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= Load;
        }
        private void Load(Scene scene,  LoadSceneMode mode)
        {
            _currentScene = scene;
        }

        #if UNITY_EDITOR
        /// <summary>
        /// Editor-only method to bake all 'Index'es of 'DataPersistence's
        /// </summary>
        [Button]
        private void BakeData()
        {
            var dataList = FindObjectsByType<DataPersistence>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            for (int i = 0; i < dataList.Length; i++)
            {
                var data = dataList[i];
                data.SetIndex(i);
                EditorUtility.SetDirty(data);
            }
            Debug.Log($"Persistent data baked in {SceneManager.GetActiveScene().name}");
        }
        #endif

        public static void SetID(int index, int value)
        {
            PlayerPrefs.SetInt(_currentScene.name + index, value);
        }
        
        public static void SetID(int index, bool value)
        {
            SetID(index, value ? 1 : 0);
        }

        public static int GetID(int index)
        {
            return PlayerPrefs.GetInt(_currentScene.name + index, -1);
        }
    }
}