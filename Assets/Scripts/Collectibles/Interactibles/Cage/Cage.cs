using System;
using UnityEngine;

namespace Cattac.Interactables
{
    public class Cage : MonoBehaviour
    {
        public Action OnGetCage;
        public void GetCage()
        {
            OnGetCage?.Invoke();
            MainGame.Instance.LevelCollectiblesData.SaveMouse(this);
        }
    }
}