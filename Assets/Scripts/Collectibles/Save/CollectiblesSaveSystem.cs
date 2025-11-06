using Cattac.Interactables;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using System.Linq;

namespace Cattac.Collectibles.Save
{
    public class CollectiblesSaveSystem : MonoBehaviour
    {
        [SerializeField] private CollectiblesManager _collectiblesManager;

        private string _cheeseKey = "Cheeses";
        private string _miceKey = "Mice_";

        private IEnumerator Start()
        {
            _collectiblesManager.CheeseCollectiblesManager.OnCheeseGain += OnCheeseGain;
            _collectiblesManager.MouseCollectibleManager.OnMouseGet += OnMouseGet;

            yield return new WaitForEndOfFrame(); // Wait after all Start initializations are done

            InitializeCheeses();
            InitializeMice();
        }

        private void InitializeCheeses()
        {
            var cheeses = PlayerPrefs.GetInt(_cheeseKey);
            _collectiblesManager.CheeseCollectiblesManager.SetCheeses(cheeses);
        }

        private void OnCheeseGain(bool hasFeedbacks)
        {
            PlayerPrefs.SetInt(_cheeseKey, _collectiblesManager.CheeseCollectiblesManager.Cheeses);
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
    }
}