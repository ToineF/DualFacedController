using System;
using UnityEngine;

namespace Common.ScreenShake
{
    [Serializable]
    public struct ScreenShakeParams
    {
        public float Strength;
        public float Duration;
        public AnimationCurve Curve;

        public ScreenShakeParams(float strength, float duration, AnimationCurve curve)
        {
            Strength = strength;
            Duration = duration;
            Curve = curve;
        }

        public static ScreenShakeParams NoForce => new ScreenShakeParams(0, 0, null);
    }
}