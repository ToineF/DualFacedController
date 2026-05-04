using UnityEngine;
using UnityEngine.Events;

public class SubMenuEvent : MonoBehaviour
{
    [SerializeField] private SubMenu _subMenu;
    [SerializeField] protected UnityEvent _onOpen;
    [SerializeField] protected UnityEvent _onClose;

    private void OnEnable()
    {
        _subMenu.OnOpen += OnOpen;
        _subMenu.OnClose += OnClose;
        OnEnableInternal();
    }
    
    protected virtual void OnEnableInternal() { }
    
    private void OnDisable()
    {
        _subMenu.OnOpen += OnOpen;
        _subMenu.OnClose += OnClose;
        OnDisableInternal();
    }
    protected virtual void OnDisableInternal() { }

    private void OnOpen()
    {
        _onOpen?.Invoke();
    }
    
    private void OnClose()
    {
        _onClose?.Invoke();
    }
}