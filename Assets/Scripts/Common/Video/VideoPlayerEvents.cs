using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace AntoineFoucault.Utilities.Video
{
    public class VideoPlayerEvents : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private VideoPlayer _videoPlayer;
        
        [Header("Events")]
        [SerializeField] private UnityEvent _startEvent;
        [SerializeField] private UnityEvent _endEvent;

        private void Awake()
        {
            _videoPlayer.started += (videoPlayer) => _startEvent?.Invoke();
            _videoPlayer.loopPointReached += (videoPlayer) => _endEvent?.Invoke();
        }
    }
}