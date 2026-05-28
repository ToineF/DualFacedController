using System;

namespace Cattac.Collectibles
{
    public class CheeseCollectibleManager
    {
        public Action<bool, bool> OnCheeseGain { get; set; }
        public int Cheeses1 { get; private set; }
        public int Cheeses2 { get; private set; }
        public int MaxCheeses { get; private set; } = 350;
        
        public void AddCheese(bool isLeftHead = false)
        {
            if (isLeftHead) Cheeses1++;
            else Cheeses2++;
            OnCheeseGain?.Invoke(true, isLeftHead);
        }

        public void SetCheeses(int cheeses)
        {
            Cheeses1 = cheeses;
            Cheeses2 = cheeses;
            OnCheeseGain?.Invoke(false, false);
        }
    }
}