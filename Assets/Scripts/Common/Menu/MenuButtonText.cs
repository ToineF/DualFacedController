using TMPro;
using UnityEngine;


public class MenuButtonText : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private TMP_Text _label;

    [SerializeField] private MenuButton _menuButton;

    [Header("Parameters")] [SerializeField]
    private Color _selectedColor = Color.white;

    private Color _normalColor;

    private void Start()
    {
        _normalColor = _label.color;
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
        _label.color = _selectedColor;
    }

    private void OnDeselect()
    {
        _label.color = _normalColor;
    }
}