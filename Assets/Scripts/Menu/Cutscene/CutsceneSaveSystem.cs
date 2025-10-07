using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Character
{
    public class CutsceneSaveSystem : MonoBehaviour
    {
        private string _introCutsceneSeenKey = "IntroCutsceneSeen";

        private static CutsceneSaveSystem Instance;

        [SerializeField] private UnityEvent _onNeverSeen;
        [SerializeField] private UnityEvent _onAlreadySeen;
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
            if (alreadySeen) _onAlreadySeen?.Invoke();
            else _onNeverSeen?.Invoke();
        }
    }
}