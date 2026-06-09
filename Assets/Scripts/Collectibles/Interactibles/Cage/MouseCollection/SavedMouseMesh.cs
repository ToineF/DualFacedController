using UnityEngine;

namespace Cattac.Interactables.MouseCollection
{
    /// <summary>
    /// A mesh of the saved mouse, with a mask
    /// </summary>
    public class SavedMouseMesh : MonoBehaviour
    {
        [field:SerializeField] public Animator Animator { get; private set; }
    }
}