using Cattac.Character.Multiplayer;
using FeedbacksEditor;
using UnityEngine;


public class IntroCutsceneMouse : MonoBehaviour
{
    public System.Action<IntroCutsceneMouse> Fall;

    [field: SerializeField] public int ID { get; private set; }
    [SerializeField] private Animator _animator;
    [SerializeField] private GameEvent _connectFeedback;
    

    private void OnEnable()
    {
        PlayersManager.OnPlayerJoinedEvent.AddListener(OnJoined);
    }

    private void OnDisable()
    {
        PlayersManager.OnPlayerJoinedEvent.AddListener(OnJoined);
    }

    private void OnJoined(int number, bool firstTime)
    {
        if (firstTime && Mathf.Min((int)PlayersManager.PlayersAmount, ID) == number)
        {
            Fall?.Invoke(this);
            _animator.Play("Trailer_Mice_Fall");
            if (_connectFeedback) GameEventsManager.PlayEvent(_connectFeedback, gameObject);
        }
    }
}