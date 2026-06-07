using System.Collections;
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
        [SerializeField] private float _hideDuration = .6f;
        
        [Header("Delay")]
        [SerializeField] private float _showDelay;
        [SerializeField] private float _hideDelay;
        
        [Header("Text")] [SerializeField] private TMP_Text _mouseName;
        [SerializeField] private CanvasGroup[] _canvasGroups;

        private void Start()
        {
            MainGame.Instance.CollectiblesManager.MouseCollectibleManager.OnMouseGet += Show;
            MainGame.Instance.CollectiblesManager.MouseCollectibleManager.OnMouseHide += Hide;
        }

        private void Show(SavedMouseData cageData, float duration, Ease ease)
        {
            StartCoroutine(ShowRoutine(cageData, duration, ease));
        }

        private IEnumerator ShowRoutine(SavedMouseData cageData, float duration, Ease ease)
        {
            yield return new WaitForSeconds(_showDelay);

            _fadeImage.DOFade(_showAlpha, duration).SetEase(ease);
            foreach (var canvasGroup in _canvasGroups)
            {
                canvasGroup.DOFade(1, duration).SetEase(ease);
            }

            _mouseName.text = cageData.Name;
        }

        private void Hide()
        {
            StartCoroutine(HideRoutine());
        }

        private IEnumerator HideRoutine()
        {
            yield return new WaitForSeconds(_hideDelay);
            
            _fadeImage.DOFade(0, _hideDuration);
            foreach (var canvasGroup in _canvasGroups)
            {
                canvasGroup.DOFade(0, _hideDuration);
            }
        }
    }
}