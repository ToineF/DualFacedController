using System.Collections.Generic;
using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables
{
    public class MovableGrabbableJoint : MonoBehaviour, IGrabbable
    {
        //[SerializeField] private Rigidbody _selfRigidbody;
        //[SerializeField] private Rigidbody _rigidbodyToApproach;
        [SerializeField] private Rigidbody _jointObject;
        private Dictionary<GameObject, Joint> _joints = new Dictionary<GameObject, Joint>();

        public void OnGrab(CharacterHead characterHead)
        {
            var newJoint = _jointObject.gameObject.AddComponent<HingeJoint>();
            newJoint.connectedBody = characterHead.CurrentRigidbody;
            _joints[characterHead.gameObject] = newJoint;

            //_joint.autoConfigureConnectedAnchor = false;
            //_joint.connectedAnchor = Vector3.back * 2f;
        }

        public void OnUngrab(CharacterHead characterHead)
        {
            if (_joints.TryGetValue(characterHead.gameObject, out var joint) == false) return;
            Destroy(joint);
            _joints.Remove(characterHead.gameObject);
        }

        public void AddForce(Vector3 force)
        {
            //.AddForce(force, ForceMode.Impulse);
        }
    }
}