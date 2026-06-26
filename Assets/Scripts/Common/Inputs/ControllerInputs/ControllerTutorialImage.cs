using EasyTextEffects.Editor.MyBoxCopy.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ControllerInputs
{
    [RequireComponent(typeof(Image))]
    public class ControllerTutorialImage : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ControllerSwitcher _manager;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        [Header("UI")]
        [SerializeField] private SpriteTextPair _keyboardSprite;
        [SerializeField] private SpriteTextPair _xboxSprite;
        [SerializeField] private SpriteTextPair _playstationSprite;
        [SerializeField] private SpriteTextPair _switchSprite;

        

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
                    _image.SetAlpha(_keyboardSprite.Sprite == null ? 0 : 1);
                    _image.sprite = _keyboardSprite.Sprite;
                    _text.text = _keyboardSprite.Text;
                    break;
                case ControllerType.XBOX:
                    _image.SetAlpha(_xboxSprite.Sprite == null ? 0 : 1);
                    _image.sprite = _xboxSprite.Sprite;
                    _text.text = _xboxSprite.Text;
                    break;
                case ControllerType.PLAYSTATION:
                    _image.SetAlpha(_playstationSprite.Sprite == null ? 0 : 1);
                    _image.sprite = _playstationSprite.Sprite;
                    _text.text = _playstationSprite.Text;
                    break;
                case ControllerType.SWITCH:
                    _image.SetAlpha(_switchSprite.Sprite == null ? 0 : 1);
                    _image.sprite = _switchSprite.Sprite;
                    _text.text = _switchSprite.Text;
                    break;
            }
        }
    }
}