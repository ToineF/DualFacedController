using System.Collections;
using UnityEngine;

namespace Cattac.Character.Multiplayer
{
    public class PlayersConnexionFeedback : MonoBehaviour
    {
        [SerializeField] private Animator _feedback;
        [SerializeField] private int _targetNumber;

        private void OnEnable()
        {
            Debug.Log(PlayersManager.OnPlayerJoinedEvent);
            PlayersManager.OnPlayerJoinedEvent.AddListener(OnJoined);
        }

        private void OnDisable()
        {
            PlayersManager.OnPlayerJoinedEvent.AddListener(OnJoined);
        }

        private void OnJoined(int number, bool firstTime)
        {
            if (firstTime &&  Mathf.Min((int)PlayersManager.PlayersAmount, _targetNumber) == number)
            {
                _feedback.enabled = true;
                StartCoroutine(Disable(2));
            }
            if (firstTime == false)
            {
                StartCoroutine(Disable(0));
            }
        }

        private IEnumerator Disable(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            
            _feedback.gameObject.SetActive(false);
        }
    }
}