using Cattac.Character;
using UnityEngine;

namespace Cattac.Interactables
{
    /// <summary>
    /// Handles the animations and feedbacks of a SeparatorSnake
    /// </summary>
    public class SeparatorSnakeAnimator :MonoBehaviour
    {
        [SerializeField] private SeparatorSnake _separatorSnake;
        [SerializeField] private BoxTriggerUnityEventPlayer _mouth;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _mouthOpenKey;

        private void Awake()
        {
            _separatorSnake.OnMouthSpit += MouthOpen;
            _mouth.OnTriggerEnter.AddListener(MouseClose);
            _animator.SetBool(_mouthOpenKey, _mouth.gameObject.activeInHierarchy);
        }
        
        private void MouthOpen(GameObject go)
        {
            if (go != _mouth.gameObject) return;
            _animator.SetBool(_mouthOpenKey, true);
        }
        
        private void MouseClose(CharacterHead head)
        {
            _animator.SetBool(_mouthOpenKey, false);
        }
    }
}