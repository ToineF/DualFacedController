using UnityEngine;

namespace FMOD
{
    public class FMODMusicPlayer : MonoBehaviour
    {
        [SerializeField] private GameMusic _targetMusic;
        [SerializeField] private StartBehaviour _startBehaviour = StartBehaviour.PLAY;

        private void Start()
        {
            switch (_startBehaviour)
            {
                case StartBehaviour.PLAY:
                    Play();
                    break;
                case StartBehaviour.STOP:
                    Stop();
                    break;
            }          
        }

        public void Play() => FMODAudioManager.Instance.PlayMusic(_targetMusic);
        public void Stop() => FMODAudioManager.Instance.StopMusic();
        
        private enum StartBehaviour
        {
            NONE = 0,
            PLAY = 1,
            STOP = 2,
        }
    }
}