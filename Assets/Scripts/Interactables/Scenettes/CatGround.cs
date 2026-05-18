using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class CatGround : MonoBehaviour
    {
        public static System.Action OnFallInternal { get; set; }
        public System.Action OnFall { get; set; }

        public void Fall()
        {
            OnFall.Invoke();
            OnFallInternal?.Invoke();
        }
    }
}