using UnityEngine;
using UnityEngine.Events;


public class BallDropZone : BoxTrigger
{
    [Header("Drop Zone Params")]
    public UnityEvent OnBallStay;
    [SerializeField] private float _stayTime;

    private float _timer;

    private void Start()
    {
        OnEnterTrigger += OnBallEnter;
    }

    private void OnBallEnter()
    {
        if (_lastOtherCollider.gameObject.GetComponent<Ball>() == false) return;
        _timer = _stayTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>() == false) return;
        _timer -= Time.deltaTime;
        if (_timer < 0) OnBallStay?.Invoke();
    }
}