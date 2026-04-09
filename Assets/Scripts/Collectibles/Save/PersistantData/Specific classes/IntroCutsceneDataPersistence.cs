using Cattac.Cutscenes;
using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Collectibles.Save
{
    public class IntroCutsceneDataPersistence : DataPersistence
    {
        [SerializeField] private IntroCutscene _cutscene;

        protected override void StartInternal()
        {
            _cutscene.OnAllConnected += Save;
        }

        private void OnDestroy()
        {
            _cutscene.OnAllConnected -= Save;
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _cutscene.AllAlreadyConnected();
            }
        }
    }
}