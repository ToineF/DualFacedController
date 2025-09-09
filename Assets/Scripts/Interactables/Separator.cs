using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables
{
    public class Separator : MonoBehaviour, IGrabbable
    {
        [SerializeField] private bool _separate;
        [SerializeField] private bool _makeKinematic = false;

        public void OnGrab(CharacterHead characterHead)
        {
            if (characterHead.IsSeparated == _separate) return;

            characterHead.CurrentGrabbable = null;
            characterHead.SetSeparation(_separate);
            OnGrabInternal(characterHead);
	    if (_makeKinematic) characterHead.TogetherRigidbody.isKinematic = _separate;
        }
        
        protected virtual void OnGrabInternal(CharacterHead characterHead) { }

        public void OnUngrab(CharacterHead characterHead)
        {
            OnUngrabInternal(characterHead);
        }
        protected virtual void OnUngrabInternal(CharacterHead characterHead) { }

        public void AddForce(Vector3 force)
        {
        }
    }
}