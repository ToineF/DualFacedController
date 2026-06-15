using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace SpecialInteractions
{
    public class ButtonAppearAnimatorGroup : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform[] _buttonAppearAnimators;
        [SerializeField] private SubMenu _subMenu;
        
        [Header("Tween")]
        [SerializeField] private Vector2 _randomWaitTime;
        [SerializeField] private float _tweenDuration;
        [SerializeField] private Ease _tweenEase;

        private void OnEnable()
        {
            _subMenu.OnOpen += OnOpen;
        }
        
        private void OnDisable()
        {
            _subMenu.OnOpen -= OnOpen;
        }

        private void OnOpen()
        {
            StopAllCoroutines();
            StartCoroutine(WaitForAnimation());
        }

        private IEnumerator WaitForAnimation()
        {
            foreach (var buttonAppearTransform in _buttonAppearAnimators)
            {
                buttonAppearTransform.DOKill();
                buttonAppearTransform.localScale = Vector3.zero;
            }

            foreach (var buttonAppearTransform in _buttonAppearAnimators)
            {
                yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(_randomWaitTime.x, _randomWaitTime.y));

                buttonAppearTransform.DOScale(Vector3.one, _tweenDuration).SetEase(_tweenEase).SetUpdate(true);
            }
        }
    }
}