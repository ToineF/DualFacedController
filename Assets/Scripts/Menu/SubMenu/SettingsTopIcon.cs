using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsTopIcon : SubMenuEvent
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;
    
    [Header("Background")]
    [SerializeField] private Sprite _backgroundSpriteSelected;
    [Header("Rotation")]
    [SerializeField] private Vector3 _startRotationOffset;
    [SerializeField] private float _zRotation;
    [SerializeField] private float _rotationInTime;
    [SerializeField] private AnimationCurve _rotationInAnimationCurve;
    [Header("Scale")]
    [SerializeField] private Vector3 _startScaleOffset;
    [SerializeField] private float _hoverScale;
    [SerializeField] private float _hoverScaleDuration;
    [SerializeField] private AnimationCurve _scaleInAnimationCurve;
    [Header("Color")]
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _targetColor;
    [SerializeField] private float _colorInTime;
    [SerializeField] private AnimationCurve _colorInAnimationCurve;
    [Header("Icon Color")]
    [SerializeField] private Color _iconStartColor;
    [SerializeField] private Color _iconTargetColor;
    
    private Vector3 _originalRotation;
    private Vector3 _originalScale;

    private void Start()
    {
        _originalRotation = _background.transform.localEulerAngles;
        _originalScale = _background.transform.localScale;
    }

    protected override void OnEnableInternal()
    {
        _onOpen.AddListener(Appear);
    }

    protected override void OnDisableInternal()
    {
        _onOpen.RemoveListener(Appear);
    }

    private void Appear()
    {
        // Reset values
        _background.transform.localScale = Vector3.Scale(_originalScale, _startScaleOffset);
        _background.transform.localEulerAngles = _originalRotation + _startRotationOffset;
        _background.color = _startColor;
        _icon.color = _iconStartColor;
        _background.sprite = _backgroundSpriteSelected;

        // Tween
        _background.DOKill();
        _background.transform.DOLocalRotate(
            new Vector3(_originalRotation.x, _originalRotation.y, _originalRotation.z +_zRotation), _rotationInTime).SetEase(_rotationInAnimationCurve).SetUpdate(true);
        _background.transform.DOScale(new Vector3(_hoverScale, _hoverScale, 1), _hoverScaleDuration).SetEase(_scaleInAnimationCurve).SetUpdate(true);
        _background.DOColor(_targetColor, _colorInTime).SetEase(_colorInAnimationCurve).SetUpdate(true);
        _icon.DOColor(_iconTargetColor, _colorInTime).SetEase(_colorInAnimationCurve).SetUpdate(true);
    }
}