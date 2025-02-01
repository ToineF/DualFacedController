using UnityEngine;

namespace Cattac.Character
{
    [CreateAssetMenu(fileName = "Character", menuName = "Cattac/CharacterData")]
    public class CharacterHeadData : ScriptableObject
    {
        [field:Header("Movements")]
        [field:SerializeField] public float Speed { get; private set; }
        [field:SerializeField, Range(0, 1)] public float TurnLerp { get; private set; }
        
        [field:Header("Ground Detection")]
        [field:SerializeField] public float AdditionalGravity {get; private set;}
        [field:SerializeField] public float GravityDamper {get; private set;}
        [field:SerializeField] public float RestPositionFromGround {get; private set;}
        [field:SerializeField] public float GroundDetectionDistance {get; private set;}
        [field:SerializeField] public LayerMask GroundLayer {get; private set;}
        
        [field:Header("Grab")]
        [field:SerializeField] public float GrabRadius { get; private set; }
        [field:SerializeField] public LayerMask GrabLayerMask { get; private set; }
        
        [field:Header("Jump")]
        [field:SerializeField] public float JumpForce {get; private set;}
        [field:SerializeField] public float JumpTime {get; private set;}
        [field:SerializeField] public float JumpDecrease {get; private set;}
        [field:SerializeField] public float MinJumpsInterval {get; private set;}
        
        [field:Header("Snake")]
        [field: SerializeField] public float SnakeOffset { get; private set; }
        [field: SerializeField] public float SnakeFrequency { get; private set; }
        [field: SerializeField] public float SnakeAmplitude { get; private set; }
    }
}