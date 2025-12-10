using System.Collections;
using UnityEngine;

namespace Cattac.Interactables
{
    public class Dancefloor : MonoBehaviour
    {
        [Header("Timing")] [SerializeField] private float _timeBeforeParty;
        
        [Header("References")]
        [SerializeField] private GameObject[] _lights;
        [SerializeField] private GameObject[] _gameObjectsToEnable;
        [SerializeField] private GameObject[] _gameObjectsToDisable;
        [SerializeField] private RotatingRigidbody[] _rotatingRigidbodies;
        [SerializeField] private float _lightFrequency;
        [SerializeField] private GameObject _cheesePrefab;

        private int _currentLightIndex;
        
        private void Start()
        {
            StartCoroutine(StartParty());
            
        }

        private IEnumerator StartParty()
        {
            yield return new WaitForSeconds(_timeBeforeParty);
            
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