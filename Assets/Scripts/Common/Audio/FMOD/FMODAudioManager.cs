using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

public class FMODAudioManager : MonoBehaviour
{
    public static FMODAudioManager Instance;
    // private static EventReference _currentMusic;

    // [SerializeField] private EventReference[] _targetMusics;
    // [SerializeField] private FMODUnity.StudioEventEmitter _musicEmitter;
    public List<FMODUnity.StudioEventEmitter> emitters = new List<FMODUnity.StudioEventEmitter>();
    private EventInstance eventInstance;

    private int _currentIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);

            eventInstance = RuntimeManager.CreateInstance(emitters[0].EventReference);
            eventInstance.start();
            // emitters[_currentIndex].Play();
            // var _targetMusic = _targetMusics.GetRandomItem();
            // if (_musicEmitter.EventReference.IsNull || _musicEmitter.EventReference.Guid != _targetMusic.Guid)
            // {
            //     _musicEmitter.Stop();
            //     _musicEmitter.EventReference = _targetMusic;
            //     _musicEmitter.Play();
            // }
            //Instance.PlayClip(_targetMusic);

        }
        else if (Instance != this)
        {
            //Instance.PlayClip(_targetMusic);
            // var _targetMusic = _targetMusics.GetRandomItem();
            // if (_musicEmitter.EventReference.Guid != _targetMusic.Guid)
            // {
            //     _musicEmitter.Stop();
            //     // _musicEmitter.EventReference = _targetMusic;
            //     // _musicEmitter.Play();
            // }
            Destroy(gameObject);
        }
    }

    public void PlayClip(EventReference eventPath, Vector3 position = default)
    {
        if (eventPath.IsNull) return;
        RuntimeManager.PlayOneShot(eventPath, position);
    }

    // public void stopstopstop()
    // {
    //     emitters[_currentIndex].Stop();
    // }

    // private EventInstance _eventInstance;
    public void PlayMusicAt(int index)
    {
        _currentIndex++;
        _currentIndex %= emitters.Count;
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        eventInstance.release();
        eventInstance = RuntimeManager.CreateInstance(emitters[_currentIndex].EventReference);
        eventInstance.start();
        // emitters[_currentIndex].Stop();
        // _currentIndex = index;
        // emitters[index].Play();
    }


    // public static bool IsPlaying(FMOD.Studio.EventInstance instance)
    // {
    //     FMOD.Studio.PLAYBACK_STATE state;
    //     instance.getPlaybackState(out state);
    //     return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    // }
    //
    // void Update()
    // {
    //     if (emitters[_currentIndex].IsPlaying())
    //     {
    //         Debug.Log("Emitter is playing");
    //     }
    //     else if (!emitters[_currentIndex].IsPlaying())
    //     {
    //         Debug.Log("Emitter is not playing");
    //     }
    // }
}