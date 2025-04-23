using System;
using UnityEngine;

namespace Cattac.Character.Ability
{
    public class BodyFireAbilityRopeFeedback : MonoBehaviour
    {
        [SerializeField] private BodyFireAbility _ability;
        [SerializeField] private Material _materialReference;
        [SerializeField] private GameObject[] _ringsWater;
        [SerializeField] private GameObject[] _ringsFire;
        [SerializeField, Range(0,1)] private float[] _ringPositions;

        private void Update()
        {
            //UpdateRing(_ability.FireAmount);
            _materialReference.SetFloat("_Saturation", _ability.FireAmount);
        }

        private void UpdateRing(float fireAmount)
        {
            var oldFireAmount = _materialReference.GetFloat("_Saturation");

            for (int i = 0; i < _ringPositions.Length; i++)
            {
                var targetPosition = _ringPositions[i];
                if (oldFireAmount < targetPosition && fireAmount >= targetPosition)
                {
                    _ringsWater[i].SetActive(true);
                    _ringsFire[i].SetActive(false);
                }
                else if (oldFireAmount >= targetPosition && fireAmount < targetPosition)
                {
                    _ringsWater[i].SetActive(false);
                    _ringsFire[i].SetActive(true);
                }
            }
        }
    }
}