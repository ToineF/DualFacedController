using System;
using System.Collections;
using System.Threading.Tasks;
using AntoineFoucault.Utilities;
using Cattac.Character.Multiplayer;
using FeedbacksEditor;
using UnityEngine;
using UnityEngine.Playables;


public class IntroCutscene : MonoBehaviour
{
    public Action OnAllConnected;

    [SerializeField] private IntroCutsceneMouse[] _mices;
    [SerializeField] private PlayableDirector _startTimeline;
    [SerializeField] private PlayableDirector _playerConnectedTimeline;
    [SerializeField] private ActivatePlayer _activatePlayer;
    [SerializeField] private GameEvent _oneConnectFeedback;
    [SerializeField] private GameEvent _allFellFeedback;
    [SerializeField] private GameObject _fallFeedbackParent;
    [SerializeField] private float _allMiceFellTimer;
    [SerializeField] private float _afterFellPlayerRegainControlTimer;
    [SerializeField] private GameObject[] _gameObjectsToDeactivateOnRestart;
    [SerializeField] private bool _startOnEditor;

    private int _miceCount = 0;
    private bool _hasAlreadyBeenConnected = false;
    private bool _canPlayerConnect = false;

    private async void Start()
    {
        await Task.Delay(10); // fix save system ugly
        
#if UNITY_EDITOR
        if (_startOnEditor == false) return;
#endif

        if (_hasAlreadyBeenConnected) return;

        MainGame.Instance.PlayersManager.AllowPlayerJoin(false);
        MainGame.Instance.PlayersManager.Inputs.SetInput(InputType.CUTSCENE);
        _activatePlayer.gameObject.SetActive(false);
        _startTimeline.Play();
    }

    private void OnTravellingEnd(PlayableDirector obj)
    {
        _canPlayerConnect = true;
        MainGame.Instance.PlayersManager.AllowPlayerJoin(true);
    }

    private void OnEnable()
    {
        foreach (var mice in _mices)
        {
            mice.Fall += OnMouseFall;
        }

        _startTimeline.stopped += OnTravellingEnd;
    }

    private void OnDisable()
    {
        foreach (var mice in _mices)
        {
            mice.Fall -= OnMouseFall;
        }

        _startTimeline.stopped -= OnTravellingEnd;
    }

    private void OnMouseFall(IntroCutsceneMouse mouse)
    {
        if (_canPlayerConnect == false) return;

        _miceCount++;
        if (_oneConnectFeedback) GameEventsManager.PlayEvent(_oneConnectFeedback, _fallFeedbackParent);
        Debug.Log(mouse.ID + " connected");

        if (_miceCount >= _mices.Length)
        {
            StartCoroutine(OnAllMouseFell());
        }
    }

    private IEnumerator OnAllMouseFell()
    {
        OnAllConnected?.Invoke();
        MainGame.Instance.PlayersManager.Inputs.SetInput(InputType.CUTSCENE);

        yield return new WaitForSeconds(_allMiceFellTimer);

        Debug.Log("All connected");
        if (_allFellFeedback) GameEventsManager.PlayEvent(_allFellFeedback, _fallFeedbackParent);
        _playerConnectedTimeline.Play();

        yield return new WaitForSeconds(_afterFellPlayerRegainControlTimer);
        MainGame.Instance.PlayersManager.Inputs.SetInput(InputType.CUTSCENE_RESUME);
    }

    public void AllAlreadyConnected()
    {
        _hasAlreadyBeenConnected = true;
        Destroy(_activatePlayer);
        _gameObjectsToDeactivateOnRestart.SetAllActive(false);
        MainGame.Instance.PlayersManager.Inputs.SetInput(InputType.CUTSCENE_RESUME);
    }
}