using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SphereTriggerUnityEvent : SphereTrigger
{
    public UnityEvent OnTriggerEnterEvent => _onTriggerEnter;
    public UnityEvent OnTriggerExitEvent => _onTriggerExit;
    
    [SerializeField] private UnityEvent _onTriggerEnter;
    [SerializeField] private UnityEvent _onTriggerExit;
    [SerializeField] private int _minimumPlayerToActivate = 1;
    
    private List<Collider> _colliders = new(); 


    protected override void OnEnterTriggerInternal(Collider other)
    {
        _colliders.Add(other);
        if (_colliders.Count >= _minimumPlayerToActivate) _onTriggerEnter?.Invoke();
    }
    
    protected override void OnExitTriggerInternal(Collider other)
    {
        _colliders.Remove(other);
        if (_colliders.Count < _minimumPlayerToActivate) _onTriggerExit?.Invoke();
    }
}