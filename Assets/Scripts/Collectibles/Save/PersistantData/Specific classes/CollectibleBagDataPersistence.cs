using Cattac.Collectibles.Save;
using UnityEngine;


    public class CollectibleBagDataPersistence : DataPersistence
    {
        [SerializeField] private CollectibleBag _bag;

        protected override void StartInternal()
        {
            _bag.OnThrownEvent += Save;
        }

        private void OnDestroy()
        {
            _bag.OnThrownEvent -= Save;
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _bag.gameObject.SetActive(false);
            }
        }
    }