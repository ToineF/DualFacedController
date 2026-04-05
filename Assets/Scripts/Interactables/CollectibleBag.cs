using System.Collections;
using AntoineFoucault.Utilities;
using DG.Tweening;
using FeedbacksEditor;
using UnityEngine;

public class CollectibleBag : MonoBehaviour
{
    public System.Action OnThrownEvent;
    
    [Header("References")]
    [SerializeField] private Transform _transformToActivate;
    [SerializeField] private Rigidbody _rigidbody;
    [Header("Parameters")]
    [SerializeField] private float _startDelay;
    [SerializeField] private float _invincibilityDelay;
    [SerializeField] private float _thrownForceForward;
    [SerializeField] private float _thrownForceUp;
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private float _scaleTime;
    [SerializeField] private GameEvent _groundHitFeedback;

    private bool _isThrown = false;
    
    public void Throw()
    {
        StartCoroutine(OnThrow());
    }

    private IEnumerator OnThrow()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        
        yield return new WaitForSeconds(_startDelay);
        
        _rigidbody.AddForce(_transformToActivate.forward * _thrownForceForward + Vector3.up * _thrownForceUp, ForceMode.Impulse);
        
        yield return new WaitForSeconds(_invincibilityDelay);
        _isThrown = true;
        OnThrownEvent?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isThrown && LayerExtensions.IsInLayerMask(other.gameObject.layer, _collisionLayer))
        {
            _isThrown = false;
            
            _transformToActivate.position = other.ClosestPointOnBounds(transform.position);
            _transformToActivate.gameObject.SetActive(true);
            var targetScale = _transformToActivate.localScale;
            _transformToActivate.localScale = Vector3.zero;
            _transformToActivate.DOScale(targetScale, _scaleTime);
            _transformToActivate.SetParent(null);
            _transformToActivate.eulerAngles = Vector3.zero;
            
            if (_groundHitFeedback != null) GameEventsManager.PlayEvent(_groundHitFeedback, _transformToActivate.gameObject);
            
            Destroy(gameObject);
        }
    }
}