using UnityEngine;

namespace Cattac.Character
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager Instance;

        [SerializeField] private bool _disableCursor = true;

        private void Awake()
        {
            Instance = this;
        }

        public void SetCursorVisible(bool isVisible)
        {
            if (_disableCursor)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Confined;
                Cursor.visible = isVisible;
            }
        }
    }
}