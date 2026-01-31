using UnityEngine;

namespace Cattac.Character.Multiplayer
{
    public class PlayersConnexionFeedback : MonoBehaviour
    {
        [SerializeField] private Animator _feedback;
        [SerializeField] private int _targetNumber;

        private void OnEnable()
        {
            PlayersManager.OnPlayerJoinedEvent += OnJoined;
        }

        private void OnDisable()
        {
            PlayersManager.OnPlayerJoinedEvent -= OnJoined;
        }

        private void OnJoined(int number)
        {
            if (Mathf.Min((int)PlayersManager.PlayersAmount, number) == _targetNumber) _feedback.enabled = true;
        }
    }
}