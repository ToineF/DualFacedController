using System;
using FeedbacksEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestructibleObject : BoxTrigger
{
    public Action OnDestroyObject;
    
    [SerializeField] private bool _useSelfRigidbody;
    [SerializeField] private Rigidbody _selfRigidbody;
    [SerializeField] private Collider _selfCollider;
    [SerializeField] private GameObject _originalMesh;
    [SerializeField] private GameObject _destructibleMesh;
    [SerializeField] private Rigidbody[] _destructibleRigidbodies;
    [SerializeField] private float _expulsionForce;
    [SerializeField] private float _torqueForce;
    [SerializeField] private float _velocityThreshold;
    [SerializeField] private GameEvent _destructEvent;

    private bool _hasEntered = false;

    protected override void OnEnterTriggerInternal(Collider other)
    {
        var targetRigidbody = _useSelfRigidbody ? _selfRigidbody : other.attachedRigidbody;
        if (targetRigidbody == null) return;
        if (targetRigidbody.linearVelocity.sqrMagnitude < _velocityThreshold) return;
        if (_hasEntered) return;
        _hasEntered = true;

        _destructibleMesh.SetActive(true);
        _destructibleMesh.transform.SetParent(_destructibleMesh.transform.parent.parent);
        _originalMesh.SetActive(false);

        foreach (var rb in _destructibleRigidbodies)
        {
            Vector3 direction = (rb.transform.position - other.transform.position).normalized;
            rb.AddForce(direction * _expulsionForce, ForceMode.Impulse);
            rb.AddTorque(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)) * _torqueForce,
                ForceMode.Impulse);
        }
        
        if (_destructEvent != null) GameEventsManager.PlayEvent(_destructEvent, gameObject);

        OnDestroyObject?.Invoke();
        _selfCollider.enabled = false;
        Destroy(this);
    }
}