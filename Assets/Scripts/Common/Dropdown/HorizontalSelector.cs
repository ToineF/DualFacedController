using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// A generic horizontal selector, to cycle through several options
/// </summary>
public class HorizontalSelector : MonoBehaviour, IMoveHandler
{
    [field:SerializeField] public UnityEvent<int> OnUpdateOption { get; set; }
    
    [field:SerializeField] public string[] Options { get; set; }
    [SerializeField] private TMP_Text _displayText;
    [SerializeField] private bool _loop = true;

    private int _currentIndex = 0;

    public void Next()
    {
        _currentIndex++;
        if (_loop) _currentIndex %= Options.Length;
        UpdateSelector();
    }

    public void Previous()
    {
        _currentIndex--;
        if (_loop) _currentIndex = (_currentIndex + Options.Length) % Options.Length;
        UpdateSelector();
    }

    private void UpdateSelector()
    {
        if (_currentIndex < 0 || _currentIndex >= Options.Length) return;
        _displayText.text = Options[_currentIndex];
        OnUpdateOption?.Invoke(_currentIndex);
    }

    public void OnMove(AxisEventData eventData)
    {
        if (eventData.moveDir == MoveDirection.Left) Previous();
        else if (eventData.moveDir == MoveDirection.Right) Next();
    }
}