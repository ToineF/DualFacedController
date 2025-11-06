using UnityEngine;

namespace Cattac.Interactables.MouseCollection
{
    /// <summary>
    /// A mesh of the saved mouse, with a mask
    /// </summary>
    public class SavedMouseMesh : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private GameObject _mask;
        
        private Transform _maskParent;

        private void Start()
        {
            _maskParent = _mask.transform.parent;
        }
    }
}