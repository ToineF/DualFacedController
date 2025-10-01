using System;
using UnityEngine;

namespace Cattac.Collectibles
{
    /// <summary>
    /// Manages the entire collectibles system
    /// </summary>
    public class CollectiblesManager : MonoBehaviour
    {
        public CheeseCollectibleManager CheeseCollectiblesManager;
        public MouseCollectibleManager MouseCollectibleManager;
        [field:SerializeField] public Transform MouseCameraParent { get; private set; }

        private void Awake()
        {
            CheeseCollectiblesManager = new CheeseCollectibleManager();
            MouseCollectibleManager = new MouseCollectibleManager();
        }
    }
}