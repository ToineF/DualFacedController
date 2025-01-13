using UnityEngine;

public class Separator : MonoBehaviour, IGrabbable
{
    [SerializeField] private bool _separate;
    
    public void OnGrab(Head head)
    {
        if (head.IsSeparated == _separate) return;
        
        head.CurrentGrabbable = null;
        head.SetSeparation(_separate);
    }

    public void OnUngrab(Head head)
    {
        
    }

    public void AddForce(Vector3 force)
    {
    }
}