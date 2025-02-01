using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

namespace MaskTransitions
{
    public class CutoutMaskUI : Image
    {
        private Material cachedMaterial;

        public override Material materialForRendering
        {
            get
            {
                if (cachedMaterial == null)
                {
                    cachedMaterial = new Material(base.materialForRendering);
                    cachedMaterial.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
                }
                return cachedMaterial;
            }
        }

        protected override void OnDestroy()
        {
            if (cachedMaterial != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(cachedMaterial);
                }
                else
                {
                    DestroyImmediate(cachedMaterial);
                }
            }
            base.OnDestroy();
        }

    }
}

