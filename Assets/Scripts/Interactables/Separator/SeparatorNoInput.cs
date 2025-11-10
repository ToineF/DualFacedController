using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables
{
    /// <summary>
    /// Separates a CharacterHead from its body, without requiering an input
    /// </summary>
    public class SeparatorNoInput : MonoBehaviour
    {
        [SerializeField] private bool _separate;
        [SerializeField] private bool _makeKinematic = false;
        private CharacterHead _currentHead; // used only if (_makeKinematic)
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterHead characterHead) == false) return;
            
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
        }

        private void Reconnect()
        {
            _currentHead.OnConnect -= Reconnect;
            _currentHead = null;
        }
    }
}