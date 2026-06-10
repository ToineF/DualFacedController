    using DG.Tweening;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.Serialization;
    using UnityEngine.UI;
    
namespace MaskTransitions
{
    public class TransitionManager : MonoBehaviour
    {
        public static TransitionManager Instance;
        
        [Header("Transition Properties")]
        [SerializeField] private Sprite _transitionImage;
        [SerializeField] private Color _transitionColor;
        [SerializeField] private bool _rotation;
        [Tooltip("Time taken for one half of the transition to complete")] [SerializeField] private float _transitionTime;

        [Header("Image Components")]
        [SerializeField] private RectTransform _parentMaskRect;
        [SerializeField] private RectTransform _maskRect;
        [SerializeField] private RectTransform _transitionCanvas;
        [SerializeField] private Image _parentMaskImage;
        [SerializeField] private CutoutMaskUI _cutoutMask;
        [SerializeField] private float _maxSizeMultiplier = 0.5f;
        
        [Header("Loading")]
        [SerializeField] private CanvasGroup _loadingUI;
        [SerializeField] private float _loadFadeTime = 0.4f;
        
        private static float _maxSize { get; set; }
        private float _screenWidth;
        private float _screenHeight;
        private float _individualTransitionTime;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            // Assign the transition sprite and color
            _parentMaskImage.sprite = _transitionImage;
            _cutoutMask.sprite = _transitionImage;
            _cutoutMask.color = _transitionColor;

            _individualTransitionTime = _transitionTime / 2;

            SetupMaxSize();
        }

        #region Setup
        void SetupMaxSize()
        {
            _screenWidth = _transitionCanvas.rect.width;
            _screenHeight = _transitionCanvas.rect.height;

            _maxSize = Mathf.Max(_screenWidth, _screenHeight);
            _maxSize += _maxSize * _maxSizeMultiplier;
        }

        void StartAnimation(float? totalTime = null)
        {
            float animationTime = totalTime ?? _individualTransitionTime;

            _maskRect.sizeDelta = Vector2.zero;
            _parentMaskRect.sizeDelta = Vector2.zero;

            _maskRect.DOSizeDelta(new Vector2(_maxSize, _maxSize), animationTime).SetEase(Ease.InOutQuad);
            if (_rotation)
                _maskRect.DORotate(new Vector3(0, 0, 180), animationTime, RotateMode.FastBeyond360).SetEase(Ease.InOutQuad);
        }

        Tween StartAnimationForLoad(float? totalTime = null)
        {
            float animationTime = totalTime ?? _individualTransitionTime;

            _maskRect.sizeDelta = Vector2.zero;
            _parentMaskRect.sizeDelta = Vector2.zero;
            _maskRect.rotation = Quaternion.identity;

            Tween blueTweenSize = _maskRect.DOSizeDelta(new Vector2(_maxSize, _maxSize), animationTime).SetEase(Ease.InOutQuad);

            Sequence animationSequence = DOTween.Sequence().Join(blueTweenSize);

            if (_rotation)
            {
                Tween blueTweenRotate = _maskRect.DORotate(new Vector3(0, 0, 180), animationTime).SetEase(Ease.InOutQuad);
                animationSequence.Join(blueTweenRotate);
            }

            return animationSequence;
        }


        void EndAnimation(float? totalTime = null)
        {
            float animationTime = totalTime ?? _individualTransitionTime;

            _maskRect.sizeDelta = new Vector2(_maxSize, _maxSize);
            _parentMaskRect.sizeDelta = Vector2.zero;
            _parentMaskRect.rotation = Quaternion.identity;

            _parentMaskRect.DOSizeDelta(new Vector2(_maxSize, _maxSize), animationTime).SetEase(Ease.InOutQuad);
            if (_rotation) _parentMaskRect.DORotate(new Vector3(0, 0, 180), animationTime).SetEase(Ease.InOutQuad);
        }
        #endregion

        #region Transition Without Scene Load
        public void PlayTransition(float transitionTime, float startDelay = 0f)
        {
            StartCoroutine(PlayTransitionWithDelay(transitionTime, startDelay));
        }

        IEnumerator PlayTransitionWithDelay(float transitionTime, float startDelay)
        {
            float dividedTime = transitionTime / 3;

            //Optional Delay
            yield return new WaitForSeconds(startDelay);

            StartAnimation(dividedTime);
            yield return new WaitForSeconds(dividedTime);
            EndAnimation(dividedTime);
        }
        #endregion

        #region Transition With Scene Load 
        public void LoadLevel(string sceneName, float delay = 0f)
        {
            StartCoroutine(LoadLevelWithWait(sceneName, delay));
        }
        
        public void LoadLevel(int sceneIndex, float delay = 0f)
        {
            StartCoroutine(LoadLevelWithWait(sceneIndex, delay));
        }

        IEnumerator LoadLevelWithWait(string sceneName, float delay)
        {
            yield return new WaitForSeconds(delay);

            Tween animationTween = StartAnimationForLoad();
            
            // Wait for the animation to complete
            yield return animationTween.WaitForCompletion();

            _loadingUI.DOFade(1, _loadFadeTime);
            yield return new WaitForSeconds(_loadFadeTime);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            _loadingUI.DOFade(0, _loadFadeTime);
            EndAnimation();
        }
        
        IEnumerator LoadLevelWithWait(int sceneIndex, float delay)
        {
            yield return new WaitForSeconds(delay);

            Tween animationTween = StartAnimationForLoad();

            // Wait for the animation to complete
            yield return animationTween.WaitForCompletion();

            _loadingUI.DOFade(1, _loadFadeTime);
            yield return new WaitForSeconds(_loadFadeTime);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);


            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            _loadingUI.DOFade(0, _loadFadeTime);
            EndAnimation();
        }
        #endregion

        #region Play Partial Transitions
        public void PlayStartHalfTransition(float transitionTime, float startDelay = 0f)
        {
            StartCoroutine(PlayStartHalfTransitionWithDelay(transitionTime, startDelay));
        }
        public void PlayEndHalfTransition(float transitionTime, float startDelay = 0f)
        {
            StartCoroutine(PlayEndHalfTransitionWithDelay(transitionTime, startDelay));
        }
        IEnumerator PlayStartHalfTransitionWithDelay(float transitionTime, float startDelay)
        {
            yield return new WaitForSeconds(startDelay);
            StartAnimation(transitionTime);
        }
        IEnumerator PlayEndHalfTransitionWithDelay(float transitionTime, float startDelay)
        {
            yield return new WaitForSeconds(startDelay);
            EndAnimation(transitionTime);
        }
        #endregion
    }
}

