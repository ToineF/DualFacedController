using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cattac.Level
{
    public class LevelCountDebug : MonoBehaviour
    {
        public static LevelCountDebug Instance;

        public int CurrentLevel => _currentLevel;
        private int _currentLevel;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                transform.SetParent(null);
                DontDestroyOnLoad(this);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1)) PreviousLevel();
            if (Input.GetKeyDown(KeyCode.F2)) ReloadLevel();
            if (Input.GetKeyDown(KeyCode.F3)) NextLevel();
        }

        public void NextLevel()
        {
            _currentLevel++;
            ReloadLevel();
        }

        public void PreviousLevel()
        {
            _currentLevel--;
            ReloadLevel();
        }

        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
