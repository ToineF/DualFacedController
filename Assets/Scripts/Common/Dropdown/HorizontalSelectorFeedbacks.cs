using DG.Tweening;
using UnityEngine;


public class HorizontalSelectorFeedbacks : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HorizontalSelector _horizontalSelector;
    [SerializeField] private Transform _nextButton;
    [SerializeField] private Transform _previousButton;

    [Header("References")]
    [SerializeField] private Vector3 _tweenAmount;
    [SerializeField] private float _tweenDuration;
    [SerializeField] private Ease _tweenEase;

    private void OnEnable()
    {
        _horizontalSelector.OnNext.AddListener(OnNext);
        _horizontalSelector.OnPrevious.AddListener(OnPrevious);
    }
    
    private void OnDisable()
    {
        _horizontalSelector.OnNext.RemoveListener(OnNext);
        _horizontalSelector.OnPrevious.RemoveListener(OnPrevious);
    }

    private void OnNext()
    {
        Tween(_nextButton);
    }
    
    private void OnPrevious()
    {
        Tween(_previousButton);
    }

    private void Tween(Transform t)
    {
        t.DOComplete();
        t.DOPunchScale(_tweenAmount, _tweenDuration).SetEase(_tweenEase).SetUpdate(true);
    }
}