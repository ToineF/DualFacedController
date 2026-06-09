using UnityEngine;

namespace Cattac.Interactables
{
    public class BallPushFeedback : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _system;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _speedThreshold = 0.1f;

        private void Start()
        {
            AudioManager.Instance.VolumeManager.AddSFXSource(_audioSource);
        }

        private void Update()
        {
            var isMoving = _rigidbody.linearVelocity.sqrMagnitude > _speedThreshold;
            if (isMoving)
            {
                if (_system.isPlaying == false) _system.Play();
                _audioSource.mute = false;
            }
            else
            {
                _system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                _audioSource.mute = true;
            }
        }
    }
}