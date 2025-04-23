using UnityEngine;


namespace Cattac.Character.Ability
{
    public class FireWaterSwitchAbility : Ability
    {
        public FireAbility CurrentAbility { get; private set; }

        [SerializeField] private FireAbility _originalAbility;
        [SerializeField] private FireAbility _overflowAbility;

        private void Awake()
        {
            CurrentAbility = _originalAbility;
        }

        public void SwitchAbility(bool isOverflow)
        {
            var previousAbility = CurrentAbility;
            
            previousAbility.UseAbility(null, false);
            if (isOverflow) CurrentAbility = _overflowAbility;
            else CurrentAbility = _originalAbility;
            
            ToggleSubscription(CurrentAbility, previousAbility);
        }

        public override void UseAbility(CharacterHead user, bool use)
        {
            CurrentAbility.UseAbility(user, use);
        }

        private void ToggleSubscription(FireAbility newAbility, FireAbility oldAbility)
        {
            oldAbility.OnAbilityActivated -= OnAbilityActivated;
            oldAbility.OnAbilityDeactivated -= OnAbilityDeactivated;
            
            newAbility.OnAbilityActivated += OnAbilityActivated;
            newAbility.OnAbilityDeactivated += OnAbilityDeactivated;
        }
        
        private void OnAbilityActivated() {}
        private void OnAbilityDeactivated() {}
    }
}