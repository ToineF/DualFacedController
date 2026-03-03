using AntoineFoucault.Utilities;
using UnityEngine;
using FMODUnity;

public class FMODAudioManager : MonoBehaviour
{
    public static FMODAudioManager Instance;
    private static EventReference _currentMusic;

    [SerializeField] private EventReference[] _targetMusics;
    [SerializeField] private FMODUnity.StudioEventEmitter _musicEmitter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);

            var _targetMusic = _targetMusics.GetRandomItem();
            if (_musicEmitter.EventReference.Guid != _targetMusic.Guid)
            {
                _musicEmitter.Stop();
                _musicEmitter.EventReference = _targetMusic;
                _musicEmitter.Play();
            }
            //Instance.PlayClip(_targetMusic);
        }
        else if (Instance != this)
        {
            //Instance.PlayClip(_targetMusic);
            var _targetMusic = _targetMusics.GetRandomItem();
            if (_musicEmitter.EventReference.Guid != _targetMusic.Guid)
            {
                _musicEmitter.Stop();
                // _musicEmitter.EventReference = _targetMusic;
                // _musicEmitter.Play();
            }
            Destroy(gameObject);
        }
    }

    public void PlayClip(EventReference eventPath, Vector3 position = default)
    {
        if (eventPath.IsNull) return;
        RuntimeManager.PlayOneShot(eventPath, position);
    }

    public void stopstopstop()
    {
        _musicEmitter.Stop();
    }


    public static bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    void Update()
    {
        if (_musicEmitter.IsPlaying())
        {
            Debug.Log("Emitter is playing");
        }
        else if (!_musicEmitter.IsPlaying())
        {
            Debug.Log("Emitter is not playing");
        }
    }
}