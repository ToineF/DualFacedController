using UnityEngine;
using UnityEngine.Events;

public class ActivationButton : MonoBehaviour, IGrabbable
{
    [SerializeField] private UnityEvent OnPressed;
    
    public void OnGrab(Head head)
    {
        OnPressed?.Invoke();
        head.CurrentGrabbable = null;
    }

    public void OnUngrab(Head head)
    {
        
    }

    public void AddForce(Vector3 force)
    {
        
    }
}