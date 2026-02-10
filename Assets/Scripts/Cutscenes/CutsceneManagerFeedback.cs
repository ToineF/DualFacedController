using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Cattac.Cutscenes
{
    public class CutsceneManagerFeedback : MonoBehaviour
    {
        [SerializeField] private Image[] _bands;
        [SerializeField] private float _fadeTime;
        
        private void Start()
        {
            CutsceneManager.Instance.OnCutsceneStart += OnCutsceneStart;
            CutsceneManager.Instance.OnCutsceneEnd += OnCutsceneEnd;
        }

        private void OnCutsceneStart()
        {
            foreach (var band in _bands)
            {
                band.DOFade(1, _fadeTime);
            }
        }

        private void OnCutsceneEnd()
        {
            foreach (var band in _bands)
            {
                band.DOFade(0, _fadeTime);
            }
        }
    }
}