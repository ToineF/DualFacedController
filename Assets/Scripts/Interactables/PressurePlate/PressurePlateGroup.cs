using System;
using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class PressurePlateGroup : MonoBehaviour
    {
        [SerializeField] private PressurePlate[] _pressurePlates;
        [SerializeField] private UnityEvent _onAllPressed;
        [SerializeField] private bool _oneShot = true;

        private int _plateNumber;
        
        private void Start()
        {
            foreach (var pressurePlate in _pressurePlates)
            {
                pressurePlate.OnTriggerEnterEvent.AddListener(OnPlateEnter); 
                pressurePlate.OnTriggerExitEvent.AddListener(OnPlateExit); 
            }
        }
        
        private void OnDestroy()
        {
            foreach (var pressurePlate in _pressurePlates)
            {
                pressurePlate.OnTriggerEnterEvent.AddListener(OnPlateEnter); 
                pressurePlate.OnTriggerExitEvent.AddListener(OnPlateExit); 
            }
        }

        private void OnPlateEnter()
        {
            _plateNumber++;
            CheckNumber();
        }

        private void OnPlateExit()
        {
            _plateNumber--;
        }

        private void CheckNumber()
        {
            if (_plateNumber == _pressurePlates.Length)
            {
                _onAllPressed?.Invoke();
                if (_oneShot) Destroy(this);
            }
        }
    }
}