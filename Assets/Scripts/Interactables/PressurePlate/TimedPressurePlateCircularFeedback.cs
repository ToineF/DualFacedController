using UnityEngine;

namespace Cattac.Interactables
{
    /// <summary>
    /// Handles the visual feedbacks for lights around the timed pressure plate
    /// </summary>
    public class TimedPressurePlateCircularFeedback : MonoBehaviour
    {
        [SerializeField] private TimedPressurePlate _timedPressurePlate;
        [SerializeField] private MeshRenderer[] _renderers;
        [SerializeField] private MeshRenderer _centerRenderer;
        [SerializeField] private Material _onColor;
        [SerializeField] private Material _offColor;
        [SerializeField] private Material _deactivateColor;

        private bool _isTicking = false;

        private void ChangeColor(int index, bool isOn)
        {
            _renderers[index].sharedMaterial = isOn ? _onColor : _offColor;
        }

        private void ChangeMainColor(float index)
        {
            _centerRenderer.sharedMaterial.SetFloat("_Progress", index);
        }
        
        private void Start()
        {
            _timedPressurePlate.OnTimerReset.AddListener(OnTimerStart);
            _timedPressurePlate.OnTimerEnd.AddListener(OnTimerEnd);
            _timedPressurePlate.OnDeactived.AddListener(OnDeactived);
        }

        private void OnDeactived()
        {
            _isTicking = false;
            foreach (var meshRenderer in _renderers)
            {
                meshRenderer.sharedMaterial = _deactivateColor;
            }

            Destroy(this);
        }

        private void OnTimerStart()
        {
            _isTicking = true;
            ChangeMainColor(0);
        }

        private void OnTimerEnd()
        {
            _isTicking = false;
            for (int i = 0; i < _renderers.Length; i++)
            {
                ChangeColor(i, false);
            }
            ChangeMainColor(1);
        }

        private void Update()
        {
            if (_isTicking == false) return;

            var percentile = 1 - _timedPressurePlate.RemainingTime;
            ChangeMainColor(percentile);
            for (int i = 0; i < _renderers.Length; i++)
            {
                ChangeColor(i, percentile > (float)i / _renderers.Length);
            }
        }
    }
}