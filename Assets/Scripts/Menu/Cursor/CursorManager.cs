using UnityEngine;

namespace Cattac.Character
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void SetCursorVisible(bool isVisible)
        {
            Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Confined;
            Cursor.visible = isVisible;
        }
    }
}