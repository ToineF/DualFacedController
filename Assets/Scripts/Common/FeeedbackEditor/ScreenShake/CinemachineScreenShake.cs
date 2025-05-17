using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Common.ScreenShake
{
    public class CinemachineScreenShake : MonoBehaviour
    {
        private static CinemachineScreenShake _instance;

        [SerializeField] private ScreenShakeType _orderingType = ScreenShakeType.MAXIMUM;
        
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        private Dictionary<ScreenShakeType, Func<float>> _orderingActions = new();

        private Vector3 _startPosition;
        private List<ScreenShakeElement> _shakeElements = new();

        private CinemachineBasicMultiChannelPerlin _cinemachinePerlin;

        private void Awake()
        {
            _instance = this;
            _cinemachinePerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Start()
        {
            _startPosition = transform.localPosition;
            _orderingActions.Add(ScreenShakeType.MAXIMUM, GetMaximumOffset);
            _orderingActions.Add(ScreenShakeType.OVERRIDE, GetOverrideOffset);
            _orderingActions.Add(ScreenShakeType.ADDITIVE, GetAdditiveOffset);
            _cinemachinePerlin.m_AmplitudeGain = 0;
        }

        public static void Shake(ScreenShakeParams shakeParams)
        {
            if (_instance != null) _instance._shakeElements.Add(new ScreenShakeElement(shakeParams));
            else Debug.LogWarning("ScreenShake Instance does not exist!");
        }

        private void Update()
        {
            if (_shakeElements.Count < 1) return;

            _cinemachinePerlin.m_AmplitudeGain = GetShakeOffset();
        }

        private float UpdateShakeElement(ScreenShakeElement shakeElement)
        {
            shakeElement.Timer += UnityEngine.Time.deltaTime;
            if (shakeElement.Timer > shakeElement.Params.Duration)
            {
                _shakeElements.Remove(shakeElement);
                return 0f;
            }

            float curveStrength = shakeElement.Params.Curve.Evaluate(shakeElement.Timer / shakeElement.Params.Duration);
            var targetDirection = curveStrength * shakeElement.Params.Strength;

            return targetDirection;
        }

        private float GetShakeOffset()
        {
            return _orderingActions[_orderingType].Invoke();
        }

        private float GetOverrideOffset()
        {
            var targetMagnitude = 0f;

            for (int i = _shakeElements.Count - 1; i >= 0; i--)
            {
                if (i == _shakeElements.Count - 1) targetMagnitude = UpdateShakeElement(_shakeElements[i]);
                else UpdateShakeElement(_shakeElements[i]);
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
                if (strength > max)
                {
                    targetOffset = UpdateShakeElement(_shakeElements[i]);
                    max = strength;
                }
                else
                {
                    UpdateShakeElement(_shakeElements[i]);
                }
            }

            return targetOffset;
        }

        private float GetAdditiveOffset()
        {
            var targetOffset = 0f;

            for (int i = _shakeElements.Count - 1; i >= 0; i--)
            {
                targetOffset += UpdateShakeElement(_shakeElements[i]);
            }

            return targetOffset;
        }
    }
}