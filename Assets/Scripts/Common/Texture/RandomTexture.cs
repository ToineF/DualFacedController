using UnityEngine;

namespace AntoineFoucault.Utilities.Texture
{
    public class RandomTexture : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderer;
        [SerializeField] private Material[] _materials;

        private void Start()
        {
            var material = _materials.GetRandomItem();
            foreach (var skinnedMeshRenderer in _skinnedMeshRenderer)
            {
                skinnedMeshRenderer.material = material;
            }
        }
    }
}