using Cattac.Character;
using DG.Tweening;
using FeedbacksEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Emotes
{
    public class CharacterEmotesManager : MonoBehaviour
    {
        [SerializeField] private CharacterHead _head;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private GameEvent[] _feedbacks;
        
        [Header("Time")]
        [SerializeField] private float _appearTime;
        [SerializeField] private Ease _appearEase;
        [SerializeField] private float _stayTime;
        [SerializeField] private float _disappearTime;
        [SerializeField] private Ease _disappearEase;

        private int _lastEmoteIndex;
        private Sequence _sequence;

        private void Start()
        {
            transform.localScale = Vector3.zero;
        }

        private void Update()
        {
            CheckForInput();
        }

        private void CheckForInput()
        {
            var head = UserInput.Instance.GetHead(_head.IsLeftHead);
            if (head == null) return;
            
            var emoteDirection = head.EmoteDirection;
            emoteDirection.Normalize();
            if ((emoteDirection.x == 0 ^ emoteDirection.y == 0) == false) return; // Only one is zero

            int emoteIndex = 0;
            if (Mathf.Approximately(emoteDirection.x, -1)) emoteIndex = 0;
            else if (Mathf.Approximately(emoteDirection.x, 1)) emoteIndex = 1;
            else if (Mathf.Approximately(emoteDirection.y, -1)) emoteIndex = 2;
            else if (Mathf.Approximately(emoteDirection.y, 1)) emoteIndex = 3;

            if (emoteIndex == _lastEmoteIndex) return;
            _lastEmoteIndex =  emoteIndex;
            UpdateIndex();
        }

        private void UpdateIndex()
        {
            _image.sprite = _sprites[_lastEmoteIndex];
            GameEventsManager.PlayEvent(_feedbacks[_lastEmoteIndex], _image.gameObject);
            
            if (_sequence != null && _sequence.IsActive())
            {
                _sequence.Kill(); // This stops and disposes the previous sequence
            }
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOScale(1f, _appearTime).SetEase(_appearEase));
            _sequence.AppendInterval(_stayTime);
            _sequence.Append(transform.DOScale(0f, _disappearTime).SetEase(_disappearEase));
            _sequence.OnComplete(() => _lastEmoteIndex = -1);
            _sequence.Play();
        }
    }
}