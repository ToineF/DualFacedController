using System.Linq;
using UnityEngine;

namespace Menu.Options
{
    /// <summary>
    /// Handles the display options, using by buttons in the options menu in the inspector
    /// </summary>
    public class DisplayOptions : MonoBehaviour
    {
        [SerializeField] private Vector2Int[] _resolutions;
        [SerializeField] private HorizontalSelector _resolutionSelector;

        private void Start()
        {
            _resolutionSelector.Options = _resolutions.Select(e => e.x + "x" + e.y).ToArray();
        }

        public void EnableFullscreen(bool enable)
        {
            Screen.fullScreen = enable;
        }
        
        public void ChangeResolution(int index)
        {
            if (index >= _resolutions.Length) return;
            Screen.SetResolution(_resolutions[index].x, _resolutions[index].y, Screen.fullScreen);
        }

        public void EnableVSync(bool enable)
        {
            QualitySettings.vSyncCount = enable ? 1 : 0;
        }
    }
}