using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Do something when OnEnable is played
/// </summary>
public class OnEnableEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnable;

    private void OnEnable()
    {
        _onEnable?.Invoke();
    }
}