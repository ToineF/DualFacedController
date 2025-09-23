using Cattac.Character;
using UnityEngine;
using UnityEngine.Events;

public class BoxTriggerUnityEventPlayer : BoxTrigger
{
    public UnityEvent OnTriggerEnter => _onTriggerEnter;
    public UnityEvent OnTriggerExit => _onTriggerExit;
    
    [SerializeField] private UnityEvent _onTriggerEnter;
    [SerializeField] private UnityEvent _onTriggerExit;

    protected override void OnEnterTriggerInternal(Collider other)
    {
        if (other.GetComponent<CharacterHead>() == false) return;
        _onTriggerEnter?.Invoke();
    }
    
    protected override void OnExitTriggerInternal(Collider other)
    {
        if (other.GetComponent<CharacterHead>() == false) return;
        _onTriggerExit?.Invoke();
    }
}