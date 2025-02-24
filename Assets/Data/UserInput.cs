using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance;

    public Vector2 LeftMoveInput {get; private set;}
    public Vector2 RightMoveInput {get; private set;}
    public bool LeftGrabInput {get; private set;}
    public bool RightGrabInput {get; private set;}
    public bool LeftSeparationInput {get; private set;}
    public bool LeftGrabInputReleased { get; private set;}
    public bool LeftGrabInputPressed { get; private set;}
    public bool RightSeparationInput {get; private set;}
    public bool RightGrabInputReleased { get; private set;}
    public bool RightGrabInputPressed { get; private set;}

    [SerializeField] private PlayerInput _playerInput;

    private InputAction _leftMoveAction;
    private InputAction _rightMoveAction;
    private InputAction _leftGrabAction;
    private InputAction _rightGrabAction;
    private InputAction _leftSeparateAction;
    private InputAction _rightSeparateAction;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SetupInputActions();
    }

    private void Update()
    {
        UpdateInputs();
    }

    private void SetupInputActions()
    {
        _leftMoveAction = _playerInput.actions["MoveLeft"];
        _rightMoveAction = _playerInput.actions["MoveRight"];
        _leftGrabAction = _playerInput.actions["GrabLeft"];
        _rightGrabAction = _playerInput.actions["GrabRight"];
        _leftSeparateAction = _playerInput.actions["SeparateLeft"];
        _rightSeparateAction = _playerInput.actions["SeparateRight"];
    }
    private void UpdateInputs()
    {
        LeftMoveInput = _leftMoveAction.ReadValue<Vector2>();
        RightMoveInput = _rightMoveAction.ReadValue<Vector2>();
        // To get onPressed event use : _leftGrabAction.WasPressedThisFrame();
        // To get onReleased event use : _leftGrabAction.WasReleasedThisFrame();
        LeftGrabInput = _leftGrabAction.IsPressed();
        LeftGrabInputReleased = _leftGrabAction.WasReleasedThisFrame();
        LeftGrabInputPressed = _leftGrabAction.WasPressedThisFrame();
        RightGrabInput = _rightGrabAction.IsPressed();
        RightGrabInputReleased = _rightGrabAction.WasReleasedThisFrame();
        RightGrabInputPressed = _rightGrabAction.WasPressedThisFrame();
        LeftSeparationInput = _leftSeparateAction.WasPressedThisFrame();
        RightSeparationInput = _rightSeparateAction.WasPressedThisFrame();
    }
}
