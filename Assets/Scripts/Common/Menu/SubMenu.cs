using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SubMenu : MonoBehaviour
    {
        [field: Header("Current Menu")]
        [field: SerializeField]
        public CanvasGroup CanvasGroup { get; private set; }

        [field: SerializeField] public GameObject FirstSelectedButton { get; private set; }

        [field: Header("Top Menu")]
        [field: SerializeField]
        public SubMenu TopSubmenu { get; private set; }

        [field: SerializeField] public GameObject TopSelectedButton { get; private set; }

        public void OpenSubMenu(SubMenu submenu)
        {
            OpenMenu(submenu, submenu.FirstSelectedButton);
            CloseMenu(this);
        }

        public void CloseSubMenu()
        {
            if (TopSubmenu != null) OpenMenu(TopSubmenu, TopSelectedButton);
            CloseMenu(this);
        }

        protected void OpenMenu(SubMenu submenu, GameObject firstSelected = null)
        {
            submenu.CanvasGroup.interactable = true;
            submenu.CanvasGroup.alpha = 1;
            submenu.CanvasGroup.blocksRaycasts = true;
            if (firstSelected != null) EventSystem.current.SetSelectedGameObject(firstSelected);
            if (submenu._action != null) submenu._action.performed += submenu.TryCloseSubMenu;
        }

        protected void CloseMenu(SubMenu submenu, GameObject firstSelected = null)
        {
            submenu.CanvasGroup.interactable = false;
            submenu.CanvasGroup.alpha = 0;
            submenu.CanvasGroup.blocksRaycasts = false;
            if (firstSelected != null) EventSystem.current.SetSelectedGameObject(firstSelected);
            if (submenu._action != null) submenu._action.performed -= submenu.TryCloseSubMenu;
        }
        
        // New Input System
        
        #region Cancel Input

        // Inputs
        private InputAction _action;
        protected bool _canPressCancel = true;
        
        protected void Start()
        {
            var inputPrefab = MainGame.Instance.PlayerInputPrefab;
            _action = inputPrefab.actions["Cancel"];
            _action.performed += PressCancel;
            OnStartInternal(inputPrefab);
        }
        protected virtual void OnStartInternal(PlayerInput inputPrefab) { }

        protected void OnDisable()
        {
            if (_action != null) _action.performed -= PressCancel;
        }

        protected virtual void TryCloseSubMenu(InputAction.CallbackContext context)
        {
            //if (CanvasGroup.alpha == 0) return;
            if (TopSubmenu == null) return;
            if (!_canPressCancel) return;


            Debug.Log("Close : " + gameObject.name);
            _canPressCancel = false;
            CloseSubMenu();
        }

        private void PressCancel(InputAction.CallbackContext context)
        {
            _canPressCancel = true;
        }

        #endregion
    }