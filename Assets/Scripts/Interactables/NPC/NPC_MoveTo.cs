using UnityEngine;

namespace Cattac.Interactables.NPC
{
    public class NPC_MoveTo : NPC_Seeker
    {
        [Header("Movements")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField, Range(0,1)] private float _acceleration;
        [SerializeField, Range(0,1)] private float _deceleration;

        private Vector3 _movement;

        protected override void UpdateInternal()
        {
            if (Target == null)
            {
                _movement = Vector3.Lerp(_movement, Vector3.zero, _deceleration);
                _rigidbody.position += _movement;
                return;
            }

            var movement = (Target.transform.position - _rigidbody.position).normalized * (_speed * Time.deltaTime);
            movement.y = 0;
            _movement = Vector3.Lerp(movement, _movement, _acceleration);
            _rigidbody.position += _movement;
        }
    }
}