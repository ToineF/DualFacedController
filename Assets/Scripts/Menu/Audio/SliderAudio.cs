using UnityEngine;
using UnityEngine.UI;

namespace Options.Audio
{
    public class SliderAudio : MonoBehaviour
    {
        [Header("Sound")]
        [SerializeField] private Slider _slider;
        [SerializeField] private SoundMode _mode;


        private void Start()
        {
            _slider.value = AudioManager.Instance.VolumeManager.GetVolume(_mode);
            _slider.onValueChanged.AddListener(value => AudioManager.Instance.VolumeManager.SetVolume(_mode, value));
        }
    }
}