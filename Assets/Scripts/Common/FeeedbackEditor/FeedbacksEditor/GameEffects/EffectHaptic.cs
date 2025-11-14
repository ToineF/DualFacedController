using System.Collections;
using Common.Haptic;
using UnityEngine;

namespace FeedbacksEditor
{
    /// <summary>
    /// Shakes the current gamepad
    /// </summary>
    [System.Serializable]
    public class EffectHaptic : GameEffect
    {
        public HapticFeedback HapticParams;
        public static bool UseHaptic { get; set; } = true;
        
        public override IEnumerator Execute(GameEvent gameEvent, GameObject target)
        {
            HapticManager.VibrateForTime(UseHaptic ? HapticParams : HapticFeedback.NoForce);
            yield break;
        }

        public override Color GetColor() => new Color(.7f, .5f, .3f);

        public override string ToString()
        {
            return $"Haptic Force {HapticParams.LowFrequency}/{HapticParams.HighFrequency} during {HapticParams.Time} seconds";
        }
    }
}