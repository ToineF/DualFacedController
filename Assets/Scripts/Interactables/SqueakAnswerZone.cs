using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables
{
    public class SqueakAnswerZone : MonoBehaviour, IGrabbable
    {
        [SerializeField] private AudioClip _meowSound;
        public void OnGrab(CharacterHead characterHead)
        {
            characterHead.CurrentGrabbable = null;
            AudioManager.Instance.PlayClip(_meowSound);
        }

        public void OnUngrab(CharacterHead characterHead)
        {
            
        }

        public void AddForce(Vector3 force)
        {
            
        }
    }
}