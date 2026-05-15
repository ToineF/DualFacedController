using System;

namespace Cattac.Collectibles
{
    public class CheeseCollectibleManager
    {
        public Action<bool> OnCheeseGain { get; set; }
        public int Cheeses { get; private set; }
        public int MaxCheeses { get; private set; } = 350;
        
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