using FeedbacksEditor;
using UnityEngine;

namespace Menu.Options
{
    /// <summary>
    /// Handles the accessibility options, using by buttons in the options menu in the inspector
    /// </summary>
    public class AccessibilityOptions : MonoBehaviour
    {
        public void EnableScreenShake(bool enable)
        {
            EffectShakeCamera.UseCameraShake = enable;
        }
        
        public void EnableHaptic(bool enable)
        {
            EffectHaptic.UseHaptic = enable;
        }
    }
}