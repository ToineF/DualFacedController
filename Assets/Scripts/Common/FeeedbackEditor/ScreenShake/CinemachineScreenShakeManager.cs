using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.ScreenShake
{
    /// <summary>
    /// Manages the screen shake in the game for cinemachine
    /// </summary>
    public class CinemachineScreenShakeManager : MonoBehaviour
    {
        public static CinemachineScreenShakeManager Instance;
        
        public bool IsShaking => _shakeElements.Count > 0;
        
        private List<ScreenShakeElement> _shakeElements = new();
        private Dictionary<ScreenShakeType, Func<float>> _orderingActions = new();


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Debug.LogError("Cinemachine Screen Shake Manager already exists!");
            }
        }
        
        private void Start()
        {
            _orderingActions.Add(ScreenShakeType.MAXIMUM, GetMaximumOffset);
            _orderingActions.Add(ScreenShakeType.OVERRIDE, GetOverrideOffset);
            _orderingActions.Add(ScreenShakeType.ADDITIVE, GetAdditiveOffset);
        }
        
        public static void Shake(ScreenShakeParams shakeParams)
        {
            if (Instance != null) Instance._shakeElements.Add(new ScreenShakeElement(shakeParams));
            else Debug.LogWarning("ScreenShake Instance does not exist!");
        }
        
        private float GetOverrideOffset()
        {
            var targetMagnitude = 0f;

            for (int i = _shakeElements.Count - 1; i >= 0; i--)
            {
                var offset = UpdateShakeElement(i);
                if (i == _shakeElements.Count - 1) targetMagnitude = offset;
            }

            return targetMagnitude;
        }

        private float GetMaximumOffset()
        {
            var targetOffset = 0f;
            float max = 0f;

            for (int i = _shakeElements.Count - 1; i >= 0; i--)
            {
                var strength = _shakeElements[i].Params.Strength;
                var offset = UpdateShakeElement(i);
                if (strength > max)
                {
                    targetOffset = offset;
                    max = strength;
                }
            }

            return targetOffset;
        }

        private float GetAdditiveOffset()
        {
            var targetOffset = 0f;

            for (int i = _shakeElements.Count - 1; i >= 0; i--)
            {
                targetOffset += UpdateShakeElement(i);
            }

            return targetOffset;
        }
        
        private float UpdateShakeElement(int index)
        {
            var shakeElement = _shakeElements[index];
            shakeElement.Timer += UnityEngine.Time.unscaledDeltaTime;
            if (shakeElement.Timer > shakeElement.Params.Duration)
            {
                _shakeElements.RemoveAt(index);
                return 0f;
            }

            float curveStrength = shakeElement.Params.Curve.Evaluate(shakeElement.Timer / shakeElement.Params.Duration);
            var targetDirection = curveStrength * shakeElement.Params.Strength;

            return targetDirection;
        }

        public float GetShakeOffset(ScreenShakeType type)
        {
            return _orderingActions[type].Invoke();
        }
    }
}