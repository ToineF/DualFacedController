using UnityEngine;

namespace Cattac.Interactables.MouseCollection
{
    /// <summary>
    /// Activates a gameObject if the associated SavedMouseData is unlocked
    /// </summary>
    public class CollectedSavedMouse : MonoBehaviour
    {
        [SerializeField] private SavedMouseData _data;
    }
}