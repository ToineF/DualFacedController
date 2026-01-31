using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cattac.Character.Multiplayer
{
    /// <summary>
    /// Handles the connexion and deconnexion of player, updating the inputs and enabling/disabling the action maps
    /// </summary>
    public class PlayersManager : MonoBehaviour
    {
        public static System.Action<int> OnPlayerJoinedEvent { get; set; }
        public static PlayersAmount PlayersAmount { get; set; } = PlayersAmount.TWO;
        public PlayersInputsUpdater Inputs { get; private set; } =  new PlayersInputsUpdater();
        
        [SerializeField] private PlayerInputManager _playerInputManager;
        [SerializeField] private PlayerInput _playerInputPrefab;
        
        private static List<InputDevice> _connectedDevices = new List<InputDevice>();
        private static int _joinedCount;
        private InputAction _joinAction = new InputAction(binding: "/*/<button>");

        private void OnEnable()
        {
            _playerInputManager.onPlayerJoined += OnPlayerJoined;
            _playerInputManager.onPlayerLeft += OnPlayerLeft;
            InputSystem.onDeviceChange += OnDeviceChange;
            _joinAction.started += OnJoinPressed;
            _joinAction.Enable();
        }

        private void OnDisable()
        {
            _playerInputManager.onPlayerJoined -= OnPlayerJoined;
            _playerInputManager.onPlayerLeft -= OnPlayerLeft;
            InputSystem.onDeviceChange -= OnDeviceChange;
            _joinAction.started -= OnJoinPressed;
        }

        private void Start()
        {
            ReconnectDevices();
        }

        private void OnPlayerJoined(PlayerInput playerInput)
        {
            Debug.Log($"PlayerJoined : {playerInput} connected");
            Inputs.AddInput(playerInput);
        }

        private void OnPlayerLeft(PlayerInput playerInput)
        {
            Debug.Log("PlayerLeft : " + playerInput);
            Inputs.RemoveInput(playerInput);
        }

        private void OnJoinPressed(InputAction.CallbackContext context)
        {
            JoinPlayer(context.control.device);
        }

        private void ReconnectDevices()
        {
            if (_joinedCount > 0)
            {
                Debug.Log("Scene Reload Start");
                for (var i = 0; i < _connectedDevices.Count; i++) 
                {
                    Debug.Log($"Device joined : {_connectedDevices[i]} linked");
                    CreateInputForDevice(_connectedDevices[i]);
                    OnPlayerJoinedEvent?.Invoke(i+1);
                }

                CheckEndJoining();
                Debug.Log("Scene Reload End");
            }
        }

        private void JoinPlayer(InputDevice device)
        {
            if (device == null) return;
            
            // Ignore Mouse
            if (device is Mouse) return;

            if (_connectedDevices.Contains(device))
            {
                Debug.Log($"Device Joined : {device} already connected");
                return;
            }

            _connectedDevices.Add(device);
            Debug.Log($"Device Joined : {device} connected");

            CreateInputForDevice(device);

            _joinedCount++;
            OnPlayerJoinedEvent?.Invoke(_joinedCount);
            CheckEndJoining();
        }

        private void CreateInputForDevice(InputDevice device)
        {
            PlayerInput.Instantiate(_playerInputPrefab.gameObject, pairWithDevice: device);
        }

        private void CheckEndJoining()
        {
            if (_joinedCount >= Mathf.Min(_playerInputManager.maxPlayerCount, (int)PlayersAmount))
            {
                Debug.Log("End joining");
                _joinAction.Disable();
            }
        }

        // Disconnection
        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (change == InputDeviceChange.Removed && _connectedDevices.Contains(device))
            {
                Debug.Log($"Warning : Device Removed : {device}");
            }
        }
    }
}