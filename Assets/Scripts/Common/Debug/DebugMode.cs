using UnityEngine;
using UnityEngine.SceneManagement;


public class DebugMode : MonoBehaviour
{
    private void Awake()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(this);
    }

//#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) // Previous Scene
        {
            if (SceneManager.GetActiveScene().buildIndex > 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if (Input.GetKeyDown(KeyCode.F2)) // Restart Scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.F3)) // Next Scene
        {
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

//#endif
}