using System;
using System.Collections.Generic;
using Cattac.Interactables;
using UnityEngine;

namespace Cattac.Collectibles
{
    public class LevelCollectiblesData : MonoBehaviour
    {
        public Action<int> OnMouseGain;
        
        [field:SerializeField] public List<Cage> Mice { get; private set; }
        
        public void SaveMouse(Cage mouse)
        {
            var index = Mice.FindIndex(e => e == mouse);
            if (index == -1) return;

            OnMouseGain?.Invoke(index);
        }
    }
}