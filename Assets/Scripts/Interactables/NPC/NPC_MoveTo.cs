using UnityEngine;

namespace Cattac.Interactables.NPC
{
    public class NPC_MoveTo : MonoBehaviour
    {
        public float DistanceToTarget => (_target.gameObject.transform.position - _rigidbody.position).sqrMagnitude;
        
        [Header("Movements")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField, Range(0,1)] private float _acceleration;
        [SerializeField, Range(0,1)] private float _deceleration;

        private Vector3 _movement;
        private IDetectable _target;

        public void UpdateInternal(IDetectable target)
        {
            _target = target;
        }

        private void Update()
        {
            if (_target == null)
            {
                _movement = Vector3.Lerp(_movement, Vector3.zero, _deceleration);
            }
            else
            {
                var movement = (_target.gameObject.transform.position - _rigidbody.position).normalized * (_speed * Time.deltaTime);
                movement.y = 0;
                _movement = Vector3.Lerp(_movement, movement, _acceleration);
            }
            
            _rigidbody.position += _movement;
            _target = null;
        }
    }
}