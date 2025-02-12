using Cattac.Character;
using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Interactables
{
    public class ActivationButton : MonoBehaviour, IGrabbable
    {
        public bool IsActivated { get; private set; }
        
        [field:SerializeField] public UnityEvent OnPressed { get; private set; }

        [Header("Timer")]
        [SerializeField] private float _timer;
        [field:SerializeField] public UnityEvent OnTimerEnd { get; private set; }

        private float _internalTimer = -1f;

        public void OnGrab(CharacterHead characterHead)
        {
            OnPressed?.Invoke();
            characterHead.CurrentGrabbable = null;
            IsActivated = true;
            _internalTimer = _timer;
        }

        private void Update()
        {
            if (_internalTimer <= 0) return;
            
            _internalTimer -= Time.deltaTime;

            if (_internalTimer <= 0)
            {
                OnTimerEnd?.Invoke();
                IsActivated = false;
            }
        }

        public void OnUngrab(CharacterHead characterHead)
        {

        }

        public void AddForce(Vector3 force)
        {

        }
    }
}