using Cattac.Character;
using UnityEngine;
using UnityEngine.Audio;

namespace Cattac.Interactables
{
    public class SqueakAnswerZone : MonoBehaviour, IGrabbable
    {
        [SerializeField] private AudioResource _meowSound;
        public void OnGrab(CharacterHead characterHead)
        {
            characterHead.CurrentGrabbable = null;
            AudioManager.Instance.PlayResource(_meowSound);
        }

        public void OnUngrab(CharacterHead characterHead)
        {
            
        }

        public void AddForce(Vector3 force)
        {
            
        }
    }
}