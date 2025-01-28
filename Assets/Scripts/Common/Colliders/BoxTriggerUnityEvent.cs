using UnityEngine;
using UnityEngine.Events;

public class BoxTriggerUnityEvent : BoxTrigger
{
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
