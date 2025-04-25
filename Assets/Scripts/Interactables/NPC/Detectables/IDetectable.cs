using UnityEngine;

namespace Cattac.Interactables.NPC
{
    public interface IDetectable
    {
        public GameObject gameObject { get; }
        public Cat_State OnDetect(Cat_StateManager catStateManager);
    }
}