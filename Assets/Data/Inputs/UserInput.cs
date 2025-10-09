using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance {get; private set;}

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
    public InputAction MoveInputReference { get; private set; }
    public InputAction GrabInputReference { get; private set; }
    public InputAction EmoteInputReference { get; private set; }
    
    public Vector2 MoveInput {get; private set;}
    public bool GrabInput {get; private set;}
    public bool GrabInputReleased { get; private set;}
    public bool GrabInputPressed { get; private set;}
    public Vector2 EmoteDirection {get; private set;}
    public PlayerInputReferences(InputAction moveInputReference, InputAction grabInputReference,
        InputAction emoteInputReference)
    {
        MoveInputReference = moveInputReference;
        GrabInputReference = grabInputReference;
        EmoteInputReference = emoteInputReference;
    }

    public void UpdateInputs()
    {
        MoveInput = MoveInputReference.ReadValue<Vector2>();
        GrabInput = GrabInputReference.IsPressed();
        GrabInputReleased = GrabInputReference.WasReleasedThisFrame();
        GrabInputPressed = GrabInputReference.WasPressedThisFrame();
        EmoteDirection = EmoteInputReference.ReadValue<Vector2>();
    }
}