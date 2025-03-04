using System;
using UnityEngine;

namespace Cattac.Collectibles
{
    public class CollectiblesManager : MonoBehaviour
    {
        public Action OnCheeseGain;
        public Action OnCheeseGainPreview;

        public int Cheeses { get; private set; }
        public int MaxCheeses { get; private set; }
        public int MaxMice { get; private set; }
        
        public void AddCheese()
        {
            Cheeses++;
            OnCheeseGain?.Invoke();
        }
    }
}