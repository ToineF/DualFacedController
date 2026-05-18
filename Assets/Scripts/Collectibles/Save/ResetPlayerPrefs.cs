using NaughtyAttributes;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class ResetPlayerPrefs : MonoBehaviour
    {
        [SerializeField] private bool _resetOnStart;
        
        private static ResetPlayerPrefs _instance;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                transform.SetParent(null);
                DontDestroyOnLoad(this);
                
                //#if UNITY_EDITOR
                ResetPrefs();
                //#endif
            }
            else if (_instance != this)
            {
                if (_resetOnStart) ResetPrefs();
                Destroy(gameObject);
            }
        }

        [Button]
        public void ResetPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}