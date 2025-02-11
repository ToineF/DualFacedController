using UnityEngine;

namespace Cattac.Character
{
    public class CharacterBody : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private CharacterBodyData _bodyData;
        
        [Header("References")]
        [SerializeField] private Rigidbody[] _soloMiceRigidbodies;
        [SerializeField] private Rigidbody[] _attachedMiceRigidbodies;
        [SerializeField] private SpringJoint[] _bodyPartsRigidbodies;

        public void SetBodyData()
        {
            for (int i = 0; i < _soloMiceRigidbodies.Length; i++)
            {
                SetRbData(_soloMiceRigidbodies[i], _bodyData.SeparatedMouseRigidbody);
                SetRbData(_attachedMiceRigidbodies[i], _bodyData.AttachedMouseRigidbody);
            }

            foreach (var joint in _bodyPartsRigidbodies)
            {
                SetJointData(joint);
            }
            
            Debug.Log("Body data updated");
        }

        private void SetRbData(Rigidbody rb, RigidbodyData rbData)
        {
            rb.angularDamping = rbData.AngularDrag;
            rb.linearDamping = rbData.LinearDrag;
            rb.mass = rbData.Mass;
        }

        private void SetJointData(SpringJoint joint)
        {
            joint.spring = _bodyData.Spring;
            joint.damper = _bodyData.Damper;
            joint.maxDistance = _bodyData.MaxDistance;
            joint.tolerance = _bodyData.Tolerance;
        }
    }
}