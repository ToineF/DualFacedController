using System;
using AntoineFoucault.Utilities;
using UnityEngine;
using UnityEngine.Splines;

namespace Cattac.Interactables.ChaseSequence
{
    public class ChasedCat : MonoBehaviour
    {
        [SerializeField] private SplineContainer[] _splines;
        [SerializeField] private SplineAnimate _splineAnimate;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private LayerMask _detectedLayer;
        [SerializeField] private Collider _detectionCollider;
        [SerializeField] private Animator _animator;

        private int _currentSplineIndex = -1;
        private Transform _target;
        private bool _movementCompleted;

        private void Start()
        {
            _splineAnimate.MaxSpeed = _moveSpeed;
            _target = MainGame.Instance.PlayerController.transform.GetChild(1)
                .GetChild(0); // PlayerController > Body > Snake (6)
            if (_target == null) Debug.LogError("PlayerController is missing");

            StartNewSpline();
        }

        private void StartNewSpline()
        {
            _currentSplineIndex++;
            CompleteMovement(false);
            if (_currentSplineIndex >= _splines.Length) return;

            _splineAnimate.Container = _splines[_currentSplineIndex];
            _splineAnimate.Restart(true);
            _splineAnimate.Completed += OnSplineCompleted;
        }

        private void CompleteMovement(bool enable)
        {
            _movementCompleted = enable;
            _detectionCollider.enabled = enable;
            _animator.SetBool("IsRunning", enable == false);
        }

        private void OnSplineCompleted()
        {
            _splineAnimate.Completed -= OnSplineCompleted;

            CompleteMovement(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_movementCompleted == false ||
                LayerExtensions.IsInLayerMask(other.gameObject.layer, _detectedLayer) == false) return;

            StartNewSpline();
        }
    }
}