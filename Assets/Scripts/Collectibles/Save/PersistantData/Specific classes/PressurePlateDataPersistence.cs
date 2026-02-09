using Cattac.Interactables;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class PressurePlateDataPersistence : DataPersistence
    {
        [SerializeField] private PressurePlate _plate;

        protected override void StartInternal()
        {
            _plate.OnTriggerEnterEvent.AddListener(Save);
        }

        private void OnDestroy()
        {
            _plate.OnTriggerEnterEvent.RemoveListener(Save);
        }


        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _plate.OnTriggerEnterEvent.Invoke();
            }
        }
    }
}