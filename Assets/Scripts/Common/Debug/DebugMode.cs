using Cattac.Character;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DebugMode : MonoBehaviour
{
    private void Awake()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(this);
    }

    private bool _isUIVisible;

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
        
        if (Input.GetKeyDown(KeyCode.F4)) // Toggle Canvas
        {
            _isUIVisible = !_isUIVisible;
            var canvasArray = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
            foreach (var canvas in canvasArray)
            {
                canvas.enabled = _isUIVisible;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.F5)) // Fly
        {
            var heads = FindObjectsByType<CharacterHead>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            foreach (var head in heads)
            {
                head.CurrentRigidbody.AddForce(Vector3.up * 1000f, ForceMode.Impulse);
            }
        }
    }

//#endif
}