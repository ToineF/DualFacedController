using Cattac.Interactables;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class TimedPressurePlateGroupDataPersistence : DataPersistence
    {
        [SerializeField] private TimedPressurePlateGroup _pressurePlateGroup;
        [SerializeField] private Door _door;

        protected override void StartInternal()
        {
            _pressurePlateGroup.OnAllActivated.AddListener(Save);
        }

        private void OnDestroy()
        {
            _pressurePlateGroup.OnAllActivated.RemoveListener(Save);
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _pressurePlateGroup.AllActivated(false);
                _door.OpenNoFeedback();
            }
        }
    }
}