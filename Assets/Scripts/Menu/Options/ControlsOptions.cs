using FeedbacksEditor;
using UnityEngine;

namespace Menu.Options
{
    /// <summary>
    /// Handles the controls options, using by buttons in the options menu in the inspector
    /// </summary>
    public class ControlsOptions : MonoBehaviour
    {
        [SerializeField] private GameEvent _screenshakePreview;
        [SerializeField] private GameEvent _hapticPreview;
        
        public void EnableScreenShake(bool enable)
        {
            EffectShakeCamera.UseCameraShake = enable;
            if (enable && _screenshakePreview) GameEventsManager.PlayEvent(_screenshakePreview, gameObject);
        }
        
        public void EnableHaptic(bool enable)
        {
            EffectHaptic.UseHaptic = enable;
            if (enable && _hapticPreview) GameEventsManager.PlayEvent(_hapticPreview, gameObject);
        }
    }
}