using System;
using System.Collections;
using FeedbacksEditor;
using FMOD;
using UnityEngine;

namespace Cattac.Interactables
{
    /// <summary>
    /// Activates the party ambiance after some time, and switch between lights for the ambiance
    /// </summary>
    public class Dancefloor : MonoBehaviour
    {
        public Action OnPartyStart;
        
        [Header("Timing")] [SerializeField] private float _timeBeforeParty;
        
        [Header("References")]
        [SerializeField] private GameObject[] _lights;
        [SerializeField] private GameObject[] _gameObjectsToEnable;
        [SerializeField] private GameObject[] _gameObjectsToDisable;
        [SerializeField] private RotatingRigidbody[] _rotatingRigidbodies;
        [SerializeField] private float _lightFrequency;
        [SerializeField] private FMODMusicPlayer _discoMusicPlayer;
        [SerializeField] private GameEvent _turnOnEvent;

        private int _currentLightIndex;
        
        private void Start()
        {
            StartCoroutine(StartParty());
            FMODAudioManager.Instance.PlayMusic(GameMusic.DISCO_ROOM);
            _discoMusicPlayer.SetDiscoLight(0);
        }

        private IEnumerator StartParty()
        {
            yield return new WaitForSeconds(_timeBeforeParty);

            StartPartyImmediate();
        }

        public void StartPartyImmediate(bool minigame = true)
        {
            GameEventsManager.PlayEvent(_turnOnEvent, gameObject);
            _discoMusicPlayer.SetDiscoLight(1);
            if (minigame) OnPartyStart?.Invoke();
            
            StartCoroutine(StartNextLight());
            foreach (var go in _gameObjectsToEnable)
            {
                go.SetActive(true);
            }
            foreach (var go in _gameObjectsToDisable)
            {
                go.SetActive(false);
            }
            foreach (var rb in _rotatingRigidbodies)
            {
                rb.enabled = true;
            }
        }

        private IEnumerator StartNextLight()
        {
            yield return new WaitForSeconds(_lightFrequency);
            
            _currentLightIndex = (_currentLightIndex + 1) % _lights.Length;
            foreach (var light in _lights)
            {
                light.SetActive(false);
            }
            _lights[_currentLightIndex].SetActive(true);
            
            StartCoroutine(StartNextLight());
        }
    }
}