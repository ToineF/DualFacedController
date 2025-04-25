using UnityEngine;

namespace Cattac.Interactables
{
    public class Cage : MonoBehaviour
    {
        public void GetCage()
        {
            MainGame.Instance.LevelCollectiblesData.SaveMouse(this);
        }
    }
}