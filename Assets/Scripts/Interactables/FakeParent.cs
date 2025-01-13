using System;
using UnityEngine;

public class FakeParent : MonoBehaviour, IGrabbable
{
    [SerializeField] protected Rigidbody _selfRigidbody;
    [SerializeField] protected Rigidbody _rigidbodyToApproach;
    [SerializeField] private float _force;
    [SerializeField] private int _forceIterations;
    [SerializeField] private bool _useY;
    [SerializeField] private ForceMode ForceMode;

    public void OnGrab(Head head)
    {
        _rigidbodyToApproach = head.Rigidbody;
    }

    public void OnUngrab(Head head)
    {
        _rigidbodyToApproach = null;
        _selfRigidbody.velocity = Vector3.zero;
        _selfRigidbody.angularVelocity = Vector3.zero;
        OnUngrabInternal(head.Rigidbody);
    }

    protected virtual void OnUngrabInternal(Rigidbody rb)
    {
        
    }

    private void FixedUpdate()
    {
        Follow();
        FixedUpdateInternal();
    }

    private void Follow()
    {
        if (_rigidbodyToApproach == null) return;

        var offset = (transform.position - _rigidbodyToApproach.position);
        var targetDirection = offset.normalized;
        for (int k = 0; k < _forceIterations; k++)
        {
            var force = _force * offset.sqrMagnitude * new Vector3(targetDirection.x, _useY ? targetDirection.y : 0, targetDirection.z);
            _rigidbodyToApproach.AddForce(force, ForceMode);
        }
    }

    protected virtual void FixedUpdateInternal()
    {

    }

    public void AddForce(Vector3 force)
    {
        AddForceInternal(force);
    }

    protected virtual void AddForceInternal(Vector3 force)
    {
        _selfRigidbody.AddForce(force, ForceMode.Impulse);
    }
}