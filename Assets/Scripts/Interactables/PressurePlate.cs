using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class PressurePlate : BoxTrigger
    {
        public UnityEvent OnTriggerEnterEvent  => _onTriggerEnter;
        public UnityEvent OnTriggerExitEvent => _onTriggerExit;
        
        [SerializeField] private UnityEvent _onTriggerEnter;
        [SerializeField] private UnityEvent _onTriggerExit;
        
        private HashSet<GameObject> _currentColliders = new();


        protected override void OnEnterTriggerInternal(Collider other)
        {
            var collidersCount = _currentColliders.Count;
            _currentColliders.Add(other.gameObject);
            UpdatePressure(collidersCount);
        }

        protected override void OnExitTriggerInternal(Collider other)
        {
            var collidersCount = _currentColliders.Count;
            _currentColliders.Remove(other.gameObject);
            UpdatePressure(collidersCount);
        }

        private void UpdatePressure(int lastCount)
        {
            var currentCount = _currentColliders.Count;

            if (lastCount == 0 && currentCount > 0)
            {
                _onTriggerEnter.Invoke();
            }
            else if (lastCount > 0 && currentCount == 0 )
            {
                _onTriggerExit.Invoke();
            }
        }
    }
}