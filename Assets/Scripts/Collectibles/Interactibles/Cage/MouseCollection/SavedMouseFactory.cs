using UnityEngine;

namespace Cattac.Interactables.MouseCollection
{
    /// <summary>
    /// A list of all the mice in all the levels in the game
    /// </summary>
    [CreateAssetMenu(menuName = "Cattac/SavedMouse/Factory")]
    public class SavedMouseFactory : ScriptableObject
    {
        [field:SerializeField] public SavedMouseData[] Mice { get; private set; } 
    }
}