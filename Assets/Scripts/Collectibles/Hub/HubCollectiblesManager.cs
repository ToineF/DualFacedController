using Cattac.Collectibles.Save;
using Cattac.Interactables;
using NaughtyAttributes;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Cattac.Collectibles.Hub
{
    /// <summary>
    /// Handles all the communication between the collectibles data stored in PlayerPrefs and the hub elements
    /// </summary>
    public class HubCollectiblesManager : MonoBehaviour
    {
        [SerializeField] private HubCheeseReward[] _cheeses;
        [SerializeField] private HubSavedMouse[] _mice;

        private void Start()
        {
            var saveSystem = MainGame.Instance.CollectiblesSaveSystem;
            saveSystem.OnTotalCheeseGain();
            
            foreach (var cheese in _cheeses)
            {
                var active = saveSystem.HasMoreThanTotalCheese(cheese.TargetAmount);
                cheese.ObjectToActivate.SetActive(!active);
            }
            
            foreach (var mouse in _mice)
            {
                var active = saveSystem.IsMouseUnlocked(mouse.Data);
                mouse.ObjectToActivate.SetActive(active);
            }
        }

#if UNITY_EDITOR
        [Button("Find Collectibles in Scene")]
        public void AssignCollectibles()
        {
            _cheeses = GameObject.FindObjectsByType<HubCheeseReward>(FindObjectsSortMode.InstanceID).Reverse().ToArray();
            _mice = GameObject.FindObjectsByType<HubSavedMouse>(FindObjectsSortMode.InstanceID).Reverse().ToArray();
            EditorUtility.SetDirty(gameObject);
        }
#endif
    }
}