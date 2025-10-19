using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables
{
    public class Separator : MonoBehaviour, IGrabbable
    {
        [SerializeField] private bool _separate;
        [SerializeField] private bool _makeKinematic = false;
        private CharacterHead _currentHead; // used only if (_makeKinematic)

        public void OnGrab(CharacterHead characterHead)
        {
            if (characterHead.IsSeparated == _separate) return;
            if (_makeKinematic && _currentHead) return; // Ensure no two heads are attached at the same time if kinematic

            characterHead.CurrentGrabbable = null;
            if (_makeKinematic)
            {
                characterHead.TogetherRigidbody.isKinematic = _separate;
                _currentHead = characterHead;
                characterHead.OnConnect += Reconnect;
            }
            characterHead.SetSeparation(_separate);
            OnGrabInternal(characterHead);
        }

        private void Reconnect()
        {
            _currentHead.OnConnect -= Reconnect;
            _currentHead = null;
        }

        protected virtual void OnGrabInternal(CharacterHead characterHead)
        {
        }

        public void OnUngrab(CharacterHead characterHead)
        {
            OnUngrabInternal(characterHead);
        }

        protected virtual void OnUngrabInternal(CharacterHead characterHead)
        {
        }

        public void AddForce(Vector3 force)
        {
        }
    }
}