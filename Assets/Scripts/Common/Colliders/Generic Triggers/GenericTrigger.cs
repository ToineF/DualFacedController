using UnityEngine;
using AntoineFoucault.Utilities;
using System;

public abstract class GenericTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected bool _oneShot = false;
    [SerializeField] protected bool _isTrigger = true;

    [Header("Filters")]
    [SerializeField] protected GameObject[] _gameObjectsToIgnore;
    [SerializeField] protected LayerMask _layersToDetect = -1;

    [Header("Gizmo Settings")]
    [SerializeField] protected bool _displayGizmos = true;
    [SerializeField] protected bool _showOnlyWhileSelected = true;
    [SerializeField] protected Color _gizmoColor = Color.green;
    [SerializeField] protected Color _gizmoSelectedColor = Color.red;
    [SerializeField] protected Color _gizmoWireColor = Color.black;
    [SerializeField] protected Color _gizmoSelectedWireColor = Color.white;

    protected Collider _collider;

    protected void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = _isTrigger;
    }

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
        
        OnEnterTriggerInternal(other);

        if (_oneShot) Destroy(gameObject);
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
