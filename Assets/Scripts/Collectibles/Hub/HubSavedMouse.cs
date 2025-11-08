using Cattac.Interactables.MouseCollection;
using UnityEngine;

namespace Cattac.Collectibles.Hub
{
    /// <summary>
    /// A saved mouse in the hub
    /// </summary>
    public class HubSavedMouse : MonoBehaviour
    {
        [field:SerializeField] public SavedMouseData Data { get; private set; }
        [field:SerializeField] public GameObject ObjectToActivate { get; private set; }
    }
}