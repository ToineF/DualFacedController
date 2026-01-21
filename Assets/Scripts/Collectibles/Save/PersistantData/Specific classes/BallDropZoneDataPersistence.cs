using Cattac.Interactables;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class BallDropZoneDataPersistence : DataPersistence
    {
        [SerializeField] private BallDropZone _dropZone;

        protected override void StartInternal()
        {
            _dropZone.OnConditionMet.AddListener(Save);
        }

        private void OnDestroy()
        {
            _dropZone.OnConditionMet.RemoveListener(Save);
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _dropZone.BallEnter();
            }
        }
    }
}