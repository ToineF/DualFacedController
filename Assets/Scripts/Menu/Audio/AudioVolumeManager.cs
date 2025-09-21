using System;
using System.Collections.Generic;
using UnityEngine;


namespace Options.Audio
{
    /// <summary>
    /// Handles the volume of the audio in the game,
    /// spawned by the AudioManager.
    /// </summary>
    public class AudioVolumeManager
    {
        private static float _masterVolume;
        private static float _musicVolume;
        private static float _sfxVolume;

        private AudioSource _musicSource;
        private AudioSource[] _sfxSource;

        private Dictionary<SoundMode, Action<float>> _actions = new();
        private Dictionary<SoundMode, Func<float>> _variables = new();

        public AudioVolumeManager(AudioSource musicSource, AudioSource[] sfxSources)
        {
            // Affect AudioSources
            _musicSource = musicSource;
            _sfxSource = sfxSources;

            // Populate Dictionary
            _actions.Add(SoundMode.Master, volume => _masterVolume = volume);
            _actions.Add(SoundMode.Music, volume => _musicVolume = volume);
            _actions.Add(SoundMode.SFX, volume => _sfxVolume = volume);
            _variables.Add(SoundMode.Master, () => _masterVolume);
            _variables.Add(SoundMode.Music, () => _musicVolume);
            _variables.Add(SoundMode.SFX, () => _sfxVolume);
        }

        public void SetVolume(SoundMode mode, float volume)
        {
            _actions[mode]?.Invoke(volume);
            UpdateSources();
        }

        private void UpdateSources()
        {
            _musicSource.volume = _musicVolume * _masterVolume;
            foreach (var sfxSource in _sfxSource)
            {
                sfxSource.volume = _sfxVolume * _masterVolume;
            }
        }

        public float GetVolume(SoundMode mode)
        {
            return _variables[mode].Invoke();
        }
    }
}