using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using UnityEngine.UI;

namespace FMOD
{
    public class FMODBus : MonoBehaviour
    {
        private static Dictionary<string, float> _busVolumes = new();
        
        private Bus _bus;
        [SerializeField] private string _name;
        [SerializeField] private Slider _slider;

        private void Start()
        {
            _bus = FMODUnity.RuntimeManager.GetBus($"bus:/{_name}");
            _slider.onValueChanged.AddListener(UpdateBus);

            if (_busVolumes.TryGetValue(_name, out var volume))
            {
                _slider.value = volume;
            }
            else
            {
                _busVolumes.Add(_name, 1);
            }
        }

        private void UpdateBus(float busVolume)
        {
            _bus.setVolume(busVolume);
            _busVolumes[_name] = busVolume;
        }
    }
}