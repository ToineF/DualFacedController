using System.Collections;
using BulletEditor;
using Options.Audio;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public AudioVolumeManager VolumeManager { get; private set; }
    [SerializeField] private AudioSource _mainMusicSource;
    [SerializeField] private AudioLowPassFilter _mainMusicLowPassFilter;
    [SerializeField] private AudioSource _sfxSourcePrefab;
    private ObjectPooling<AudioSource> _sfxSource = new ObjectPooling<AudioSource>();

    private int _sfxSourceIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(this);
            
            _sfxSource.Initialize(20, _sfxSourcePrefab, transform);
        
            // Setup VolumeManager
            VolumeManager = new AudioVolumeManager(_mainMusicSource, _sfxSource.Pool);
            var musicVolume = _mainMusicSource.volume;
            var sfxVolume = _sfxSourcePrefab.volume;
            VolumeManager.SetVolume(SoundMode.Master, 1f);
            VolumeManager.SetVolume(SoundMode.Music, musicVolume);
            VolumeManager.SetVolume(SoundMode.SFX, sfxVolume);
        }
    }
    
    public void PlayResource(AudioResource resource)
    {
        var source = _sfxSource.GetFreeObject();
        source.resource = resource;
        source.Play();
    }


    public void FadeAudioSourceVolume(AudioSource source, float time, float volume)
    {
        StartCoroutine(StartFade(source, time, volume));
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void EnableMusicLowPass(bool enable)
    {
        _mainMusicLowPassFilter.enabled = enable;
    }
}
