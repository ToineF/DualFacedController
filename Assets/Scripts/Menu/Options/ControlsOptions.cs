using FeedbacksEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Options
{
    /// <summary>
    /// Handles the controls options, using by buttons in the options menu in the inspector
    /// </summary>
    public class ControlsOptions : MonoBehaviour
    {
        private static bool _useScreenShake = true;
        private static bool _useHaptic = true;
        
        [SerializeField] private Toggle _toggleScreenshake;
        [SerializeField] private Toggle _toggleHaptic;
        [SerializeField] private GameEvent _screenshakePreview;
        [SerializeField] private GameEvent _hapticPreview;
        
        private void Start()
        {
            _toggleScreenshake.isOn = _useScreenShake;
            _toggleScreenshake.onValueChanged.AddListener(EnableScreenShake);
            
            _toggleHaptic.isOn = _useHaptic;
            _toggleHaptic.onValueChanged.AddListener(EnableHaptic);
        }
        
        private void EnableScreenShake(bool enable)
        {
            _useScreenShake = enable;
            EffectShakeCamera.UseCameraShake = enable;
            if (enable && _screenshakePreview) GameEventsManager.PlayEvent(_screenshakePreview, gameObject);
        }
        
        private void EnableHaptic(bool enable)
        {
            _useHaptic = enable;
            EffectHaptic.UseHaptic = enable;
            if (enable && _hapticPreview) GameEventsManager.PlayEvent(_hapticPreview, gameObject);
        }
    }
}