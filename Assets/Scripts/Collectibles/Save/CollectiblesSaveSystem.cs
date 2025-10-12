using Cattac.Interactables;
using DG.Tweening;
using UnityEngine;
using System.Collections;

namespace Cattac.Collectibles.Save
{
    public class CollectiblesSaveSystem : MonoBehaviour
    {
        [SerializeField] private CollectiblesManager _collectiblesManager;
        [SerializeField] private int _mouseIDOffset = 0;
        
        private string _cheeseKey = "Cheeses";
        private string _miceKey = "Mice_";

        private IEnumerator Start()
        {
	    _collectiblesManager.CheeseCollectiblesManager.OnCheeseGain += OnCheeseGain;
            _collectiblesManager.MouseCollectibleManager.OnMouseGet += OnMouseGet;

	    yield return new WaitForEndOfFrame();  // Wait after all Start initializations are done

            InitializeCheeses();
            InitializeMice();
        }

        private void InitializeCheeses()
        {
            var cheeses = PlayerPrefs.GetInt(_cheeseKey);
            _collectiblesManager.CheeseCollectiblesManager.SetCheeses(cheeses);
        }

        private void OnCheeseGain()
        {
            var currentCheeses = PlayerPrefs.GetInt(_cheeseKey);
            PlayerPrefs.SetInt(_cheeseKey, currentCheeses + 1);
        }

        private void InitializeMice()
        {
            var mice = MainGame.Instance.LevelCollectiblesData.Mice;
            for (int i = 0; i < mice.Count; i++)
            {
                var isSaved = PlayerPrefs.GetInt(_miceKey+(i+_mouseIDOffset)) == 1;
                if (isSaved)
                {
                    mice[i].gameObject.SetActive(false);
                    MainGame.Instance.LevelCollectiblesData.OnMouseGain?.Invoke(i, false);
                }
            }
        }
        
        private void OnMouseGet(Cage cage, float time, Ease ease)
        {
            var mice = MainGame.Instance.LevelCollectiblesData.Mice;
            for (int i = 0; i < mice.Count; i++)
            {
                if (cage == mice[i])
                {
                    PlayerPrefs.SetInt(_miceKey+(i+_mouseIDOffset), 1);
                    return;
                }
            }
        }
    }
}