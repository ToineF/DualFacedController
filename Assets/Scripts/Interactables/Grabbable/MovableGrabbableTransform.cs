using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables
{
    public class MovableGrabbableTransform : MonoBehaviour, IGrabbable
    {
        private Transform _originalParent;

        private void Start()
        {
            _originalParent = transform.parent;
        }

        public void OnGrab(CharacterHead characterHead)
        {
            ToggleFollow(characterHead);
        }

        public void OnUngrab(CharacterHead characterHead)
        {
            ToggleFollow(characterHead);
        }

        private void ToggleFollow(CharacterHead characterHead)
        {
            if (transform.parent == characterHead.GrabParent.transform)
                transform.SetParent(_originalParent);
            else 
                transform.SetParent(characterHead.GrabParent.transform);
        }

        public void AddForce(Vector3 force)
        {
            
        }
    }
}