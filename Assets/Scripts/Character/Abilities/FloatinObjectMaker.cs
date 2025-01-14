using System;
using UnityEngine;

public class FloatingObjectMaker : Ability
{
    [SerializeField] private float _distanceFromUser = 1f;
    [SerializeField] private float _upRaycastOffset = 1f;
    [SerializeField] private float _sphereCastRadius = 2f;
    [SerializeField] private LayerMask _objectsLayer;
    
    [Header("Tilt Animation")]
    [SerializeField] float _tiltAmount = 5f;
    [SerializeField] float _tiltSpeed = 2f;
    [SerializeField, Range(0, 1)] private float _followLerp;
    
    private GameObject _currentObject;
    private Vector3 _currentTargetPosition;

    private void Update()
    {
        if (_currentObject == null) return;
        
        float tilt = Mathf.Sin(Time.time * _tiltSpeed) * _tiltAmount;
        _currentObject.transform.rotation = Quaternion.Euler(tilt, tilt, tilt);
        _currentObject.transform.position = Vector3.Lerp(_currentObject.transform.position, new Vector3(_currentObject.transform.position.x, transform.position.y,
            _currentObject.transform.position.z), _followLerp);
    }

    public override void UseAbility(Head user)
    {
        var lastDirection = user.LastDirection.normalized;
        _currentTargetPosition = user.transform.position + new Vector3(lastDirection.x, 0, lastDirection.y) * _distanceFromUser + Vector3.up * _upRaycastOffset;
        Physics.SphereCast(_currentTargetPosition, _sphereCastRadius, Vector3.down, out RaycastHit hit, Mathf.Infinity, _objectsLayer);
        if (hit.collider == null) return;

        var rb = hit.collider.GetComponent<Rigidbody>();
        if (rb == null) return;

        if (hit.collider.gameObject == _currentObject)
        {
            rb.isKinematic = false;
            _currentObject = null;
        }
        else
        {
            if (_currentObject != null)
            {
                _currentObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            rb.isKinematic = true;
            _currentObject = hit.collider.gameObject;
        }
    }

    private void OnDrawGizmosSelected()
    {
        AntoineFoucault.Utilities.GizmoExtensions.DrawSphereCast(_currentTargetPosition, _sphereCastRadius, Vector3.down, Mathf.Infinity);

    }
}
