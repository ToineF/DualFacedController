using FeedbacksEditor;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _openOnStart = false;
    [SerializeField] private GameEvent _openFeedback;
    [SerializeField] private GameEvent _closeFeedback;

    private const string _openParameter = "Open";
    
    private void Awake()
    {
        if (_openOnStart) OpenNoFeedback();
        else CloseNoFeedback();
    }

    public void Close()
    {
        CloseNoFeedback();
        if (_closeFeedback != null) GameEventsManager.PlayEvent(_closeFeedback, gameObject);
    }
    
    private void CloseNoFeedback()
    {
        _animator.SetBool(_openParameter, false);
    }
    
    public void Open()
    {
        OpenNoFeedback();
        if (_openFeedback != null) GameEventsManager.PlayEvent(_openFeedback, gameObject);
    }

    public void OpenNoFeedback()
    {
        _animator.SetBool(_openParameter, true);
    }
}
