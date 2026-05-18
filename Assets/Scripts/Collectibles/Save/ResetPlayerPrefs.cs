using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class ResetPlayerPrefs : MonoBehaviour
    {
        private void Awake()
        {
            //#if UNITY_EDITOR
            ResetPrefs();
            //#endif
        }

        public void ResetPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}