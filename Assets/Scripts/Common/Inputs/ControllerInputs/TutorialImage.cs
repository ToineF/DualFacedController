using UnityEngine;
using UnityEngine.UI;

namespace ControllerInputs
{
    [RequireComponent(typeof(Image))]
    public class TutorialImage : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private ControllerSwitcher _manager;

        [Header("UI")]
        [SerializeField] private Sprite _keyboardSprite;
        [SerializeField] private Sprite _xboxSprite;
        [SerializeField] private Sprite _playstationSprite;
        [SerializeField] private Sprite _switchSprite;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            if (_manager != null) _manager.OnControllerTypeChange += OnControllerChange;
            OnControllerChange(_manager.ControllerType);
        }

        private void OnControllerChange(ControllerType type)
        {
            switch (type)
            {
                case ControllerType.KEYBOARD:
                    _image.sprite = _keyboardSprite;
                    break;
                case ControllerType.XBOX:
                    _image.sprite = _xboxSprite;
                    break;
                case ControllerType.PLAYSTATION:
                    _image.sprite = _playstationSprite;
                    break;
                case ControllerType.SWITCH:
                    _image.sprite = _switchSprite;
                    break;
            }
        }
    }
}