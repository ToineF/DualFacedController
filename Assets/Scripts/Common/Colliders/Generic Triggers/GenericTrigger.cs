using UnityEngine;
using AntoineFoucault.Utilities;
using System;
using NaughtyAttributes;

public abstract class GenericTrigger : MonoBehaviour
{
    [Header("Settings")] [Foldout("Trigger"), SerializeField]
    protected bool _oneShot = false;

    [Foldout("Trigger"), SerializeField] protected bool _isTrigger = true;

    [Header("Filters")] [Foldout("Trigger"), SerializeField]
    protected GameObject[] _gameObjectsToIgnore;

    [Foldout("Trigger"), SerializeField] protected LayerMask _layersToDetect = -1;

    [Header("Gizmo Settings")] [Foldout("Trigger"), SerializeField]
    protected bool _displayGizmos = true;

    [Foldout("Trigger"), SerializeField] protected bool _showOnlyWhileSelected = true;
    [Foldout("Trigger"), SerializeField] protected Color _gizmoColor = Color.green;
    [Foldout("Trigger"), SerializeField] protected Color _gizmoSelectedColor = Color.red;
    [Foldout("Trigger"), SerializeField] protected Color _gizmoWireColor = Color.black;
    [Foldout("Trigger"), SerializeField] protected Color _gizmoSelectedWireColor = Color.white;

    private bool _triggered = false;
    protected Collider _collider;

    protected void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = _isTrigger;
        AwakeInternal();
    }
    
    protected virtual void AwakeInternal() { }

    #region Trigger

    protected void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        TriggerEnter(collision.collider);
    }

    private void TriggerEnter(Collider other)
    {
        if (!IsCollisionValid(other)) return;
        if (_oneShot && _triggered) return;

        OnEnterTriggerInternal(other);
        _triggered = true;
        //if (_oneShot) Destroy(gameObject);
    }

    protected virtual void OnEnterTriggerInternal(Collider other)
    {
    }

    protected void OnTriggerExit(Collider other)
    {
        TriggerExit(other);
    }

    protected void OnCollisionExit(Collision collision)
    {
        TriggerExit(collision.collider);
    }

    private void TriggerExit(Collider other)
    {
        if (!IsCollisionValid(other)) return;

        OnExitTriggerInternal(other);
    }

    protected virtual void OnExitTriggerInternal(Collider other)
    {
    }

    protected void OnTriggerStay(Collider other)
    {
        TriggerStay(other);
    }

    protected void OnCollisionStay(Collision collision)
    {
        TriggerStay(collision.collider);
    }

    private void TriggerStay(Collider other)
    {
        if (!IsCollisionValid(other)) return;

        OnStayTriggerInternal(other);
    }

    protected virtual void OnStayTriggerInternal(Collider other)
    {
    }

    private bool IsCollisionValid(Collider other)
    {
        // GameObject Check
        if (_gameObjectsToIgnore.Length > 0)
        {
            foreach (GameObject go in _gameObjectsToIgnore)
            {
                if (go == other.gameObject) return false;
            }
        }

        // Layer Check
        if (!LayerExtensions.IsInLayerMask(other.gameObject.layer, _layersToDetect)) return false;

        return true;
    }

    #endregion

    #region Gizmos

    protected void OnDrawGizmos()
    {
        if (!_displayGizmos) return;
        if (!_showOnlyWhileSelected) return;

        DrawGizmos(_gizmoColor, _gizmoWireColor);
    }

    protected void OnDrawGizmosSelected()
    {
        if (!_displayGizmos) return;

        DrawGizmos(_gizmoSelectedColor, _gizmoSelectedWireColor);
    }

    protected abstract void DrawGizmos(Color boxColor, Color wireColor);

    #endregion
}