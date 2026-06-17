using UnityEngine;

namespace AntoineFoucault.Utilities.Texture
{
    public class RandomTexture : MonoBehaviour
    {
        [SerializeField] private Renderer[] _renderer;
        [SerializeField] private Material[] _materials;
        [SerializeField] private int _materialIndex = 0;

        private void Start()
        {
            var material = _materials.GetRandomItem();
            foreach (var skinnedMeshRenderer in _renderer)
            {
                skinnedMeshRenderer.materials[_materialIndex] = material;
            }
        }
    }
}