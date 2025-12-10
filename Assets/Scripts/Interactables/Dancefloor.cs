using System;
using System.Collections;
using UnityEngine;

namespace Cattac.Interactables
{
    public class Dancefloor : MonoBehaviour
    {
        [SerializeField] private GameObject[] _lights;
        [SerializeField] private float _lightFrequency;
        [SerializeField] private GameObject _cheesePrefab;

        private int _currentLightIndex;
        
        private void Start()
        {
            StartCoroutine(StartNextLight());
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