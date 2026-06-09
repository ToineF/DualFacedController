using UnityEngine;

namespace Cattac.Interactables
{
    public class BallPushFeedback : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _system;
        [SerializeField] private AudioSource _audioSource;
        
        private const float _speedThreshold = 0.01f;

        private void Update()
        {
            var isMoving = _rigidbody.linearVelocity.sqrMagnitude > _speedThreshold;
            if (isMoving)
            {
                if (_system.isPlaying == false) _system.Play();
                _audioSource.volume = 1;
            }
            else
            {
                _system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                _audioSource.volume = 0;
            }
        }
    }
}