using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class BallDropZone : BoxTrigger
    {
        [Header("Drop Zone Params")] public UnityEvent OnBallStay;
        [SerializeField] private float _stayTime;

        private float _timer;

        protected override void OnEnterTriggerInternal(Collider other)
        {
            if (other.gameObject.GetComponent<Ball>() == false) return;
            _timer = _stayTime;
        }

        protected override void OnStayTriggerInternal(Collider other)
        {
            if (other.gameObject.GetComponent<Ball>() == false) return;
            _timer -= Time.deltaTime;
            if (_timer < 0) OnBallStay?.Invoke();
        }
    }
}