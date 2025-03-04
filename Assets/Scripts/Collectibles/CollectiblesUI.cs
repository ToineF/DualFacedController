using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

namespace Cattac.Collectibles
{
    public class CollectiblesUI : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private CollectiblesManager _collectiblesManager;

        [SerializeField] private CanvasGroup _collectiblesUI;

        [Header("Visibility")] [SerializeField]
        private float _hideTime;

        [SerializeField] private float _appearFadeTime;
        [SerializeField] private float _disappearFadeTime;
        [SerializeField] private float _stayFadeTime;

        [Header("Children")]
        //[SerializeField] private Transform _childrenUIParent;
        //[SerializeField] private Image _childHiddenImagePrefab;
        //[SerializeField] private Image _childFoundImagePrefab;
        [SerializeField]
        private GameObject[] _childrenImages;

        [Header("Cheese")] [SerializeField] private Animator _cheeseAnimator;
        [SerializeField] private CanvasGroup _cheesesUI;
        [SerializeField] private TMP_Text _cheesesCountText;
        [SerializeField] private TMP_Text _maxCheesesCountText;

        [Header("Mices")] [SerializeField] private CanvasGroup _miceUI;
        [SerializeField] private TMP_Text _miceCountText;
        [SerializeField] private TMP_Text _maxMiceCountText;

        [Header("Feedbacks")] [SerializeField]
        private AntoineFoucault.Utilities.Tween.DoTweenPunchFeedback _cheeseFeedbacks;

        [SerializeField] private AntoineFoucault.Utilities.Tween.DoTweenPunchFeedback _miceFeedbacks;

        private Image[] _collectiblesImage;
        private Coroutine _currentVisibilityCoroutine;
        private bool _isVisible;
        private bool _highPriority;

        private void Start()
        {
            _collectiblesManager.OnCheeseGain += UpdateCheeseUI;
            _collectiblesManager.OnCheeseGainPreview += ShowUICollectible;
            _collectiblesManager.OnMouseGain += UpdateMouseUI;

            UpdateCheeseUI();
            UpdateMouseUI();
        }

        private void UpdateCheeseUI()
        {
            if (_cheesesCountText == null) return;

            _cheesesCountText.transform.DOComplete();
            _cheesesCountText.transform.DOPunchPosition(_cheeseFeedbacks.PunchDirection, _cheeseFeedbacks.PunchTime,
                _cheeseFeedbacks.PunchVibrato, _cheeseFeedbacks.PunchElasticity);
            _cheesesCountText.text = _collectiblesManager.Cheeses.ToString("D3");
            _cheeseAnimator.SetBool("isVisible", true);
            ShowUICollectible();
        }

        private void UpdateMouseUI()
        {
            if (_miceCountText == null) return;

            _miceCountText.transform.DOComplete();
            _miceCountText.transform.DOPunchPosition(_miceFeedbacks.PunchDirection, _miceFeedbacks.PunchTime,
                _miceFeedbacks.PunchVibrato, _miceFeedbacks.PunchElasticity);
            _miceCountText.text = _collectiblesManager.Mice.ToString();
            ShowUICollectible();
        }

        /*private void Update()
        {
            CheckForUIHide();
        }*/

        /*private void CheckForUIHide()
        {
            //if (_childrenManager.Manager.States.IsInState(_childrenManager.Manager.States.IdleState)) // Stop moving
            //{
            //ShowHideUIAfterTime(_hideTime, true);
            //}
            //else if (!_childrenManager.Manager.States.IsInState(_childrenManager.Manager.States.IdleState)) // Starts moving
            //{
            //if (_currentVisibilityCoroutine != null) ShowHideUIAfterTime(0, false);
            //}
        }*/

        private void ShowUICollectible()
        {
            _highPriority = true;
            ShowHideUIImmediate(true, true);
            ShowHideUIAfterTime(_stayFadeTime, false, true);
        }

        private void ShowHideUIAfterTime(float time, bool isVisible, bool highPriority = false)
        {
            if (isVisible == _isVisible && !highPriority) return;
            if (_highPriority && !highPriority) return;
            _isVisible = isVisible;
            if (_currentVisibilityCoroutine != null) StopCoroutine(_currentVisibilityCoroutine);
            _currentVisibilityCoroutine = StartCoroutine(ShowHideUIAfterTimeRoutine(time, isVisible));
        }

        private void ShowHideUIImmediate(bool isVisible, bool highPriority = false)
        {
            if (isVisible == _isVisible && !highPriority) return;
            if (_highPriority && !highPriority) return;
            _isVisible = isVisible;

            //_collectiblesUI.DOFade(isVisible ? 1 : 0, isVisible ? _appearFadeTime : _disappearFadeTime);
        }

        private IEnumerator ShowHideUIAfterTimeRoutine(float time, bool isVisible)
        {
            yield return new WaitForSeconds(time);

            //_collectiblesUI.DOFade(isVisible ? 1 : 0, isVisible ? _appearFadeTime : _disappearFadeTime);
            _cheeseAnimator.SetBool("isVisible", isVisible);
            _highPriority = false;
        }
    }
}