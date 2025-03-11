using Cattac.Character.Ability;
using UnityEngine;

namespace Cattac.Interactables
{
    public class Torch : BoxTrigger
    {
        [SerializeField] private GameObject _vfx;
        
        protected override void OnEnterTriggerInternal(Collider other)
        {
            if (other.TryGetComponent(out FireWater water) == false) return;
            
            _vfx.SetActive(water.IsFire);
        }
    }
}