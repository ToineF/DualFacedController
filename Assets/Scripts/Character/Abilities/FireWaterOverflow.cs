using UnityEngine;

namespace Cattac.Character.Ability
{
    public class FireWaterOverflow : MonoBehaviour
    {
        [SerializeField] private BodyFireAbility _ability;
        
        [Header("Water Overflow")]
        [SerializeField] private CharacterHead _fireHead;
        [SerializeField] private Transform _nextBodyPart;
        [SerializeField] private float _propulsionSpeed;
        [SerializeField] private float _propulsionY;
        
        [Header("Fire Overflow")]
        [SerializeField] private CharacterHead _waterHead;
        [SerializeField] private ParticleSystem _fireOverflowVFX;
        [SerializeField] private float _fireOverflowSpeed;

        private bool _fireOverflow = false;

        private void Start()
        {
            _ability.OnFireOverflow += OnFireOverflow;
            _ability.OnWaterOverflow += OnWaterOverflow;
        }

        private void Update()
        {
            if (_fireOverflow)
            {
                var randomX = Random.Range(-1, 2);
                var randomY = Random.Range(-1, 2);
                _waterHead.CurrentRigidbody.AddForce(new Vector3(randomX, 0, randomY).normalized * _fireOverflowSpeed);
            }
        }

        private void OnFireOverflow()
        {
            _fireOverflowVFX.Play();
            _fireOverflow = true;
        }
        
        private void OnWaterOverflow()
        {
            _fireHead.SetSeparation(true);
            _fireHead.CurrentRigidbody.AddForce((_fireHead.transform.position - _nextBodyPart.position + Vector3.up * _propulsionY) * _propulsionSpeed);
        }
    }
}