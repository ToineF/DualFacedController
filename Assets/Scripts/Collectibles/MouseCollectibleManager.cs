using System;
using Cattac.Interactables;

namespace Cattac.Collectibles
{
    public class MouseCollectibleManager
    {
        public Action<Cage, float, DG.Tweening.Ease> OnMouseGet;
        public Action<Cage> OnMouseHide;
    }
}