using Cattac.Interactables.ChaseSequence;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class ChasedCatDataPersistence : DataPersistence
    {
        [SerializeField] private ChasedCat _chasedCat;

        protected override void StartInternal()
        {
            _chasedCat.OnChaseEnd += Save;
        }

        private void OnDestroy()
        {
            _chasedCat.OnChaseEnd -= Save;
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _chasedCat.gameObject.SetActive(false);
            }
        }
    }
}