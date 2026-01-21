using Cattac.Interactables;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class DancefloorDataPersistence : DataPersistence
    {
        [SerializeField] private Dancefloor _dancefloor;

        protected override void StartInternal()
        {
            _dancefloor.OnPartyStart += Save;
        }

        private void OnDestroy()
        {
            _dancefloor.OnPartyStart -= Save;
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _dancefloor.StopAllCoroutines();
                _dancefloor.StartPartyImmediate(false);
            }
        }
    }
}