using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Audio;

public class FMODBusMixerLinker : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup unityMixer;

    private Bus musicBus;

    private void Start()
    {
        musicBus = RuntimeManager.GetBus("bus:/Music");
    }

    public void SetMusicVolume(float sliderValue)
    {
        // sliderValue = 0..1

        // Unity mixer
        float db = Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20f;
        unityMixer.audioMixer.SetFloat("MusicVolume", db);

        // FMOD bus
        musicBus.setVolume(sliderValue);
        
    }
    
    private float lastDb;

    void Update()
    {
        if (unityMixer.audioMixer.GetFloat("MusicVolume", out float db))
        {
            if (!Mathf.Approximately(db, lastDb))
            {
                lastDb = db;

                float linear = Mathf.Pow(10f, db / 20f);
                musicBus.setVolume(linear);
            }
        }
    }

}