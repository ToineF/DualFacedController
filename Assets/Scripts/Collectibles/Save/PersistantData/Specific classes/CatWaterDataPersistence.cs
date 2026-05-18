using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class CatWaterDataPersistence : DataPersistence
    {
        [SerializeField] private CatWater _catWater;
        [SerializeField] private GameObject _gameObjectToHide;

        protected override void StartInternal()
        {
            _catWater.OnEnterWater += Save;
        }

        private void OnDestroy()
        {
            _catWater.OnEnterWater -= Save;
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _gameObjectToHide.gameObject.SetActive(false);
            }
        }
    }
}