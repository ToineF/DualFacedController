using UnityEngine;
using FMOD.Studio;
using UnityEngine.UI;

namespace FMOD
{
    public class FMODBus : MonoBehaviour
    {
        private Bus _bus;
        [SerializeField] private string _name;
        [SerializeField] private Slider _slider;
        
        private void Awake()
        {
            _slider.onValueChanged.AddListener(UpdateBus);
        }

        private void Start()
        {
            _bus = FMODUnity.RuntimeManager.GetBus($"bus:/{_name}");
        }

        private void UpdateBus(float busVolume)
        {
            _bus.setVolume(busVolume);
        }
    }
}