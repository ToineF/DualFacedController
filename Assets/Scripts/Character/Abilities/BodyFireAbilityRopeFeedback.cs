using System;
using UnityEngine;

namespace Cattac.Character.Ability
{
    public class BodyFireAbilityRopeFeedback : MonoBehaviour
    {
        [SerializeField] private BodyFireAbility _ability;
        [SerializeField] private Material _materialReference;
        [SerializeField] private Transform[] _rings;

        private void Update()
        {
            _materialReference.SetFloat("_Saturation", _ability.FireAmount);
            UpdateRing(_ability.FireAmount);
        }

        private void UpdateRing(float fireAmount)
        {
            //if ()
            //_rings[]
        }
    }
}