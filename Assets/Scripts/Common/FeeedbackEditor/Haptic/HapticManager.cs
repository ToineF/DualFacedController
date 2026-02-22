using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Haptic
{
    public class HapticManager : MonoBehaviour
    {
        private static HapticManager _instance;

        private void Awake()
        {
            _instance = this;
        }
        
        private void Start()
        {
            StopHaptic();
        }
        
        private void StopHaptic()
        {
            StopAllCoroutines();
            SetAllMotorSpeeds(0, 0);
        }
        
        /// <summary>
        /// Start vibrating a gamepad for a set time in seconds
        /// </summary>
        /// <param name="lowFrequency"></param>
        /// <param name="highFrequency"></param>
        /// <param name="time"></param>
        public static void VibrateForTime(float lowFrequency, float highFrequency, float time)
        {
            if (_instance == null)
            {
                Debug.LogWarning("HapticManager Instance does not exist!");
                return;
            }
                
            if (Gamepad.all.Count < 0) return;
            SetAllMotorSpeeds(lowFrequency, highFrequency);
            _instance.StartCoroutine(_instance.VibrateController(time));
        }

        /// <summary>
        /// Start vibrating a gamepad for a set time in seconds
        /// </summary>
        /// <param name="feedback"></param>
        public static void VibrateForTime(HapticFeedback feedback)
        {
            VibrateForTime(feedback.LowFrequency, feedback.HighFrequency, feedback.Time);
        }

        private IEnumerator VibrateController(float time)
        {
            yield return new WaitForSeconds(time);
            SetAllMotorSpeeds(0, 0);
        }

        private static void SetAllMotorSpeeds(float lowFrequency, float highFrequency)
        {
            foreach (var gamepad in Gamepad.all)
            {
                gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
            }
        }
    }

    [Serializable]
    public struct HapticFeedback
    {
        public float LowFrequency;
        public float HighFrequency;
        public float Time;

        public HapticFeedback(float low, float high, float time)
        {
            LowFrequency = low;
            HighFrequency = high;
            Time = time;
        }

        public static HapticFeedback NoForce => new HapticFeedback(0, 0, 0);
    }
}