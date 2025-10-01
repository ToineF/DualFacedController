using System;

namespace Cattac.Collectibles
{
    public class CheeseCollectibleManager
    {
        public Action OnCheeseGain;
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