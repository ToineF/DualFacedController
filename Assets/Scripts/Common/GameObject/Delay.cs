using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Delay : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private UnityEvent _event;

    public void PlayWithDelay()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        yield return new WaitForSeconds(_time);
        _event.Invoke();
    }
}