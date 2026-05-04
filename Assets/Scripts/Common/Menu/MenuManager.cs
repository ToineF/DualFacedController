using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [field:SerializeField] public Transition Transition { get;  private set; }
    public static MenuManager Instance { get; private set; }
    public bool CanClickButtons {get; private set;}

    private void Awake()
    {
        Instance = this;
        CanClickButtons = true;
    }

    public void SetButtonsUnclickable()
    {
        CanClickButtons = false;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
