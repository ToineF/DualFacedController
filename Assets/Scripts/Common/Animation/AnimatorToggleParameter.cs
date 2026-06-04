using FeedbacksEditor;
using UnityEngine;


public class AnimatorToggleParameter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _parameterName;
    [SerializeField] private GameEvent _onFeedback;
    [SerializeField] private GameEvent _offFeedback;

    private bool _isOn;
    
    public void Toggle()
    {
        _animator.SetBool(_parameterName, !_animator.GetBool(_parameterName));
    }

    public void SetBool(bool value)
    {
        _animator.SetBool(_parameterName, value);
        if (_isOn != value)
        {
            _isOn = value;
            GameEventsManager.PlayEvent(value ? _onFeedback : _offFeedback, _animator.gameObject);
        }
    }
}