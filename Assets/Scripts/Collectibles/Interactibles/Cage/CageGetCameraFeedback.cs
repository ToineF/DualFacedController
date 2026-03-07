using Cattac.Interactables.MouseCollection;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cattac.Interactables
{
    public class CageGetCameraFeedback : MonoBehaviour
    {
        [Header("Fade")]
        [SerializeField] private Image _fadeImage;
        [SerializeField] private float _showAlpha = .6f;
        [SerializeField] private float _hideDuration =.6f;
        
        [Header("Text")]
        [SerializeField] private TMP_Text _mouseName;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Start()
        {
            MainGame.Instance.CollectiblesManager.MouseCollectibleManager.OnMouseGet += Show;
            MainGame.Instance.CollectiblesManager.MouseCollectibleManager.OnMouseHide += Hide;
        }

        private void Show(SavedMouseData cageData, float duration, Ease ease)
        {
            _fadeImage.DOFade(_showAlpha, duration).SetEase(ease);
            _canvasGroup.DOFade(1, duration).SetEase(ease);
            _mouseName.text = cageData.Name;
        }
        
        private void Hide()
        {
            _fadeImage.DOFade(0, _hideDuration);
            _canvasGroup.DOFade(0, _hideDuration);
        }
    }
}