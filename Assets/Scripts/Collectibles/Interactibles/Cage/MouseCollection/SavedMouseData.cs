using UnityEngine;

namespace Cattac.Interactables.MouseCollection
{
    /// <summary>
    /// A data representing a single, unique saved mouse
    /// </summary>
    [CreateAssetMenu(menuName = "Cattac/SavedMouse/Mouse")]
    public class SavedMouseData : ScriptableObject
    {
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField] public SavedMouseMesh Mesh { get; private set; }
    }
}