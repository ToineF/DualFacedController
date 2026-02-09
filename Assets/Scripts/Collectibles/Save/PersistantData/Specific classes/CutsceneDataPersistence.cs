using Cattac.Cutscenes;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class CutsceneDataPersistence : DataPersistence
    {
        [SerializeField] private Cutscene _cutscene;

        protected override void StartInternal()
        {
            _cutscene.OnEnd += Save;
        }

        private void OnDestroy()
        {
            _cutscene.OnEnd -= Save;
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _cutscene.HasPlayed = true;
                foreach (var skipEvent in _cutscene.OnSkipEvents)
                {
                    skipEvent?.Invoke();
                }
            }
        }
    }
}