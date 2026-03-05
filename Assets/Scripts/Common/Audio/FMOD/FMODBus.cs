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

        private readonly float _minSliderValue = -20f;
        private readonly float _maxSliderValue = 10f;
        
        private void Awake()
        {
            _slider.onValueChanged.AddListener(UpdateBus);
        }

        private void Start()
        {
            _bus = FMODUnity.RuntimeManager.GetBus($"bus:/{_name}");
            dd();
            //getbusyes();
        }

        private void UpdateBus(float busVolume)
        {
            _bus.setVolume(busVolume);
        }

        private void dd()
        {
            _bus.getChannelGroup(out var group);
            GetDSPByName(_bus, "dsds", out DSP dsds);
        }
        
        public void GetDSPByName(FMOD.Studio.Bus bus, string targetName, out FMOD.DSP dsp)
        {
            dsp = new FMOD.DSP();

            bus.getChannelGroup(out FMOD.ChannelGroup cg);
            cg.getNumDSPs(out int numDSPs);

            for (int i = 0; i < numDSPs; ++i)
            {
                cg.getDSP(i, out FMOD.DSP tmpDsp);

                tmpDsp.getInfo(out string dspName, out uint version, out int channels, out int configwidth, out int configheight);

                if (dspName.Contains(targetName))
                {
                    dsp = tmpDsp;
                }
            }
        }
    }
}