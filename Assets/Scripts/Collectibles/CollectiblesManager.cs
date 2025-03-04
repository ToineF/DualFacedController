using System;
using UnityEngine;

namespace Cattac.Collectibles
{
    public class CollectiblesManager : MonoBehaviour
    {
        public Action OnCheeseGain;
        public Action OnCheeseGainPreview;
        public Action OnMouseGain;

        public int Cheeses { get; private set; }
        public int Mice { get; private set; }
        public int MaxCheeses { get; private set; }
        public int MaxMice { get; private set; }
        

        public void AddCheese()
        {
            Cheeses++;
            OnCheeseGain?.Invoke();
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