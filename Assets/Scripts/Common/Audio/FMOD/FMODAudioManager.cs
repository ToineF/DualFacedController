using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using NotImplementedException = System.NotImplementedException;

namespace FMOD
{
    public class FMODAudioManager : MonoBehaviour
    {
        public static FMODAudioManager Instance;

        // TODO : Set as a scriptable object later
        [Header("References")] [SerializeField]
        private EventReference[] _musicReferences;

        private EventInstance _musicInstance;

        private int _currentIndex = 0;

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
            //eventInstance.release();
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
    }
}