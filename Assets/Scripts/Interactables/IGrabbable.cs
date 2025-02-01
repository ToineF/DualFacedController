using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables
{
    public interface IGrabbable
    {
        public void OnGrab(CharacterHead characterHead);
        public void OnUngrab(CharacterHead characterHead);
        public void AddForce(Vector3 force);
    }
}