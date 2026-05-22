using System;
using System.Collections;
using AntoineFoucault.Utilities;
using Cattac.Character.Multiplayer;
using FeedbacksEditor;
using UnityEngine;
using UnityEngine.Playables;


public class IntroCutscene : MonoBehaviour
{
    public Action OnAllConnected;
    
    [SerializeField] private IntroCutsceneMouse[] _mices;
    [SerializeField] private GameObject _startCamera;
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private ActivatePlayer _activatePlayer;
    [SerializeField] private GameEvent _oneConnectFeedback;
    [SerializeField] private GameEvent _allFellFeedback;
    [SerializeField] private GameObject _fallFeedbackParent;
    [SerializeField] private float _allMiceFellTimer;
    [SerializeField] private float _afterFellPlayerRegainControlTimer;
    [SerializeField] private GameObject[] _gameObjectsToDeactivateOnRestart;

    private int _miceCount = 0;
private bool _hasAlreadyBeenConnected = false;

    private void Start()
    {
        if (_hasAlreadyBeenConnected) return;
        
        _startCamera.SetActive(true);
        _activatePlayer.gameObject.SetActive(false);
        MainGame.Instance.PlayersManager.Inputs.SetInput(InputType.CUTSCENE);
    }

    private void OnEnable()
    {
        foreach (var mice in _mices)
        {
            mice.Fall += OnMouseFall;
        }
    }
    private void OnDisable()
    {
        foreach (var mice in _mices)
        {
            mice.Fall -= OnMouseFall;
        }
    }
    
    private void OnMouseFall(IntroCutsceneMouse mouse)
    {
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
        _playableDirector.Play();
        
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