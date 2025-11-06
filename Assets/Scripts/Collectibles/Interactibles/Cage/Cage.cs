using System;
using Cattac.Interactables.MouseCollection;
using UnityEngine;

namespace Cattac.Interactables
{
    public class Cage : MonoBehaviour
    {
        public Action OnGetCage;
        
        [field:SerializeField] public SavedMouseData Data { get; private set; }
        
        public void GetCage()
        {
            OnGetCage?.Invoke();
            MainGame.Instance.LevelCollectiblesData.SaveMouse(this);
        }
    }
}