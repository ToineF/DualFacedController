using System;

namespace Cattac.Collectibles
{
    public class CheeseCollectibleManager
    {
        public Action<bool> OnCheeseGain { get; set; }
        public int Cheeses { get; private set; }
        public int MaxCheeses { get; private set; }
        public int MaxMice { get; private set; }
        
        public void AddCheese()
        {
            Cheeses++;
            OnCheeseGain?.Invoke(true);
        }

        public void SetCheeses(int cheeses)
        {
            Cheeses = cheeses;
            OnCheeseGain?.Invoke(false);
        }
    }
}