using System;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

namespace FMOD
{
    public class FMODAudioManager : MonoBehaviour
    {
        public static FMODAudioManager Instance;

        [Header("References")] [SerializeField] private EventReference[] _musicReferences;
        [SerializeField] private EventReference _pauseMenuFilter;
        
        private EventInstance _musicInstance;
        private EventInstance _pauseMenuSnapshot;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            _pauseMenuSnapshot = FMODUnity.RuntimeManager.CreateInstance(_pauseMenuFilter);
        }

        public void PlayClip(EventReference eventPath, Vector3 position = default)
        {
            if (eventPath.IsNull) return;
            RuntimeManager.PlayOneShot(eventPath, position);
        }

        public void PlayMusic(GameMusic gameMusic)
        {
            PlayMusic(_musicReferences[(int)gameMusic]);
        }

        public void PlayMusic(EventReference eventReference)
        {
            // Polish transitions later
            _musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _musicInstance.release();
            _musicInstance = RuntimeManager.CreateInstance(eventReference);
            _musicInstance.start();
        }

        public void StopMusic()
        {
            _musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        public void SetParameter(string parameterName, int amount)
        {
            _musicInstance.setParameterByName(parameterName, amount);
        }
        
        public void PlayEQFilter()
        {
            _pauseMenuSnapshot.start();
        }

        public void StopEQFilter()
        {
            _pauseMenuSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}