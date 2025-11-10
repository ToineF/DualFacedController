using UnityEngine;
using UnityEngine.Events;

public class BoxTriggerUnityEvent : BoxTrigger
{
    public UnityEvent OnTriggerEnterEvent => _onTriggerEnter;
    public UnityEvent OnTriggerExitEvent => _onTriggerExit;
    
    [SerializeField] private UnityEvent _onTriggerEnter;
    [SerializeField] private UnityEvent _onTriggerExit;

    protected override void OnEnterTriggerInternal(Collider other)
    {
        _onTriggerEnter?.Invoke();
    }
    
    protected override void OnExitTriggerInternal(Collider other)
    {
        _onTriggerExit?.Invoke();
    }
}
