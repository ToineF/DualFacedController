using UnityEngine;
using UnityEngine.UI;


public class MenuButtonImage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _image;
    [SerializeField] private MenuButton _menuButton;

    [Header("Parameters")]
    [SerializeField] private Sprite _selectedSprite;

    private Sprite _normalSprite;

    private void Start()
    {
        _normalSprite = _image.sprite;
    }

    private void OnEnable()
    {
        _menuButton.OnSelectEvent += OnSelect;
        _menuButton.OnDeselectEvent += OnDeselect;
    }

    private void OnDisable()
    {
        _menuButton.OnSelectEvent -= OnSelect;
        _menuButton.OnDeselectEvent -= OnDeselect;
    }

    private void OnSelect()
    {
        _image.sprite = _selectedSprite;
    }

    private void OnDeselect()
    {
        _image.sprite = _normalSprite;
    }
}