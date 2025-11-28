using UnityEngine;
using UnityEngine.Events;

public class AnyInputsEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _unityEvent;
    [SerializeField] private float _startOffset;
    [SerializeField] private bool _oneShot = true;

    private float _timer;

    private void Update()
    {
        _timer  += Time.deltaTime;
        
        if (_timer > _startOffset && Input.anyKeyDown)
        {
            _unityEvent?.Invoke();
            if (_oneShot) Destroy(this);
        }
    }
}