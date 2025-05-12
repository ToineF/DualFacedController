using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaskTransitions
{
    public class SceneSwitch : MonoBehaviour
    {
        public string sceneToLoadName;
        public float totalTransitionTime;

        public void SwitchScene()
        {
            TransitionManager.Instance.LoadLevel(sceneToLoadName);
        }
        
        public void RestartScene()
        {
            TransitionManager.Instance.LoadLevel(SceneManager.GetActiveScene().name);
        }
        
        public void RestartSceneRaw()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void SwitchNextScene()
        {
            TransitionManager.Instance.LoadLevel(SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex + 1).name);
        }
        
        public void SwitchNextSceneRaw()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void PlayTransition()
        {
            TransitionManager.Instance.PlayTransition(totalTransitionTime);
        }

        void PlayStartOfTransition()
        {
            TransitionManager.Instance.PlayStartHalfTransition(totalTransitionTime / 2);
        }

        void PlayEndOfTransition()
        {
            TransitionManager.Instance.PlayEndHalfTransition(totalTransitionTime / 2);
        }

        /*#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                PlayStartOfTransition();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                PlayEndOfTransition();
            }
        }
        #endif*/
    }
}