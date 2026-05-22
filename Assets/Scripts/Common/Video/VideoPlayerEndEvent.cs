using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace AntoineFoucault.Utilities.Video
{
    public class VideoPlayerEndEvents : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private float _framesBeforeEnd;
        
        [Header("Events")]
        [SerializeField] private UnityEvent _endEvent;

        private bool _hasEnded;

        private void Update()
        {
            if(_hasEnded == false && (ulong)(_videoPlayer.frame + 1) >= _videoPlayer.frameCount - _framesBeforeEnd)
            {
                _endEvent?.Invoke();
                _hasEnded = true;
            }
        }
    }
}