using UnityEngine;
using UnityEngine.Events;

public class ActivationButton : MonoBehaviour, IGrabbable
{
    [SerializeField] private UnityEvent OnPressed;
    
    public void OnGrab(CharacterHead characterHead)
    {
        OnPressed?.Invoke();
        characterHead.CurrentGrabbable = null;
    }

    public void OnUngrab(CharacterHead characterHead)
    {
        
    }

    public void AddForce(Vector3 force)
    {
        
    }
}