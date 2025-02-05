using System;
using UnityEngine;

namespace Cattac.Character
{
    [CreateAssetMenu(fileName = "Body", menuName = "Cattac/BodyData")]
    public class CharacterBodyData : ScriptableObject
    {
        [field:Header("Rigidbody")]
        [field:SerializeField] public RigidbodyData SeparatedMouseRigidbody { get; private set; }
        [field:SerializeField] public RigidbodyData AttachedMouseRigidbody { get; private set; }
        [field:SerializeField] public RigidbodyData BodyPartRigidbody { get; private set; }
        
        [field:Header("Joint")]
        [field:SerializeField] public float Spring { get; private set; }
        [field:SerializeField] public float Damper { get; private set; }
        [field:SerializeField] public float MaxDistance { get; private set; }
        [field:SerializeField] public float Tolerance { get; private set; }
    }

    [Serializable]
    public class RigidbodyData
    {
        [field:SerializeField] public float Mass { get; private set; }
        [field:SerializeField] public float LinearDrag { get; private set; }
        [field:SerializeField] public float AngularDrag { get; private set; }
    }
}