using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Character
{
    /// <summary>
    /// Generic event for when a character head squeaks
    /// </summary>
    public class SqueakEvent : MonoBehaviour
    {
        [SerializeField] private CharacterHead _head;
        [SerializeField] private UnityEvent _onSqueak;

        private void Start()
        {
            _head.OnSqueak += Squeak;
        }

        private void OnDestroy()
        {
            _head.OnSqueak -= Squeak;
        }

        private void Squeak()
        {
            _onSqueak?.Invoke();
        }
    }
}