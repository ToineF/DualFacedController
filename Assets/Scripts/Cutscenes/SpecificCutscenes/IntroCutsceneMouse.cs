using Cattac.Character.Multiplayer;
using UnityEngine;


public class IntroCutsceneMouse : MonoBehaviour
{
    public System.Action<IntroCutsceneMouse> Fall;

    [field: SerializeField] public int ID { get; private set; }
    [SerializeField] private Animator _animator;
    

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
        }
    }
}