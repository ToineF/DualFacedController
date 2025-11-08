using UnityEngine;

namespace Cattac.Collectibles.Hub
{
    /// <summary>
    /// A reward for having enough cheese in the hub
    /// </summary>
    public class HubCheeseReward : MonoBehaviour
    {
        [field:SerializeField] public int TargetAmount { get; private set; }
        [field:SerializeField] public GameObject ObjectToActivate { get; private set; }
    }
}