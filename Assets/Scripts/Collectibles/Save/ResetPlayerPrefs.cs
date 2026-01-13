using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class ResetPlayerPrefs : MonoBehaviour
    {
        public static ResetPlayerPrefs Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                transform.SetParent(null);
                DontDestroyOnLoad(this);
                
                //#if UNITY_EDITOR
                ResetPrefs();
                //#endif
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void ResetPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}