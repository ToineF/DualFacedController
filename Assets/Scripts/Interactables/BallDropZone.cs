using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class BallDropZone : BoxTrigger
    {
        [Header("Drop Zone Params")] public UnityEvent OnBallStay;
        [SerializeField] private bool _oneShotBall;
        [SerializeField] private float _stayTime;
        [SerializeField] private int _ballAmount;

        private float _timer;
        private HashSet<Ball> _currentBalls = new();

        protected override void OnEnterTriggerInternal(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Ball ball) == false) return;
            _currentBalls.Add(ball);
            _timer = _stayTime;
        }

        protected override void OnStayTriggerInternal(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Ball ball) == false) return;
            _currentBalls.Add(ball);
            if (_currentBalls.Count < _ballAmount) return;
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                OnBallStay?.Invoke();
                if (_oneShotBall) Destroy(this);
            }
        }

        protected override void OnExitTriggerInternal(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Ball ball) == false) return;
            _currentBalls.Remove(ball);
            _timer = _stayTime;
        }
    }
}