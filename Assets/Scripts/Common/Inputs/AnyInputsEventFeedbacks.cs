using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class AnyInputsEventFeedbacks : MonoBehaviour
{
    [SerializeField] private AnyInputsEvent _inputsEvent;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _filledImage;

    [Header("Parameters")]
    [SerializeField] private float _appearSpeed;
    [SerializeField] private float _hideSpeed;
    [SerializeField] private Color _fullColor;
    [SerializeField] private Vector3 _fullScale;
    [SerializeField] private float _fullScaleTime;

    private bool _hasEnded = false;

    private void Update()
    {
        _filledImage.fillAmount = _inputsEvent.Percentile;


        if (_hasEnded) return;
        if (_inputsEvent.Percentile > 0.01f)
        {
            _canvasGroup.DOFade(1, _appearSpeed);
            if (_inputsEvent.Percentile >= 1)
            {
                _canvasGroup.DOFade(0, _hideSpeed);
                _filledImage.DOColor(_fullColor, _fullScaleTime);
                _filledImage.transform.DOPunchScale(_fullScale, _fullScaleTime);
                _hasEnded = true;
            }
        }
        else
        {
            _canvasGroup.DOFade(0, _hideSpeed);
        }
    }
}