using System;
using UnityEngine;

public class RigidbodyApproacher : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbodyToApproach;
    [SerializeField] private float _force;
    [SerializeField] private int _forceIterations;
    [SerializeField] private bool _useY;
    [SerializeField] private ForceMode ForceMode;

    private void FixedUpdate()
    {
        var offset = (transform.position - _rigidbodyToApproach.position);
        var targetDirection = offset.normalized;
        for (int k = 0; k < _forceIterations; k++)
        {
            var force = _force * offset.sqrMagnitude * new Vector3(targetDirection.x, _useY ? targetDirection.y : 0, targetDirection.z);
            _rigidbodyToApproach.AddForce(force, ForceMode);
        }
    }
}