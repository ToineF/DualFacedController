using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cattac.Character.Multiplayer
{
    public class PlayersManager : MonoBehaviour
    {
        [SerializeField] private PlayerConnexionManager _playerConnexionManager;
        
        private InputAction _leftMoveAction;
        private InputAction _rightMoveAction;
        private InputAction _leftGrabAction;
        private InputAction _rightGrabAction;
        private InputAction _leftSeparateAction;
        private InputAction _rightSeparateAction;

        private List<PlayerInput> _playersInputs = new List<PlayerInput>();
        
        private void Start()
        {
        }

        private void OnEnable()
        {
            
            foreach (var playerInput in _playersInputs.ToList())
            {
                //var input = playerInput.actions.FindActionMap("Player");
                //input.Enable();
                playerInput.gameObject.SetActive(true);
                //playerInput.ActivateInput();
            }
            _playerConnexionManager.OnPlayerJoin += OnPlayerJoined;
            _playerConnexionManager.OnPlayerLeft += OnPlayerLeft;
        }
        
        private void OnDisable()
        {
            //_playerConnexionManager.enabled = false;
            foreach (var playerInput in _playersInputs)
            {
                //var input = playerInput.actions.FindActionMap("Player");
                //input.Disable();
                playerInput.gameObject.SetActive(false);
                //playerInput.DeactivateInput();
            }
            _playerConnexionManager.OnPlayerJoin -= OnPlayerJoined;
            _playerConnexionManager.OnPlayerLeft -= OnPlayerLeft;
        }

        private void OnPlayerJoined(PlayerInput playerInput)
        {
            Debug.Log("PlayerJoined : " + playerInput);
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

        public void Activate(bool activate)
        {
            _playerConnexionManager.enabled = activate;
            Debug.Log("Players Manager : " + activate);
            foreach (var playerInput in _playersInputs)
            {
                if (activate) playerInput.ActivateInput();
                else playerInput.DeactivateInput();
            }
        }
    }
}