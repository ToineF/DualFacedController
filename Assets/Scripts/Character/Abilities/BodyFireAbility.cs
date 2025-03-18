using System;
using UnityEngine;

namespace Cattac.Character.Ability
{
    public class BodyFireAbility : MonoBehaviour
    {
        public Action OnFireOverflow;
        public Action OnWaterOverflow;

        public float FireAmount => _fireAmount;
        
        [Header("References")]
        [SerializeField] private FireAbility _fireAbility;
        [SerializeField] private FireAbility _waterAbility;
        
        [Header("Settings")]
        [SerializeField] private float _rate;

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
            if (_fireActivated) _fireAmount -= Time.deltaTime * _rate;
            if (_waterActivated) _fireAmount += Time.deltaTime * _rate;
            
            if (_fireAmount < 0.01f && lastFireAmount >= 0.01f) OnWaterOverflow?.Invoke();
            if (_fireAmount > 0.99f && lastFireAmount <= 0.99f) OnFireOverflow?.Invoke();
            
            _fireAmount = Mathf.Clamp01(_fireAmount);
        }
    }
}