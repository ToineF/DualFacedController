using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private MenuManager _menuManager;

    [Header("Button Parameters")] 
    [SerializeField] private string _targetScene;
    [SerializeField] private Selectable _selectable;

    [Header("Scale")] [SerializeField] private float _originalScale = 1;
    [SerializeField] private float _hoverScale = 1.1f;
    [SerializeField] private float _hoverScaleDuration = 0.3f;
    [SerializeField] private float _notHoverScaleDuration = 0.5f;
    [SerializeField] private AnimationCurve _scaleInAnimationCurve;
    [SerializeField] private AnimationCurve _scaleOutAnimationCurve;

    [Header("Rotation")] [SerializeField] private float _zRotation;
    [SerializeField] private float _rotationInTime = .2f;
    [SerializeField] private float _rotationOutTime = .2f;
    [SerializeField] private AnimationCurve _rotationInAnimationCurve;
    [SerializeField] private AnimationCurve _rotationOutAnimationCurve;
    private Vector3 _originalRotation;

    private void Start()
    {
        _menuManager = MenuManager.Instance;
        _originalRotation = transform.localEulerAngles;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_menuManager.CanClickButtons == false) return;

        OnSelect();
        _selectable.Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnDeselect();
    }

    public void GoToScene()
    {
        TryClickButtonTransition(() => SceneManager.LoadScene(_targetScene));
    }

    public void RestartScene()
    {
        TryClickButtonTransition(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void GoToNextScene()
    {
        TryClickButtonTransition(() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 % SceneManager.sceneCountInBuildSettings));
    }

    public void QuitGame()
    {
        TryClickButtonTransition(() => Application.Quit());
    }

    private void TryClickButtonTransition(Action action)
    {
        if (_menuManager.CanClickButtons == false) return;

        _menuManager.SetButtonsUnclickable();

        _menuManager.Transition.SetTransition(() => action?.Invoke());
    }

    public void OnSelect()
    {
        if (_menuManager.CanClickButtons == false) return;

        transform.DOKill();
        transform.DOLocalRotate(
            new Vector3(_originalRotation.x, _originalRotation.y,
                _originalRotation.z + _zRotation), _rotationInTime).SetEase(_rotationInAnimationCurve).SetUpdate(true);
        transform.DOScale(new Vector3(_hoverScale, _hoverScale), _hoverScaleDuration).SetEase(_scaleInAnimationCurve).SetUpdate(true);
    }

    public void OnDeselect()
    {
        transform.DOKill();
        transform.DOLocalRotate(
            new Vector3(_originalRotation.x, _originalRotation.y,
                _originalRotation.z), _rotationOutTime).SetEase(_rotationOutAnimationCurve).SetUpdate(true);
        transform.DOScale(new Vector3(_originalScale, _originalScale), _notHoverScaleDuration).SetEase(_scaleOutAnimationCurve).SetUpdate(true);
    }
}