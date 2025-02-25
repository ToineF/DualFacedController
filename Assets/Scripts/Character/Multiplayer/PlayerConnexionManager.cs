using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cattac.Character.Multiplayer
{
    /// <summary>
    /// Manage the connexion and deconnexion of the players 
    /// </summary>
    public class PlayerConnexionManager : MonoBehaviour
    {
        public Action<UnityEngine.InputSystem.PlayerInput> OnPlayerJoin;
        public Action<UnityEngine.InputSystem.PlayerInput> OnPlayerLeft;
        
        [SerializeField] private PlayerInputManager _playerInputManager;

        private void OnEnable()
        {
            _playerInputManager.onPlayerJoined += PlayerJoin;
            _playerInputManager.onPlayerLeft += PlayerLeft;
        }

        private void OnDisable()
        {
            _playerInputManager.onPlayerJoined -= PlayerJoin;
            _playerInputManager.onPlayerLeft -= PlayerLeft;
        }

        private void PlayerJoin(PlayerInput input)
        {
            OnPlayerJoin?.Invoke(input);
            Debug.Log($"Player joined : {input.user}");
        }

        private void PlayerLeft(PlayerInput input)
        {
            OnPlayerLeft?.Invoke(input);
            Debug.Log($"Player left : {input.user}");
        }
    }
}