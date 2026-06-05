using Cattac.Interactables;
using UnityEngine;


    public class DanceWorkshop : MonoBehaviour
    {
        [field:SerializeField] public int Priority { get; private set; }
        [field:SerializeField] public Transform PlayerStartPoint { get; private set; }
        [field:SerializeField, Tooltip("If left null, random pressure plates will be selected")] public PressurePlate[] PressurePlatesToPress { get; private set; }
        [field: SerializeField, Tooltip("If left to -1, takes the default value")] public float TimeToComplete { get; private set; } = -1f;


    }