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
        [SerializeField] private PlayerInputManager _playerInputManager;
        
        private InputAction _leftMoveAction;
        private InputAction _rightMoveAction;
        private InputAction _leftGrabAction;
        private InputAction _rightGrabAction;
        private InputAction _leftSeparateAction;
        private InputAction _rightSeparateAction;

        private List<PlayerInput> _playersInputs = new List<PlayerInput>();
        
        private bool _isPause = false;
        private bool _isCutscene = false;


        private void OnEnable()
        {
            _playerInputManager.onPlayerJoined += OnPlayerJoined;
            _playerInputManager.onPlayerLeft += OnPlayerLeft;
        }

        private void OnDisable()
        {
            _playerInputManager.onPlayerJoined -= OnPlayerJoined;
            _playerInputManager.onPlayerLeft -= OnPlayerLeft;
        }


        private void OnPlayerJoined(PlayerInput playerInput)
        {
            Debug.Log($"PlayerJoined : { playerInput } connected");
            _playersInputs.Add(playerInput);
            UpdateInputs(playerInput);
        }
        
        private void OnPlayerLeft(PlayerInput playerInput)
        {
            Debug.Log("PlayerLeft : " + playerInput);
            _playersInputs.Remove(playerInput);
            UpdateInputs(playerInput);
        }

        private void UpdateInputs(PlayerInput lastPlayerInput)
        {
            // Disable or enable joining based on player count
            if (_playersInputs.Count >= _playerInputManager.maxPlayerCount && _playerInputManager.joiningEnabled)
                _playerInputManager.DisableJoining();
            else if (_playersInputs.Count < _playerInputManager.maxPlayerCount && !_playerInputManager.joiningEnabled)
                _playerInputManager.EnableJoining();
            
            if (_playersInputs.Count == 1)
            {
                _leftMoveAction = lastPlayerInput.actions["MoveLeft"];
                _leftGrabAction = lastPlayerInput.actions["GrabLeft"];
                _leftSeparateAction = lastPlayerInput.actions["SeparateLeft"];
                _rightMoveAction = lastPlayerInput.actions["MoveRight"];
                _rightGrabAction = lastPlayerInput.actions["GrabRight"];
                _rightSeparateAction = lastPlayerInput.actions["SeparateRight"];
            }
            else if (_playersInputs.Count == 2)
            {
                _rightMoveAction = lastPlayerInput.actions["MoveLeft"];
                _rightGrabAction = lastPlayerInput.actions["GrabLeft"];
                _rightSeparateAction = lastPlayerInput.actions["SeparateLeft"];
            }
            
            SetPlayers();
        }
        
        public void SetPlayers()
        {
            UserInput.Instance.LeftHead = new PlayerInputReferences(_leftMoveAction, _leftGrabAction, _leftSeparateAction);
            UserInput.Instance.RightHead = new PlayerInputReferences(_rightMoveAction, _rightGrabAction, _rightSeparateAction);
        }

        public void SetInput(InputType inputType)
        {
            foreach (var playerInput in _playersInputs)
            {
                switch (inputType)
                {
                    case InputType.PAUSE_RESUME:
                        _isPause = false;
                        UpdateInputMaps(playerInput);
                        break;
                    
                    case InputType.PAUSE:
                        _isPause = true;
                        UpdateInputMaps(playerInput);
                        break;
                    
                    case InputType.CUTSCENE:
                        _isCutscene = true;
                        UpdateInputMaps(playerInput);
                        break;
                    case InputType.CUTSCENE_RESUME:
                        _isCutscene = false;
                        UpdateInputMaps(playerInput);
                        break;
                }
            }
        }

        private void UpdateInputMaps(PlayerInput playerInput)
        {
            if (_isPause) playerInput.actions.FindActionMap("UnityUI").Enable();
            else playerInput.actions.FindActionMap("UnityUI").Disable();
            if (_isCutscene || _isPause) playerInput.actions.FindActionMap("Player").Disable();
            else playerInput.actions.FindActionMap("Player").Enable();
        }
    }
}