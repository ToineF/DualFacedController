using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using FeedbacksEditor;

namespace Cattac.Collectibles
{
    public class CollectiblesUI : MonoBehaviour
    {
        [SerializeField] private bool _useFeedbacks = true;
        
        [Header("Cheese")] [SerializeField] private float _cheeseStayFadeTime;
        [SerializeField] private Animator _cheeseAnimator;
        [SerializeField] private TMP_Text _cheesesCountText;
        [SerializeField] private Transform[] _moveCheesesTransforms;
        [SerializeField] private Transform[] _scaleCheesesTransforms;
        [SerializeField] private AntoineFoucault.Utilities.Tween.DoTweenPunchFeedback _moveCheeseFeedbacks;
        [SerializeField] private AntoineFoucault.Utilities.Tween.DoTweenPunchFeedback _scaleCheeseFeedbacks;

        [Header("Mices")] [SerializeField] private float _miceStayFadeTime;
        [SerializeField] private float _micesUIAppearDelay;
        [SerializeField] private float _waitTimeBetweenMiceAppear;
        [SerializeField] private float _waitTimeBetweenMiceDisappear;
        [SerializeField] private Animator _miceParentAnimator;
        [SerializeField] private Transform _miceUIParent;
        [SerializeField] private Animator _miceImagePrefab;
        [SerializeField] private AntoineFoucault.Utilities.Tween.DoTweenPunchFeedback _appearMouseScaleFeedback;
        [SerializeField] private AntoineFoucault.Utilities.Tween.DoTweenPunchFeedback _disappearMouseRotateFeedback;
        [SerializeField] private AntoineFoucault.Utilities.Tween.DoTweenPunchFeedback _disappearMouseScaleFeedback;
        [SerializeField] private GameEvent _mouseUIIconAppearEvent;

        private LevelCollectiblesData _levelCollectiblesData;
        private bool _isVisible;
        private bool _highPriority;
        private readonly int _animatorVisibility = Animator.StringToHash("isVisible");

        private CheeseCollectibleManager _cheeseCollectibleManager;
        private Coroutine _cheeseCoroutine;

        private Animator[] _miceAnimators;
        private Coroutine _miceCoroutine;

        private void Start()
        {
            _cheeseCollectibleManager = MainGame.Instance.CollectiblesManager.CheeseCollectiblesManager;
            _levelCollectiblesData = MainGame.Instance.LevelCollectiblesData;

            _cheeseCollectibleManager.OnCheeseGain += UpdateCheeseUI;
            _levelCollectiblesData.OnMouseGain += UpdateMouseUI;

            InitCheeseUI();
            InitMouseUI();
        }

        #region Cheeses

        private void InitCheeseUI()
        {
            _cheesesCountText.text = _cheeseCollectibleManager.Cheeses.ToString("D3");
        }

        private void UpdateCheeseUI(bool hasFeedbacks)
        {
            if (_cheesesCountText == null) return;

            _cheesesCountText.text = _cheeseCollectibleManager.Cheeses.ToString("D3");

            if (_useFeedbacks && hasFeedbacks)
            {
                foreach (var cheesesTransform in _moveCheesesTransforms)
                {
                    cheesesTransform.transform.DOComplete();
                    cheesesTransform.transform.DOPunchPosition(_moveCheeseFeedbacks.PunchDirection,
                        _moveCheeseFeedbacks.PunchTime, _moveCheeseFeedbacks.PunchVibrato,
                        _moveCheeseFeedbacks.PunchElasticity);
                }

                foreach (var cheesesTransform in _scaleCheesesTransforms)
                {
                    cheesesTransform.transform.DOComplete();
                    cheesesTransform.transform.DOPunchScale(_scaleCheeseFeedbacks.PunchDirection,
                        _scaleCheeseFeedbacks.PunchTime, _scaleCheeseFeedbacks.PunchVibrato,
                        _scaleCheeseFeedbacks.PunchElasticity);
                }

                if (_cheeseAnimator) _cheeseAnimator.SetBool(_animatorVisibility, true);
                ShowUICollectible(_cheeseAnimator, _cheeseStayFadeTime);
            }
        }

