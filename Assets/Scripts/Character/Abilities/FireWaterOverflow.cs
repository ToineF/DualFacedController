using System;
using UnityEngine;

namespace Cattac.Character.Ability
{
    public class FireWaterOverflow : MonoBehaviour
    {
        [SerializeField] private BodyFireAbility _ability;
        [SerializeField] private ParticleSystem _fireOverflowVFX;
        [SerializeField] private CharacterHead _waterOverflowHead;
        [SerializeField] private Transform _nextBodyPart;
        [SerializeField] private float _propulsionSpeed;
        [SerializeField] private float _propulsionY;

        private void Start()
        {
            _ability.OnFireOverflow += OnFireOverflow;
            _ability.OnWaterOverflow += OnWaterOverflow;
        }

        private void OnFireOverflow()
        {
            Debug.Log("Fire overflow");
            _fireOverflowVFX.Play();
        }
        
        private void OnWaterOverflow()
        {
            Debug.Log("Water overflow");
            _waterOverflowHead.SetSeparation(true);
            _waterOverflowHead.CurrentRigidbody.AddForce((_waterOverflowHead.transform.position - _nextBodyPart.position + Vector3.up * _propulsionY) * _propulsionSpeed);
        }
    }
}