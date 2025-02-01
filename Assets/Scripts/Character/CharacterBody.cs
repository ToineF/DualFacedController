using UnityEngine;

namespace Cattac.Character
{
    public class CharacterBody : MonoBehaviour
    {
        [field: Header("References")]
        [field: SerializeField]
        public JointToggler Head { get; set; }
        [field: SerializeField] public JointToggler Tail { get; set; }

        public void ToggleJointSeparation(bool separate, bool isHead)
        {
            var joint = isHead ? Head : Tail;
            if (separate) Separate(joint);
            else Reattach(joint);
        }

        private void Separate(JointToggler joint)
        {
            //joint.enabled = false;
        }

        private void Reattach(JointToggler joint)
        {
            //joint.enabled = true;
        }
    }
}