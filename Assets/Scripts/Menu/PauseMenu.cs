using FMOD;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Cattac.Character
{
    public class PauseMenu : SubMenu
    {
        public UnityEvent OnPause;
        public UnityEvent OnResume;
        public bool GameIsPaused { get; private set; } = false;

        [Header("References")] [SerializeField] private CanvasGroup _globalPauseUIMenu;

        [Header("Options Menu")] [SerializeField] private SubMenu[] _subMenusToClose;

        [SerializeField] private bool _active = true;
        [SerializeField] private bool _showCursorOnStart = false;

        private CursorManager _cursorManager;
        private InputAction _pauseAction;

        protected override void OnStartInternal(PlayerInput playerInput)
        {
            _pauseAction = playerInput.actions["Pause"];
            _globalPauseUIMenu.alpha = 0;
            _cursorManager = CursorManager.Instance;
            Resume();
            if (_showCursorOnStart) _cursorManager.SetCursorVisible(true);

            if (_active == false) Destroy(this);
        }

        private void Update()
        {
            if (_active && _pauseAction.WasPressedThisFrame()) StartTogglePause();
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
            MainGame.Instance.PlayersManager.Inputs.SetInput(Multiplayer.InputType.PAUSE_RESUME);
            GameIsPaused = false;
            _globalPauseUIMenu.alpha = 0f;
            _globalPauseUIMenu.interactable = false;
            _globalPauseUIMenu.blocksRaycasts = false;
            foreach (var submenu in _subMenusToClose)
            {
                CloseMenu(submenu);
            }
            CloseMenu(this);
            EventSystem.current.SetSelectedGameObject(null);
            _cursorManager.SetCursorVisible(false);
            //AudioManager.Instance.EnableMusicLowPass(false);
            FMODAudioManager.Instance.StopEQFilter();
            OnResume?.Invoke();
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            //AudioManager.Instance.EnableMusicLowPass(true);
            FMODAudioManager.Instance.PlayEQFilter();
            GameIsPaused = true;
            _globalPauseUIMenu.alpha = 1f;
            _globalPauseUIMenu.interactable = true;
            _globalPauseUIMenu.blocksRaycasts = true;
            OpenMenu(this, FirstSelectedButton);
            _cursorManager.SetCursorVisible(true);
            MainGame.Instance.PlayersManager.Inputs.SetInput(Multiplayer.InputType.PAUSE);
            OnPause?.Invoke();
        }

        protected override void TryCloseSubMenu(InputAction.CallbackContext context)
        {
            if (!_canPressCancel) return;
            Resume();
        }
    }
}