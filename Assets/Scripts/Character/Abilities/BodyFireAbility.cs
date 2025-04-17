using System;
using UnityEngine;

namespace Cattac.Character.Ability
{
    public class BodyFireAbility : MonoBehaviour
    {
        public Action OnFireOverflowStart;
        public Action OnWaterOverflowStart;
        public Action OnFireOverflowStop;
        public Action OnWaterOverflowStop;

        public float FireAmount => _fireAmount;
        
        [Header("References")]
        [SerializeField] private FireAbility _fireAbility;
        [SerializeField] private FireAbility _waterAbility;
        
        [Header("Settings")]
        [SerializeField] private float _gaugeIncreaseRate;
        [SerializeField, Range(0,1)] private float _gaugeDecreaseLerp;
        [SerializeField] private float _gaugeDecreaseWait;

        private float _fireAmount = 0.5f;
        private bool _fireActivated;
        private bool _waterActivated;

        private void Start()
        {
            _fireAbility.OnAbilityActivated += () => _fireActivated = true;
            _fireAbility.OnAbilityDeactivated += () => _fireActivated = false;
            _waterAbility.OnAbilityActivated += () => _waterActivated = true;
            _waterAbility.OnAbilityDeactivated += () => _waterActivated = false;
        }

        private void Update()
        {
            var lastFireAmount = _fireAmount;
            if (_fireActivated) _fireAmount -= Time.deltaTime * _gaugeIncreaseRate;
            if (_waterActivated) _fireAmount += Time.deltaTime * _gaugeIncreaseRate;

            // When no ability used, gauge returns to center
            if (_fireActivated == false && _waterActivated == false) _gaugeDecreaseTimer += Time.deltaTime;
            else _gaugeDecreaseTimer = 0;
            if (_gaugeDecreaseTimer >= _gaugeDecreaseWait) _fireAmount = Mathf.Lerp(_fireAmount, 0.5f, _gaugeDecreaseLerp);
            
            if (_fireAmount < 0.01f && lastFireAmount >= 0.01f) OnWaterOverflowStart?.Invoke();
            if (_fireAmount > 0.99f && lastFireAmount <= 0.99f) OnFireOverflowStart?.Invoke();
            if (_fireAmount >= 0.01f && lastFireAmount < 0.01f) OnWaterOverflowStop?.Invoke();
            if (_fireAmount <= 0.99f && lastFireAmount > 0.99f) OnFireOverflowStop?.Invoke();
            
            _fireAmount = Mathf.Clamp01(_fireAmount);
        }
    }
}