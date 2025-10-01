using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables
{
    public class Eatable : MonoBehaviour, IGrabbable
    {
        [SerializeField] private GameObject _children;
        [SerializeField] private GameObject _objectToDestroy;

        public void OnGrab(CharacterHead characterHead)
        {
            characterHead.CurrentGrabbable = null;
            _children.SetActive(true);
            _children.transform.SetParent(_objectToDestroy.transform.parent);
            Destroy(_objectToDestroy);
        }

        public void OnUngrab(CharacterHead characterHead)
        {
        }

        public void AddForce(Vector3 force)
        {
        }
    }
}