using UnityEngine;

namespace Cattac.Character
{
    public class CutsceneSaveSystem : MonoBehaviour
    {
        private string _introCutsceneSeenKey = "IntroCutsceneSeen";

        private static CutsceneSaveSystem Instance;

        [SerializeField] private GameObject[] _showOnNeverSeen;
        [SerializeField] private GameObject[] _showOnAlreadySeen;

        private void Start()
        {
            bool seen = PlayerPrefs.HasKey(_introCutsceneSeenKey) && PlayerPrefs.GetInt(_introCutsceneSeenKey) == 1;
            ShowThings(seen);
            if (seen == false)
            {
                PlayerPrefs.SetInt(_introCutsceneSeenKey, 1);
            }
        }

        private void ShowThings(bool alreadySeen)
        {
            foreach (var go in _showOnAlreadySeen)
            {
                go.SetActive(alreadySeen);
            }

            foreach (var go in _showOnNeverSeen)
            {
                go.SetActive(alreadySeen == false);
            }
        }
    }
}