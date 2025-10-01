using System.Collections;
using Cattac.Character.Multiplayer;
using DG.Tweening;
using FeedbacksEditor;
using UnityEngine;

namespace Cattac.Interactables
{
    public class CageFeedbacks : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Cage _cage;
        [SerializeField] private Animator _mouseAnimator;
        
        [Header("Apparition")]
        [SerializeField] private Ease _rollEase;
        [SerializeField] private string _landAnimation;
        [SerializeField] private float _duration = 2f;
        [SerializeField] private GameEvent _landFeedback;
        
        [Header("Disparition")]
        [SerializeField] private string _hideAnimation;
        [SerializeField] private float _hideBeforeTime = 4f;
        
        private void Start()
        {
            _cage.OnGetCage += OnGetCage;
        }

        private void OnGetCage()
        {
            _mouseAnimator.enabled = true;
            var parent = MainGame.Instance.CollectiblesManager.MouseCameraParent;
            transform.SetParent(parent, true);
            transform.DOLocalMove(Vector3.zero, _duration).SetEase(_rollEase);
            transform.DOLocalRotate(Vector3.zero, _duration).SetEase(_rollEase).OnComplete(Land);
            
            MainGame.Instance.CollectiblesManager.MouseCollectibleManager.OnMouseGet?.Invoke(_cage, _duration, _rollEase);
            MainGame.Instance.PlayersManager.SetInput(InputType.CUTSCENE);
        }

        private void Land()
        {
            _mouseAnimator.SetTrigger(_landAnimation);
            GameEventsManager.PlayEvent(_landFeedback, gameObject);
            
            StartCoroutine(WaitForHide());
        }

        private IEnumerator WaitForHide()
        {
            yield return new WaitForSeconds(_hideBeforeTime);
            Hide();
        }

        private void Hide()
        {
            _mouseAnimator.SetTrigger(_hideAnimation);
            MainGame.Instance.CollectiblesManager.MouseCollectibleManager.OnMouseHide?.Invoke(_cage);
            MainGame.Instance.PlayersManager.SetInput(InputType.CUTSCENE_RESUME);
        }
    }
}