using UnityEngine;

namespace Cattac.Character.Ability
{
    public class FireWaterOverflow : MonoBehaviour
    {
        [SerializeField] private BodyFireAbility _ability;
        
        [Header("Abilities")]
        [SerializeField] private FireWaterSwitchAbility _originalFireAbility;
        [SerializeField] private FireWaterSwitchAbility _originalWaterAbility;

        private void Start()
        {
            _ability.OnFireOverflowStart += OnFireOverflowStart;
            _ability.OnWaterOverflowStart += OnWaterOverflowStart;
            _ability.OnFireOverflowStop += OnFireOverflowStop;
            _ability.OnWaterOverflowStop += OnWaterOverflowStop;
        }

        private void OnFireOverflowStart()
        {
            _originalWaterAbility.SwitchAbility(true);
        }
        
        private void OnFireOverflowStop()
        {
            _originalWaterAbility.SwitchAbility(false);
        }
        
        private void OnWaterOverflowStart()
        {
            _originalFireAbility.SwitchAbility(true);
        }
        
        private void OnWaterOverflowStop()
        {
            _originalFireAbility.SwitchAbility(false);
        }
    }
}