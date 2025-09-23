using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class TimedPressurePlateGroup : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onAllActivated;
        [SerializeField] private TimedPressurePlate[] _pressurePlates;

        private void Start()
        {
            foreach (var pressurePlate in _pressurePlates)
            {
                pressurePlate.OnTimerReset.AddListener(TimeReset);
            }
        }

        private void TimeReset()
        {
            if (AreAllActivated())
            {
                Debug.Log("All activated youhou");
                foreach (var pressurePlate in _pressurePlates)
                {
                    pressurePlate.Deactivate();
                }

                _onAllActivated?.Invoke();
            }
            else
            {
                Debug.Log("TIME RESUME");
                foreach (var pressurePlate in _pressurePlates)
                {
                    if (pressurePlate.IsTicking)
                        pressurePlate.ResetTimer();
                }
            }
        }

        private bool AreAllActivated()
        {
            foreach (var pressurePlate in _pressurePlates)
            {
                if (pressurePlate.IsTicking == false) return false;
            }

            return true;
        }
    }
}