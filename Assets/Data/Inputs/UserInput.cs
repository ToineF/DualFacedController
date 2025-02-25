using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance;

    public PlayerInputReferences LeftHead { get; set; }
    public PlayerInputReferences RightHead { get; set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public PlayerInputReferences GetHead(bool isLeft)
    {
        return isLeft ? LeftHead : RightHead;
    }

    private void Update()
    {
        LeftHead?.UpdateInputs();
        RightHead?.UpdateInputs();
    }
}


[Serializable]
public class PlayerInputReferences
{
    [field:SerializeField] public InputAction MoveInputReference { get; private set; }
    [field:SerializeField] public InputAction GrabInputReference { get; private set; }
    [field:SerializeField] public InputAction SeparationInputReference { get; private set; }
    
    public Vector2 MoveInput {get; private set;}
    public bool GrabInput {get; private set;}
    public bool GrabInputReleased { get; private set;}
    public bool GrabInputPressed { get; private set;}
    public bool SeparationInput {get; private set;}

    public PlayerInputReferences(InputAction moveInputReference, InputAction grabInputReference,
        InputAction separationInputReference)
    {
        MoveInputReference = moveInputReference;
        GrabInputReference = grabInputReference;
        SeparationInputReference = separationInputReference;
    }

    public void UpdateInputs()
    {
        MoveInput = MoveInputReference.ReadValue<Vector2>();
        GrabInput = GrabInputReference.IsPressed();
        GrabInputReleased = GrabInputReference.WasReleasedThisFrame();
        GrabInputPressed = GrabInputReference.WasPressedThisFrame();
        SeparationInput = SeparationInputReference.IsPressed();
    }
}