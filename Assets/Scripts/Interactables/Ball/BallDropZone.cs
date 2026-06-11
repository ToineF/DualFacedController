using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class BallDropZone : BoxTrigger
    {
        public UnityEvent OnBallAdded { get; set; } = new();

        public UnityEvent OnBallRemoved { get; set; } = new();
        public int RemainingBallAmount => Mathf.Max(_ballAmount - _currentBalls.Count, 0);

        [Header("Drop Zone Params")]
        public UnityEvent OnConditionMet;

        [SerializeField] private bool _oneShotBall;
        [SerializeField] private float _stayTime;
        [SerializeField] private int _ballAmount;
        [SerializeField] private bool _approachObjectOnEnter = false;
        [SerializeField] private float _approachObjectOnEnterLerp;

        private float _timer;
        private bool _timeTicks;
        private HashSet<Ball> _currentBalls = new();

        protected override void OnEnterTriggerInternal(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Ball ball) == false) return;
            TryAddBall(ball);
            _timer = _stayTime;
        }

        protected override void OnStayTriggerInternal(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Ball ball) == false) return;
            TryAddBall(ball);
            if (_currentBalls.Count < _ballAmount) return;
            _timer -= Time.deltaTime;
            if (_timer < 0 && _timeTicks)
            {
                BallEnter();
            }
        }

        public void BallEnter(bool sendEvent = true)
        {
            _timeTicks = false;
            if (sendEvent) OnConditionMet?.Invoke();
            if (_oneShotBall) Destroy(this);
        }

        protected override void OnExitTriggerInternal(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Ball ball) == false) return;
            TryRemoveBall(ball);
            _timer = _stayTime;
        }

        private void TryAddBall(Ball ball)
        {
            var lastCount = _currentBalls.Count;
            _currentBalls.Add(ball);
            var modified = _currentBalls.Count > lastCount;
            if (modified)
            {
                _timeTicks = true;
                OnBallAdded?.Invoke();
                if (_approachObjectOnEnter)
                {
                    ball.TryGetComponent<Rigidbody>(out var rb);
                    rb.isKinematic = true;
                    rb.DOMove(transform.position, _approachObjectOnEnterLerp);
                }
            }
        }
        
        private void TryRemoveBall(Ball ball)
        {
            var lastCount = _currentBalls.Count;
            _currentBalls.Remove(ball);
            var modified = _currentBalls.Count < lastCount;
            if (modified)
            {
                _timeTicks = true;
                OnBallRemoved?.Invoke();
            }
        }
    }
}