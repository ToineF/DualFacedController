using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Cattac.Interactables
{
    public class CageGetCameraFeedback : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;
        [SerializeField] private float _showAlpha = .6f;
        [SerializeField] private float _hideDuration =.6f;

        private void Start()
        {
            MainGame.Instance.CollectiblesManager.MouseCollectibleManager.OnMouseGet += Show;
            MainGame.Instance.CollectiblesManager.MouseCollectibleManager.OnMouseHide += Hide;
        }

        private void Show(Cage cage, float duration, Ease ease)
        {
            _fadeImage.DOFade(_showAlpha, duration).SetEase(ease);
        }
        
        private void Hide(Cage cage)
        {
            _fadeImage.DOFade(0, _hideDuration);
        }
    }
}