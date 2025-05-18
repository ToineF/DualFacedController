using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Cattac.Collectibles
{
    public class CollectiblesUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _collectiblesUI;

        [Header("Visibility")]
        [SerializeField] private float _cheeseStayFadeTime;
        [SerializeField] private float _miceStayFadeTime;

        [Header("Cheese")]
        [SerializeField] private Animator _cheeseAnimator;
        [SerializeField] private TMP_Text _cheesesCountText;
        [SerializeField] private Transform[] _cheesesTransforms;

        [Header("Mices")]
        [SerializeField] private Animator _miceParentAnimator;
        [SerializeField] private Transform _miceUIParent;
        [SerializeField] private Animator _miceImagePrefab;

        [Header("Feedbacks")]
        [SerializeField] private AntoineFoucault.Utilities.Tween.DoTweenPunchFeedback _cheeseFeedbacks;

        private CollectiblesManager _collectiblesManager;
        private LevelCollectiblesData _levelCollectiblesData;
        
        private Animator[] _miceAnimators;
        private Coroutine _miceCoroutine;
        private Coroutine _cheeseCoroutine;
        private bool _isVisible;
        private bool _highPriority;

        private void Start()
        {
            _collectiblesManager = MainGame.Instance.CollectiblesManager;
            _levelCollectiblesData = MainGame.Instance.LevelCollectiblesData;
            
            _collectiblesManager.OnCheeseGain += UpdateCheeseUI;
            _levelCollectiblesData.OnMouseGain += UpdateMouseUI;

            InitCheeseUI();
            InitMouseUI();
        }

        private void InitCheeseUI()
        {
            _cheesesCountText.text = _collectiblesManager.Cheeses.ToString("D3");
        }

        private void UpdateCheeseUI()
        {
            if (_cheesesCountText == null) return;

            foreach (var cheesesTransform in _cheesesTransforms)
            {
                cheesesTransform.transform.DOComplete();
                cheesesTransform.transform.DOPunchPosition(_cheeseFeedbacks.PunchDirection, _cheeseFeedbacks.PunchTime, _cheeseFeedbacks.PunchVibrato, _cheeseFeedbacks.PunchElasticity);
            }
            _cheesesCountText.text = _collectiblesManager.Cheeses.ToString("D3");
            _cheeseAnimator.SetBool("isVisible", true);
            ShowUICollectible(_cheeseAnimator, _cheeseStayFadeTime);
        }

        private void InitMouseUI()
        {
            _miceAnimators = new Animator[_levelCollectiblesData.Mice.Count];
            for (int i = 0; i < _miceAnimators.Length; i++)
            {
                _miceAnimators[i] = Instantiate(_miceImagePrefab, _miceUIParent);
            }
        }
        
        private void UpdateMouseUI(int index)
        {
            if (index < 0 || index >= _miceAnimators.Length) return;

            _miceAnimators[index].SetTrigger("isVisible");
            _miceParentAnimator.SetBool("isVisible", true);
            ShowUICollectible(_miceParentAnimator, _miceStayFadeTime);
        }
        
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
            if (animator == _cheeseAnimator) StartCoroutine(ref _cheeseCoroutine, animator, time, isVisible);
            else StartCoroutine(ref _miceCoroutine, animator, time, isVisible);
        }

        private void StartCoroutine(ref Coroutine coroutine, Animator animator, float time, bool isVisible)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(ShowHideUIAfterTimeRoutine(animator, time, isVisible));
        }

        private void ShowHideUIImmediate(bool isVisible, bool highPriority = false)
        {
            if (isVisible == _isVisible && !highPriority) return;
            if (_highPriority && !highPriority) return;
            _isVisible = isVisible;

            //_collectiblesUI.DOFade(isVisible ? 1 : 0, isVisible ? _appearFadeTime : _disappearFadeTime);
        }

        private IEnumerator ShowHideUIAfterTimeRoutine(Animator animator, float time, bool isVisible)
        {
            yield return new WaitForSeconds(time);

            //_collectiblesUI.DOFade(isVisible ? 1 : 0, isVisible ? _appearFadeTime : _disappearFadeTime);
            animator.SetBool("isVisible", isVisible);
            _highPriority = false;
        }
    }
}