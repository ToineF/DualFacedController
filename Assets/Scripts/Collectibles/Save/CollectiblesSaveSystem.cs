using Cattac.Interactables;
using DG.Tweening;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class CollectiblesSaveSystem : MonoBehaviour
    {
        [SerializeField] private CollectiblesManager _collectiblesManager;

        private string _cheeseKey = "Cheeses";

        private void Start()
        {
            _collectiblesManager.CheeseCollectiblesManager.OnCheeseGain += OnCheeseGain;
            _collectiblesManager.MouseCollectibleManager.OnMouseGet += OnMouseGet;
            
            var cheeses = PlayerPrefs.GetInt(_cheeseKey);
            _collectiblesManager.CheeseCollectiblesManager.SetCheeses(cheeses);
        }
        
        private void OnCheeseGain()
        {
            var currentCheeses = PlayerPrefs.GetInt(_cheeseKey);
            PlayerPrefs.SetInt(_cheeseKey, currentCheeses + 1);
        }
        
        private void OnMouseGet(Cage cage, float time, Ease ease)
        {
            
        }
    }
}