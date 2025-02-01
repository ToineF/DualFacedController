using Cattac.Character;
using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class ActivationButton : MonoBehaviour, IGrabbable
    {
        [SerializeField] private UnityEvent OnPressed;

        public void OnGrab(CharacterHead characterHead)
        {
            OnPressed?.Invoke();
            characterHead.CurrentGrabbable = null;
        }

        public void OnUngrab(CharacterHead characterHead)
        {

        }

        public void AddForce(Vector3 force)
        {

        }
    }
}