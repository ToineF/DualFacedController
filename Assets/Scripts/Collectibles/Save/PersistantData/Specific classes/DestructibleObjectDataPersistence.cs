using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class DestructibleObjectDataPersistence : DataPersistence
    {
        [SerializeField] private DestructibleObject _destructibleObject;

        protected override void StartInternal()
        {
            _destructibleObject.OnDestroyObject += Save;
        }

        private void OnDestroy()
        {
            _destructibleObject.OnDestroyObject -= Save;
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _destructibleObject.gameObject.SetActive(false);
            }
        }
    }
}