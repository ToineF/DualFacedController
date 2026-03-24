using UnityEngine;

namespace Cattac.Interactables
{
    public class SinusBalance : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _speed = 1;
        [SerializeField, Range(-1,1)] private float _offset;
        [SerializeField] private float _maxAngle = 30;

        private void Update()
        {
            transform.eulerAngles = Vector3.forward * (_maxAngle * Mathf.Sin(Time.time * _speed + _offset * Mathf.PI) );
        }
    }
}