using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Options
{
    /// <summary>
    /// Handles the display options, using by buttons in the options menu in the inspector
    /// </summary>
    public class DisplayOptions : MonoBehaviour
    {
        private static int _resolutionIndex = 1; 
        private static bool _isFullscreen = true;
        private static bool _hasVSync = true; 
        
        [SerializeField] private Vector2Int[] _resolutions;
        [SerializeField] private HorizontalSelector _resolutionSelector;
        [SerializeField] private Toggle _fullscreenToggle;
        [SerializeField] private Toggle _vsyncToggle;

        private void Start()
        {
            _resolutionSelector.Options = _resolutions.Select(e => e.x + "x" + e.y).ToArray();
            
            _resolutionSelector.CurrentIndex = _resolutionIndex;
            _resolutionSelector.OnValueChange.AddListener(ChangeResolution);
            
            _fullscreenToggle.isOn = _isFullscreen;
            _fullscreenToggle.onValueChanged.AddListener(EnableFullscreen);
            
            _vsyncToggle.isOn = _hasVSync;
            _vsyncToggle.onValueChanged.AddListener(EnableVSync);
        }

        private void ChangeResolution(int index)
        {
            if (index >= _resolutions.Length) return;
            _resolutionIndex = index;
            Screen.SetResolution(_resolutions[index].x, _resolutions[index].y, Screen.fullScreen);
        }
        
        private void EnableFullscreen(bool enable)
        {
            _isFullscreen = enable;
            Screen.fullScreen = enable;
        }

        private void EnableVSync(bool enable)
        {
            _hasVSync = enable;
            QualitySettings.vSyncCount = enable ? 1 : 0;
        }
    }
}