
using UnityEngine;

namespace Interactables.Collectibles
{
    public abstract class Collectible<T> : SphereTrigger
    {
        protected T _owner;

        protected override void OnEnterTriggerInternal(Collider other)
        {
            if (other.TryGetComponent(out T component) == false) return;
            if (_owner != null) return; // Ensure the collectible is taken only once

            _owner = component;

            OnPickUp();
        }

        protected virtual void OnPickUp()
        {
            OnDeath();
        }

        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}