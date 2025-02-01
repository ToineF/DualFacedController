using UnityEngine;

namespace Cattac.Interactables
{
    public class Cage : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N)) _animator.enabled = true;
        }
    }
}