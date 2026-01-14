using System.Collections.Generic;
using System.Threading.Tasks;
using Cattac.Interactables;
using Cattac.Interactables.Collectibles;
using DG.Tweening;
using UnityEngine;
using Cattac.Interactables.MouseCollection;

namespace Cattac.Collectibles.Save
{
    public class CollectiblesSaveSystem : MonoBehaviour
    {
        [SerializeField] private CollectiblesManager _collectiblesManager;

        private const string _currentCheeseKey = "CurrentCheeses";
        private const string _totalCheeseKey = "TotalCheeses";
        private const string _cheeseKey = "Mice_";
        private const string _miceKey = "Mice_";

        private Cage[] _cages;

        private async void Start()
        {
            _collectiblesManager.CheeseCollectiblesManager.OnCheeseGain += OnCheeseGain;
            _collectiblesManager.MouseCollectibleManager.OnMouseGet += OnMouseGet;

            await Task.Delay(1);
            
            InitializeCheeses();
            InitializeMice();
        }

        private void InitializeCheeses()
        {
            // Set current cheese number
            var cheesesAmount = PlayerPrefs.GetInt(_currentCheeseKey);
            _collectiblesManager.CheeseCollectiblesManager.SetCheeses(cheesesAmount);
            
            // Find all cheeses and deactivate them if already picked up
            //var cheesesInScene= GameObject.FindObjectsByType<CheeseCollectible>(FindObjectsSortMode.None);
            // for (int i = 0; i < cheesesInScene.Length; i++)
            // {
            //     for (int j = 0; j < miceData.Length; j++)
            //     {
            //         if (miceDataInScene[i] == miceData[j])
            //         {
            //             var isSaved = PlayerPrefs.GetInt(_miceKey + j) == 1;
            //             if (isSaved)
            //             {
            //                 foreach (var cage in _cages)
            //                 {
            //                     if (cage.Data == miceData[j])
            //                     {
            //                         cage.gameObject.SetActive(false);
            //                     }
            //                 }
            //
            //                 MainGame.Instance.LevelCollectiblesData.OnMouseGain?.Invoke(i, false);
            //             }
            //
            //             break;
            //         }
            //     }
            // }
            
            // Make more generic save system for doors, scenettes and other?
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
            _cages = GameObject.FindObjectsByType<Cage>(FindObjectsSortMode.None);
            var miceDataInScene = MainGame.Instance.LevelCollectiblesData.Mice;
            var miceData = MainGame.Instance.CollectiblesFactory.Mice;
            for (int i = 0; i < miceDataInScene.Count; i++)
            {
                for (int j = 0; j < miceData.Length; j++)
                {
                    if (miceDataInScene[i] == miceData[j])
                    {
                        var isSaved = PlayerPrefs.GetInt(_miceKey + j) == 1;
                        if (isSaved)
                        {
                            foreach (var cage in _cages)
                            {
                                if (cage.Data == miceData[j])
                                {
                                    cage.gameObject.SetActive(false);
                                }
                            }

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