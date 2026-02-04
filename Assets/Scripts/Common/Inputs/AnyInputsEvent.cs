using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Sends an event if any input is pressed
/// </summary>
public class AnyInputsEvent : MonoBehaviour
{
    public float Percentile => _holdTimer / _holdTime;
    
    [SerializeField] private UnityEvent _unityEvent;
    [SerializeField] private float _startOffset;
    [SerializeField] private bool _oneShot = true;
    [SerializeField] private float _holdTime = 0f;

    private float _startTimer;
    private float _holdTimer;

    private void Update()
    {
        _startTimer  += Time.deltaTime;
        if (_startTimer <= _startOffset) return;
        
        if (Input.anyKey)
        {
            _holdTimer += Time.deltaTime;
            if (_holdTimer >= _holdTime)
            {
                _unityEvent?.Invoke();
                if (_oneShot) Destroy(this);
            }
        }
        else
        {
            _holdTimer = 0;
        }
    }
}