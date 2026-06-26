using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Switch;
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

        [SerializeField] private InputActionAsset _inputActionAsset;

        private InputActionMap _actionMap;
        private ControllerType _controllerType;
        
        private const string _actionMapName = "UnityUI";

        
        private void OnEnable()
        {
            _actionMap = _inputActionAsset.FindActionMap(_actionMapName);

            foreach (var action in _actionMap.actions)
            {
                action.performed += OnAction;
            }

            _actionMap.Enable();
        }

        private void OnDisable()
        {
            foreach (var action in _actionMap.actions)
            {
                action.performed -= OnAction;
            }
            
            _actionMap.Disable();
        }
        
        private void OnAction(InputAction.CallbackContext ctx)
        {
            var device = ctx.control.device;

            if (device is Keyboard) ControllerType = ControllerType.KEYBOARD;
            else if (device is XInputController) ControllerType = ControllerType.XBOX;
            else if (device is DualShockGamepad) ControllerType = ControllerType.PLAYSTATION;
            else if (device is SwitchProControllerHID) ControllerType = ControllerType.SWITCH;
            else ControllerType = ControllerType.XBOX;
        }
    }
}