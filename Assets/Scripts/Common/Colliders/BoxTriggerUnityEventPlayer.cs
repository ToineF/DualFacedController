using Cattac.Character;
using UnityEngine;
using UnityEngine.Events;

public class BoxTriggerUnityEventPlayer : BoxTrigger
{
    public UnityEvent<CharacterHead> OnTriggerEnter => _onTriggerEnter;
    public UnityEvent<CharacterHead> OnTriggerExit => _onTriggerExit;
    
    [SerializeField] private UnityEvent<CharacterHead> _onTriggerEnter;
    [SerializeField] private UnityEvent<CharacterHead> _onTriggerExit;

    protected override void OnEnterTriggerInternal(Collider other)
    {
        if (other.TryGetComponent(out CharacterHead head) == false) return;
        _onTriggerEnter?.Invoke(head);
    }
    
    protected override void OnExitTriggerInternal(Collider other)
    {
        if (other.TryGetComponent(out CharacterHead head) == false) return;
        _onTriggerExit?.Invoke(head);
    }
}