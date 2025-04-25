using UnityEngine;

namespace Cattac.Interactables.NPC
{
    public class NPC_Eatable : MonoBehaviour, IDetectable
    {
        public Cat_State OnDetect(Cat_StateManager catStateManager)
        {
            return catStateManager.CatMoveToFish;
        }
    }
}