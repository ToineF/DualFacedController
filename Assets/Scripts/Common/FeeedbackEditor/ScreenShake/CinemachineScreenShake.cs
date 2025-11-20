using Cinemachine;
using UnityEngine;

namespace Common.ScreenShake
{
    /// <summary>
    /// A cinemachine camera that can be shaked
    /// </summary>
    public class CinemachineScreenShake : MonoBehaviour
    {
        [SerializeField] private ScreenShakeType _orderingType = ScreenShakeType.MAXIMUM;

        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        
        private CinemachineBasicMultiChannelPerlin _cinemachinePerlin;
        private CinemachineScreenShakeManager _screenShakeManager;

        private void Awake()
        {
            _cinemachinePerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Start()
        {
            _cinemachinePerlin.m_AmplitudeGain = 0;
            _screenShakeManager = CinemachineScreenShakeManager.Instance;
        }
        
        private void Update()
        {
            if (_screenShakeManager.IsShaking == false) return;

            _cinemachinePerlin.m_AmplitudeGain = _screenShakeManager.GetShakeOffset(_orderingType);
        }
    }
}