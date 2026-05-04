using UnityEngine;

namespace Menu.Options
{
    /// <summary>
    /// Handles the audio options, using by buttons in the options menu in the inspector
    /// </summary>
    public class AudioOptions : MonoBehaviour
    {
        [SerializeField] private SFXPlayer _sfxPreview;

        private const float _timerDelay = 0.125f;
        private float _timer = 1f;
        
        public void TryPlayPreview()
        {
            if (_timer > 0f) return;
            
            _timer = _timerDelay;
            _sfxPreview.Play();
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
        }
    }
}