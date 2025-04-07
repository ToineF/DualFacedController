using Cattac.Character.Ability;
using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class Torch : BoxTrigger
    {
        [Header("Torch")]
        [SerializeField] private GameObject _vfx;
        [SerializeField] private UnityEvent _onLitEvent;
        [SerializeField] private UnityEvent _onUnlitEvent;
        
        protected override void OnEnterTriggerInternal(Collider other)
        {
            if (other.TryGetComponent(out FireWater water) == false) return;
            
            _vfx.SetActive(water.IsFire);
            if (water.IsFire) _onLitEvent?.Invoke();
            else _onUnlitEvent?.Invoke();
        }
    }
}