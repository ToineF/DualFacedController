using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Do something when Start is played
/// </summary>
public class OnStart : MonoBehaviour
{
    [SerializeField] private UnityEvent _onStart;

    private void Start()
    {
        _onStart?.Invoke();
    }
}