using UnityEngine;
using UnityEngine.Events;

public class BoxTriggerUnityEvent : BoxTrigger
{
    [SerializeField] private UnityEvent _onTriggerEnter;
    [SerializeField] private UnityEvent _onTriggerExit;

    private void Start()
    {
        OnEnterTrigger += () => _onTriggerEnter?.Invoke();
        OnExitTrigger += () => _onTriggerExit?.Invoke();
    }
}
