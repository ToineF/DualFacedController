using Cattac.Interactables;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class TimedPressurePlateDataPersistence : DataPersistence
    {
        [SerializeField] private TimedPressurePlate _pressurePlate;

        protected override void StartInternal()
        {
            _pressurePlate.OnDeactived.AddListener(Save);
        }

        private void OnDestroy()
        {
            _pressurePlate.OnDeactived.RemoveListener(Save);
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _pressurePlate.Deactivate(false);
            }
        }
    }
}