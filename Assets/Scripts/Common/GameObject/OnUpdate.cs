using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Do something when Update is played
/// </summary>
public class OnUpdate : MonoBehaviour
{
    [SerializeField] private UnityEvent _onUpdate;
    [SerializeField] private UpdateMode _updateMode;

    private void Update()
    {
        if (_updateMode == UpdateMode.UPDATE) _onUpdate?.Invoke();
    }
    
    private void FixedUpdate()
    {
        if (_updateMode == UpdateMode.FIXED_UPDATE) _onUpdate?.Invoke();
    }
    
    private void LateUpdate()
    {
        if (_updateMode == UpdateMode.LATE_UPDATE) _onUpdate?.Invoke();
    }
}