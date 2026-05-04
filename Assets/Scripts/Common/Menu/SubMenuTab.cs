using UnityEngine;
using UnityEngine.InputSystem;


public class SubMenuTab : SubMenu
{
    [Header("Tabs Menus")]
    [SerializeField] private SubMenu _previousSubMenu;
    [SerializeField] private SubMenu _nextSubMenu;

    private InputAction _previousAction;
    private InputAction _nextAction;

    protected override void OnStartInternal(PlayerInput inputPrefab)
    {
        _previousAction = inputPrefab.actions["Previous"];
        _nextAction = inputPrefab.actions["Next"];
    }

    protected override void OnDisableInternal()
    {
        if (_previousAction != null) _previousAction.performed -= PressPrevious;
        if (_nextAction != null) _nextAction.performed -= PressNext;
    }
    
    protected override void OpenMenuInternal()
    {
        if (_previousAction != null) _previousAction.performed += PressPrevious;
        if (_nextAction != null) _nextAction.performed += PressNext;
    }

    protected override void CloseMenuInternal()
    {
        if (_previousAction != null) _previousAction.performed -= PressPrevious;
        if (_nextAction != null) _nextAction.performed -= PressNext;
    }

    private void PressPrevious(InputAction.CallbackContext context)
    {
        if (_previousSubMenu)
        {
            CloseSubMenu();
            TopSubmenu.OpenSubMenu(_previousSubMenu);
        }
    }

    private void PressNext(InputAction.CallbackContext context)
    {
        Debug.Log("Pressing next menu");
        if (_nextSubMenu)
        {
            CloseSubMenu();
            TopSubmenu.OpenSubMenu(_nextSubMenu);
        }
    }
}