using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Cattac.Character
{
    public class PauseMenu : SubMenu
    {
        public UnityEvent OnPause;
        public UnityEvent OnResume;
        public bool GameIsPaused { get; private set; } = false;
        
        [Header("References")]
        [SerializeField] private CanvasGroup _globalPauseUIMenu;

        [Header("Options Menu")]
        [SerializeField] private SubMenu[] _subMenusToClose;

        private InputAction _pauseAction;

        protected override void OnStartInternal(PlayerInput playerInput)
        {
            _pauseAction = playerInput.actions["Pause"];
            _globalPauseUIMenu.alpha = 0;
            SetCursorVisible(false);
        }

        private void Update()
        {
            if (_pauseAction.WasPressedThisFrame()) StartTogglePause();
        }

        private void StartTogglePause()
        {
            //if (Manager.States.IsInMovableState() == false) return; //SPECIFIC SCENES WHERE WE DONT WANT THE PLAYER TO PAUSE
                                                                    // + specific moments when you don't want the player to be able to pause (ex : Quit Game Transition)

            if (GameIsPaused)
                Resume();
            else
                Pause();
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            MainGame.Instance.PlayersManager.SetInput(Multiplayer.InputType.GAMEPLAY);
            GameIsPaused = false;
            _globalPauseUIMenu.alpha = 0f;
            _globalPauseUIMenu.interactable = false;
            _globalPauseUIMenu.blocksRaycasts = false;
            foreach (var submenu in _subMenusToClose)
            {
                CloseMenu(submenu);
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            CloseMenu(this);
            SetCursorVisible(false);
            OnResume?.Invoke();
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            MainGame.Instance.PlayersManager.SetInput(Multiplayer.InputType.PAUSE);
            GameIsPaused = true;
            _globalPauseUIMenu.alpha = 1f;
            _globalPauseUIMenu.interactable = true;
            _globalPauseUIMenu.blocksRaycasts = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            OpenMenu(this, FirstSelectedButton);
            SetCursorVisible(true);
            OnPause?.Invoke();
        }

        protected override void TryCloseSubMenu(InputAction.CallbackContext context)
        {
            if (!_canPressCancel) return;
            Resume();
        }
        
        private void SetCursorVisible(bool isVisible)
        {
            Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isVisible;
        }
    }
}