using System;
using UnityEngine;

namespace Cattac.Collectibles
{
    public class CollectiblesManager : MonoBehaviour
    {
        public Action OnCheeseGain;
        public Action OnCheeseRemove;
        public Action OnCheeseGainPreview;
        public Action OnMouseGain;

        public int Cheeses { get; private set; }
        public int Mice { get; private set; }
        public int CurrentCheeses { get; private set; }

        public int MaxCheeses { get; private set; }
        public int MaxMice { get; private set; }
        

        public void AddCheese()
        {
            Cheeses++;
            CurrentCheeses++;
            OnCheeseGain?.Invoke();
        }

        public void RemoveCheese(int amount)
        {
            CurrentCheeses = Math.Max(0, CurrentCheeses - amount);
            OnCheeseRemove?.Invoke();
        }

        public void AddMouse()
        {
            Mice++;
            OnMouseGain?.Invoke();
        }

        public void AddCheesePreview()
        {
            OnCheeseGainPreview?.Invoke();
        }
    }
}