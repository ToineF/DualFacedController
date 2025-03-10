using UnityEngine;

namespace Cattac.Character.Visuals
{
    public class HeadStretchFeedback : MonoBehaviour
    {
        [SerializeField] private CharacterHead _characterHead;
        [SerializeField] private ParticleSystem _VFX;
        [SerializeField] private Animator _walkAnimator;

        private bool _isStreched;

        private void Update()
        {
            var currentStrech = _characterHead.IsMovementBlocked;
            if (_isStreched == currentStrech) return;

            _isStreched = currentStrech;
            if (currentStrech) _VFX.Play();
            else _VFX.Stop();
            _walkAnimator.speed = currentStrech ? 2 : 1;
        }
    }
}