using UnityEngine;
using UnityEngine.Events;

public class AnyInputsEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _unityEvent;
    [SerializeField] private bool _oneShot = true;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            _unityEvent?.Invoke();
            if (_oneShot) Destroy(this);
        }
    }
}