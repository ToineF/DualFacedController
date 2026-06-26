using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.XInput;


namespace ControllerInputs
{
    public class ControllerSwitcher : MonoBehaviour
    {
        // Inputs
        public Action<ControllerType> OnControllerTypeChange;

        public ControllerType ControllerType
        {
            get => _controllerType;
            set
            {
                if (_controllerType != value) OnControllerTypeChange?.Invoke(value);
                _controllerType = value;
            }
        }

        [SerializeField] private InputActionAsset inputActions;
        //private Gamepad _gamepad;
        private ControllerType _controllerType;
        //public bool IsGamepad { get; private set; }

        // Propulsion
        private InputActionMap map;

        
        private void OnEnable()
        {
            map = inputActions.FindActionMap("UnityUI");

            foreach (var action in map.actions)
                action.performed += OnAction;

            map.Enable();
        }

        private void OnDisable()
        {
            foreach (var action in map.actions)
                action.performed -= OnAction;
        }
        
        private void OnAction(InputAction.CallbackContext ctx)
        {
            var device = ctx.control.device;

            if (device is Keyboard || device is Mouse)
                ControllerType = ControllerType.KEYBOARD;
            else if (device is XInputController)
                ControllerType = ControllerType.XBOX;
            else if (device is DualShockGamepad)
                ControllerType = ControllerType.PLAYSTATION;
            else if (device is SwitchProControllerHID)
                ControllerType = ControllerType.SWITCH;
        }

        // private void Awake()
        // {
        //     //if (Gamepad.all.Count > 0) _gamepad = Gamepad.current;
        //     //AssignControllerType();
        //     _playerInput.onControlsChanged += OnControlsChanged;
        //
        // }
        //
        // private void OnControlsChanged(UnityEngine.InputSystem.PlayerInput obj)
        // {
        //     if (obj.currentControlScheme == "Gamepad")
        //     {
        //         var gamepad = playerInput.devices.OfType<Gamepad>().FirstOrDefault();
        //         
        //         if (gamepad is XInputController)
        //             ControllerType = ControllerType.XBOX;
        //         else if (gamepad is DualShockGamepad)
        //             ControllerType = ControllerType.PLAYSTATION;
        //         else if (gamepad is SwitchProControllerHID)
        //             ControllerType = ControllerType.SWITCH;
        //     }
        //     else
        //     {
        //         if (_controllerType != ControllerType.KEYBOARD)
        //         {
        //             ControllerType = ControllerType.KEYBOARD;
        //         }
        //     }
        // }

        // private void OnEnable()
        // {
        //     InputUser.onChange += OnInputDeviceChange;
        // }
        //
        // private void OnDisable()
        // {
        //     InputUser.onChange -= OnInputDeviceChange;
        // }


        // private void LateUpdate()
        // {
        //     UpdateControllerType();
        // }
        //
        // private void UpdateControllerType()
        // {
        //     if (!IsGamepad)
        //     {
        //         ControllerType = ControllerType.KEYBOARD;
        //     }
        //     else
        //     {
        //         if (_gamepad == null)
        //         {
        //             ControllerType = ControllerType.KEYBOARD;
        //             return;
        //         }
        //
        //         AssignControllerType();
        //     }
        // }

        // private void AssignControllerType()
        // {
        //     if (Gamepad.all.Count <= 0) return;
        //     var gamepad = Gamepad.current;
        //
        //     if (gamepad is UnityEngine.InputSystem.XInput.XInputController)
        //     {
        //         ControllerType = ControllerType.XBOX;
        //     }
        //     else if (gamepad is UnityEngine.InputSystem.DualShock.DualShockGamepad)
        //     {
        //         ControllerType = ControllerType.PLAYSTATION;
        //     }
        //     else if (gamepad is UnityEngine.InputSystem.Switch.SwitchProControllerHID)
        //     {
        //         ControllerType = ControllerType.SWITCH;
        //     }
        //
        //     _gamepad = gamepad;
        // }
        //
        // private void OnInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
        // {
        //     IsGamepad = (device.name != "Keyboard" && device.name != "Mouse");
        // }
    }
}