using Cattac.Interactables.Collectibles;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class CheeseDataPersistence : DataPersistence
    {
        [SerializeField] private CheeseCollectible _collectible;

        protected override void StartInternal()
        {
            _collectible.OnPickUpEvent += Save;
        }

        private void OnDestroy()
        {
            _collectible.OnPickUpEvent -= Save;
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _collectible.gameObject.SetActive(false);
            }
        }
    }
}