using System;
using Cattac.Interactables.MouseCollection;

namespace Cattac.Collectibles
{
    public class MouseCollectibleManager
    {
        public Action<SavedMouseData, float, DG.Tweening.Ease> OnMouseGet { get; set; }
        public Action OnMouseHide { get; set; }
    }
}