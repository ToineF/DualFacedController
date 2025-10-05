using UnityEngine;

namespace Cattac.Collectibles
{
    /// <summary>
    /// Manages the entire collectibles system
    /// </summary>
    public class CollectiblesManager : MonoBehaviour
    {
        [field:SerializeField] public Transform MouseCameraParent { get; private set; }
        
        public CheeseCollectibleManager CheeseCollectiblesManager { get; private set; }
        public MouseCollectibleManager MouseCollectibleManager { get; private set; }

        private void Awake()
        {
            CheeseCollectiblesManager = new CheeseCollectibleManager();
            MouseCollectibleManager = new MouseCollectibleManager();
        }
    }
}