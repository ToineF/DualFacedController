using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class CatGroundDataPersistence : DataPersistence
    {
        [SerializeField] private CatGround _catGround;
        [SerializeField] private GameObject _gameObjectToHide;

        protected override void StartInternal()
        {
            _catGround.OnFall += Save;
        }

        private void OnDestroy()
        {
            _catGround.OnFall -= Save;
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