using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// A generic horizontal selector, to cycle through several options
/// </summary>
public class HorizontalSelector : MonoBehaviour, IMoveHandler
{
    public UnityEvent OnNext { get; set; } = new UnityEvent();
    public UnityEvent OnPrevious { get; set; } = new UnityEvent();
    [field:SerializeField] public UnityEvent<int> OnValueChange { get; set; }

    public int CurrentIndex
    {
        get => _currentIndex;
        set
        {
            _currentIndex = value;
            UpdateSelector();
        }
    }
    
    [field:SerializeField] public string[] Options { get; set; }
    [SerializeField] private TMP_Text _displayText;
    [SerializeField] private bool _loop = true;

    private int _currentIndex = 0;


    public void Next()
    {
        CurrentIndex++;
        if (_loop) CurrentIndex %= Options.Length;
        UpdateSelector();
        OnNext?.Invoke();
    }

    public void Previous()
    {
        CurrentIndex--;
        if (_loop) CurrentIndex = (CurrentIndex + Options.Length) % Options.Length;
        UpdateSelector();
        OnPrevious?.Invoke();
    }

    private void UpdateSelector()
    {
        if (CurrentIndex < 0 || CurrentIndex >= Options.Length) return;
        _displayText.text = Options[CurrentIndex];
        OnValueChange?.Invoke(CurrentIndex);
    }

    public void OnMove(AxisEventData eventData)
    {
        if (eventData.moveDir == MoveDirection.Left) Previous();
        else if (eventData.moveDir == MoveDirection.Right) Next();
    }
}