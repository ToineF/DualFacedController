using UnityEngine;

namespace Cattac.Character.Ability
{
    public class BodyFireAbility : MonoBehaviour
    {
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
            if (_fireActivated) _fireAmount -= Time.deltaTime * _rate;
            if (_waterActivated) _fireAmount += Time.deltaTime * _rate;
            _fireAmount = Mathf.Clamp01(_fireAmount);
        }
    }
}