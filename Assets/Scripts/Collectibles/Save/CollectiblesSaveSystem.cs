using Cattac.Interactables;
using DG.Tweening;
using UnityEngine;
using Cattac.Interactables.MouseCollection;

namespace Cattac.Collectibles.Save
{
    public class CollectiblesSaveSystem : MonoBehaviour
    {
        [SerializeField] private CollectiblesManager _collectiblesManager;

        private string _currentCheeseKey = "CurrentCheeses";
        private string _totalCheeseKey = "TotalCheeses";
        private string _miceKey = "Mice_";

        private void Start()
        {
            _collectiblesManager.CheeseCollectiblesManager.OnCheeseGain += OnCheeseGain;
            _collectiblesManager.MouseCollectibleManager.OnMouseGet += OnMouseGet;
            
            InitializeCheeses();
            InitializeMice();
        }

        private void InitializeCheeses()
        {
            var cheeses = PlayerPrefs.GetInt(_currentCheeseKey);
            _collectiblesManager.CheeseCollectiblesManager.SetCheeses(cheeses);
        }

        private void OnCheeseGain(bool hasFeedbacks)
        {
            PlayerPrefs.SetInt(_currentCheeseKey, _collectiblesManager.CheeseCollectiblesManager.Cheeses);
        }

        /// <summary>
        /// Updates the total cheeses count and resets the current cheeses count
        /// </summary>
        public void OnTotalCheeseGain()
        {
            var totalCheeses = PlayerPrefs.GetInt(_totalCheeseKey);
            var currentCheeses = PlayerPrefs.GetInt(_currentCheeseKey);
            PlayerPrefs.SetInt(_totalCheeseKey, totalCheeses + currentCheeses);
            PlayerPrefs.SetInt(_currentCheeseKey, 0);

            // Update the collectiblesManagers
            InitializeCheeses();
        }

        /// <summary>
        /// Checks if a target amount of cheeses is superior or equals to the total cheeses count
        /// </summary>
        /// <param name="targetCheeses"></param>
        /// <returns></returns>
        public bool HasMoreThanTotalCheese(int targetCheeses)
        {
            var totalCheeses = PlayerPrefs.GetInt(_totalCheeseKey);
            return targetCheeses >= totalCheeses;
        }

        private void InitializeMice()
        {
            var miceInScene = MainGame.Instance.LevelCollectiblesData.Mice;
            var miceData = MainGame.Instance.CollectiblesFactory.Mice;
            for (int i = 0; i < miceInScene.Count; i++)
            {
                for (int j = 0; j < miceData.Length; j++)
                {
                    if (miceInScene[i].Data == miceData[j])
                    {
                        var isSaved = PlayerPrefs.GetInt(_miceKey + j) == 1;
                        if (isSaved)
                        {
                            miceInScene[i].gameObject.SetActive(false);
                            MainGame.Instance.LevelCollectiblesData.OnMouseGain?.Invoke(i, false);
                        }

                        break;
                    }
                }
                
            }
        }

        private void OnMouseGet(Cage cage, float time, Ease ease)
        {
            var miceData = MainGame.Instance.CollectiblesFactory.Mice;
            for (int i = 0; i < miceData.Length; i++)
            {
                if (cage.Data == miceData[i])
                {
                    PlayerPrefs.SetInt(_miceKey + i, 1);
                    return;
                }
            }
        }

        /// <summary>
        /// Is a SavedMouseData saved?
        /// </summary>
        /// <param name="targetData"></param>
        /// <returns></returns>
        public bool IsMouseUnlocked(SavedMouseData targetData)
        {
            var miceData = MainGame.Instance.CollectiblesFactory.Mice;
            for (int i = 0; i < miceData.Length; i++)
            {
                if (miceData[i] == targetData)
                {
                    var isSaved = PlayerPrefs.GetInt(_miceKey + i) == 1;
                    return isSaved;
                }
            }

            return false;
        }
    }
}