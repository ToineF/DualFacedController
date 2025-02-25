using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class ActivationButtonGroup : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onActivated;
        [SerializeField] private ActivationButton[] _buttons;

        private void Start()
        {
            foreach (var button in _buttons)
            {
                button.OnPressed.AddListener(CheckActivation);
            }
        }

        private void Update()
        {
            CheckActivation();
        }

        private void CheckActivation()
        {
            foreach (var button in _buttons)
            {
                if (button.IsActivated == false) return;
            }
            
            foreach (var button in _buttons)
            {
                button.OnPressed.RemoveAllListeners();
                button.OnTimerEnd.RemoveAllListeners();
                Destroy(button);
            }
            
            _onActivated?.Invoke();
        }
    }
}