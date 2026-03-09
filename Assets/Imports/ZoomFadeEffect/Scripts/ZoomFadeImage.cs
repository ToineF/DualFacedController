using System;
using UnityEngine;

public class ZoomFadeImage : MonoBehaviour
{
    public Action OnTweenStart;
    public Action OnTweenFinish;
    
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private float _lerp;
    [SerializeField] private float _targetScale;

    private bool _isTweening;
    private readonly float _endThreshold = 0.01f;

    public void Play()
    {
        _isTweening = true;
        _group.alpha = 1;
        _group.transform.localScale = Vector3.one;
        OnTweenStart?.Invoke();
    }

    private void Update()
    {
        if (_isTweening == false) return;

        _group.alpha = Mathf.Lerp(_group.alpha, 0, _lerp * Time.deltaTime);
        _group.transform.localScale = Vector3.Lerp(_group.transform.localScale, Vector3.one * _targetScale, _lerp * Time.deltaTime);
        if (_group.alpha < _endThreshold)
        {
            _isTweening = false;
            _group.alpha = 0;
            OnTweenFinish?.Invoke();
        }
    }
}
