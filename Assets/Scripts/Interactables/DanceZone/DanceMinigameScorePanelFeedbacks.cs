using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Cattac.Interactables
{
    public class DanceMinigameScorePanelFeedbacks : MonoBehaviour
    {
        [SerializeField] private DanceMinigameScorePanel _scorePanel;

        [Header("Highlight Punch Scale")] [SerializeField]
        private Vector3 _highlightPunchScale;

        [SerializeField] private float _highlightPunchTime;
        [SerializeField] private Ease _highlightPunchEase;

        [Header("Appear Punch Position")] [SerializeField]
        private float _appearStartYPosition;

        [SerializeField] private float _appearYPosition;
        [SerializeField] private float _appearPunchTime;
        [SerializeField] private Ease _appearPunchEase;
        [SerializeField] private float _timeBetweenHighlights;
        [SerializeField] private float _hightlightsStartDelay;
        
        [Header("End")]
        [SerializeField] private float _endTimeBetweenHighlights;


        private void OnEnable()
        {
            _scorePanel.OnHighlight += OnHighlight;
            _scorePanel.OnAppear += OnAppear;
            _scorePanel.OnEnd += OnEnd;
        }

        private void OnDisable()
        {
            _scorePanel.OnHighlight -= OnHighlight;
            _scorePanel.OnAppear -= OnAppear;
            _scorePanel.OnEnd -= OnEnd;
        }

        private void OnHighlight(int index)
        {
            var highlight = _scorePanel.Highlights[index].transform.parent.parent;

            highlight.DOComplete();
            highlight.DOPunchScale(_highlightPunchScale, _highlightPunchTime).SetEase(_highlightPunchEase);
        }

        private void OnAppear()
        {
            StartCoroutine(AppearCoroutine());
        }

        private IEnumerator AppearCoroutine()
        {
            foreach (var highlight in _scorePanel.Highlights)
            {
                var targetPosition = highlight.transform.parent.parent.localPosition;
                targetPosition.z = _appearStartYPosition;
                highlight.transform.parent.parent.localPosition = targetPosition;
            }

            yield return new WaitForSeconds(_hightlightsStartDelay);

            foreach (var highlight in _scorePanel.Highlights)
            {
                highlight.transform.parent.parent.DOLocalMoveZ(_appearYPosition, _appearPunchTime).SetEase(_appearPunchEase);

                yield return new WaitForSeconds(_timeBetweenHighlights);
            }
        }
        
        private void OnEnd(bool win)
        {
            StartCoroutine(EndCoroutine(win));

        }
        
        private IEnumerator EndCoroutine(bool win)
        {
            for (int i = 0; i < _scorePanel.Highlights.Length; i++)
            {
                var highlight = _scorePanel.Highlights[i].transform.parent.parent;
                
                if (_scorePanel.WinByHighlight[i] == win)
                {
                    highlight.DOComplete();
                    highlight.DOPunchScale(_highlightPunchScale, _highlightPunchTime).SetEase(_highlightPunchEase);
                    
                    yield return new WaitForSeconds(_endTimeBetweenHighlights);
                }
            }
        }
    }
}