        #endregion

        #region Mices

        private void InitMouseUI()
        {
            _miceAnimators = new Animator[_levelCollectiblesData.Mice.Count];
            for (int i = 0; i < _miceAnimators.Length; i++)
            {
                _miceAnimators[i] = Instantiate(_miceImagePrefab, _miceUIParent, false);
                if (_useFeedbacks) _miceAnimators[i].transform.localScale = Vector3.zero;
            }
        }

        private async void UpdateMouseUI(int index, bool hasFeedbacks)
        {
            if (index < 0 || index >= _miceAnimators.Length) return;

            await Task.Delay((int)(_micesUIAppearDelay * 1000));


            if (_useFeedbacks && hasFeedbacks)
            {
                //if (_miceParentAnimator) _miceParentAnimator.SetBool(_animatorVisibility, true);
                foreach (var animator in _miceAnimators)
                {
                    await Task.Delay((int)(_waitTimeBetweenMiceAppear * 1000));
                    animator.transform.DOComplete();
                    animator.transform
                        .DOScale(_appearMouseScaleFeedback.PunchDirection, _appearMouseScaleFeedback.PunchTime)
                        .SetEase(_appearMouseScaleFeedback.Ease);
                    //animator.Play("CageMouseIcon_Appear");
                    GameEventsManager.PlayEvent(_mouseUIIconAppearEvent, animator.gameObject);
                }
            }

            _miceAnimators[index].SetTrigger(_animatorVisibility);

            if (_useFeedbacks && hasFeedbacks)
            {
                //ShowUICollectible(_miceParentAnimator, _miceStayFadeTime);
                await Task.Delay((int)(_miceStayFadeTime * 1000));
                foreach (var animator in _miceAnimators)
                {
                    await Task.Delay((int)(_waitTimeBetweenMiceDisappear * 1000));
                    animator.transform
                        .DOScale(_disappearMouseScaleFeedback.PunchDirection, _disappearMouseScaleFeedback.PunchTime)
                        .SetEase(_disappearMouseScaleFeedback.Ease);
                    animator.transform
                        .DOLocalRotate(_disappearMouseRotateFeedback.PunchDirection, _disappearMouseRotateFeedback.PunchTime,
                            RotateMode.FastBeyond360).SetEase(_disappearMouseRotateFeedback.Ease);
                    //animator.Play("CageMouseIcon_Disappear");
                }
            }
        }

        #endregion

        #region Generic

        private void ShowUICollectible(Animator animator, float stayFadeTime)
        {
            _highPriority = true;
            ShowHideUIImmediate(true, true);
            ShowHideUIAfterTime(animator, stayFadeTime, false, true);
        }

        private void ShowHideUIAfterTime(Animator animator, float time, bool isVisible, bool highPriority = false)
        {
            if (isVisible == _isVisible && !highPriority) return;
            if (_highPriority && !highPriority) return;
            _isVisible = isVisible;
            if (animator == _cheeseAnimator)
                StartCollectiblesCoroutine(ref _cheeseCoroutine, animator, time, isVisible);
            else StartCollectiblesCoroutine(ref _miceCoroutine, animator, time, isVisible);
        }

        private void StartCollectiblesCoroutine(ref Coroutine coroutine, Animator animator, float time, bool isVisible)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(ShowHideUIAfterTimeRoutine(animator, time, isVisible));
        }

        private void ShowHideUIImmediate(bool isVisible, bool highPriority = false)
        {
            if (isVisible == _isVisible && !highPriority) return;
            if (_highPriority && !highPriority) return;
            _isVisible = isVisible;
        }

        private IEnumerator ShowHideUIAfterTimeRoutine(Animator animator, float time, bool isVisible)
        {
            yield return new WaitForSeconds(time);

            if (animator) animator.SetBool(_animatorVisibility, isVisible);
            _highPriority = false;
        }

        #endregion
    }
}