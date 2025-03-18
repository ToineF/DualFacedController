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
            UpdateRing(_ability.FireAmount);
            _materialReference.SetFloat("_Saturation", _ability.FireAmount);
        }

        private void UpdateRing(float fireAmount)
        {
            //var oldFireAmount = _materialReference.GetFloat("_Saturation");
            
            //if (oldFireAmount )
            //if ()
            //_rings[]
        }
    }
}