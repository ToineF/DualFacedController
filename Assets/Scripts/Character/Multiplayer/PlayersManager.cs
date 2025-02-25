using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cattac.Character.Multiplayer
{
    public class PlayersManager : MonoBehaviour
    {
        public static PlayersManager Instance;
        
        [SerializeField] private PlayerConnexionManager _playerConnexionManager;
        
        private InputAction _leftMoveAction;
        private InputAction _rightMoveAction;
        private InputAction _leftGrabAction;
        private InputAction _rightGrabAction;
        private InputAction _leftSeparateAction;
        private InputAction _rightSeparateAction;

        private List<PlayerInput> _playersInputs = new List<PlayerInput>();

        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            _playerConnexionManager.OnPlayerJoin += OnPlayerJoined;
            _playerConnexionManager.OnPlayerLeft += OnPlayerLeft;
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
    }
}