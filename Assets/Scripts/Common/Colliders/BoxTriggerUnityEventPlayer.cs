using System.Collections.Generic;
using Cattac.Character;
using UnityEngine;
using UnityEngine.Events;

public class BoxTriggerUnityEventPlayer : BoxTrigger
{
    public UnityEvent<CharacterHead> OnTriggerEnter => _onTriggerEnter;
    public UnityEvent<CharacterHead> OnTriggerExit => _onTriggerExit;
    
    [SerializeField] private UnityEvent<CharacterHead> _onTriggerEnter;
    [SerializeField] private UnityEvent<CharacterHead> _onTriggerExit;
    [SerializeField] private int _minimumPlayerToActivate = 1;

    private HashSet<CharacterHead> _characters = new(); 

    protected override void OnEnterTriggerInternal(Collider other)
    {
        if (other.TryGetComponent(out CharacterHead head) == false) return;
        _characters.Add(head);
        if (_characters.Count >= _minimumPlayerToActivate) _onTriggerEnter?.Invoke(head);
    }
    
    protected override void OnExitTriggerInternal(Collider other)
    {
        if (other.TryGetComponent(out CharacterHead head) == false) return;
        _characters.Remove(head);
        if (_characters.Count < _minimumPlayerToActivate)  _onTriggerExit?.Invoke(head);
    }
}