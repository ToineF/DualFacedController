using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class Lever : MonoBehaviour
    {
        public UnityEvent OnLeverRight => _onLeverRight;
        public UnityEvent OnLeverLeft => _onLeverLeft;
        
        [SerializeField] private UnityEvent _onLeverLeft;
        [SerializeField] private UnityEvent _onLeverRight;
        [SerializeField] private Transform _lever;
        [SerializeField] private float _angleThreshold;

        private bool _isLeft;

        private void Update()
        {
            var angle = _lever.transform.localEulerAngles.z % 360;
            var isInverted = angle > 180;
            if (isInverted) angle = 360 - angle;
            if (angle > _angleThreshold)
            {
                if (_isLeft)
                {
                    if (isInverted == false)
                    {
                        //Debug.Log("Lever to the RIGHT");
                        _onLeverRight?.Invoke();
                        _isLeft = false;
                    }
                }
                else
                {
                    if (isInverted)
                    {
                        //Debug.Log("Lever to the LEFT");
                        _onLeverLeft?.Invoke();
                        _isLeft = true;
                    }
                }
            }
        }
    }
}