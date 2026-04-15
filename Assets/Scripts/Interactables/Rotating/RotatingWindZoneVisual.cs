using UnityEngine;

namespace Cattac.Interactables
{
    public class RotatingWindZoneVisual : MonoBehaviour
    {
        [SerializeField] private RotatingWindZone _windZone;
        [SerializeField] private Transform _visual;
        [SerializeField] private float _speedMultiplier;

        private void Update()
        {
            var angle = Time.deltaTime * Mathf.Sign(_windZone.TurnSpeed) * Mathf.Sqrt(Mathf.Abs(_windZone.TurnSpeed)) * _speedMultiplier;
            _visual.transform.eulerAngles +=  Vector3.up * angle;
        }
    }
}