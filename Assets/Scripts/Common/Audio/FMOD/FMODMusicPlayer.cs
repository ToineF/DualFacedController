using UnityEngine;

namespace FMOD
{
    public class FMODMusicPlayer : MonoBehaviour
    {
        [SerializeField] private GameMusic _targetMusic;
        [SerializeField] private bool _playOnStart;

        private void Start()
        {
            if (_playOnStart) Play();
        }

        public void Play() => FMODAudioManager.Instance.PlayMusic(_targetMusic);
    }
}