using UnityEngine;

public class Separator : MonoBehaviour, IGrabbable
{
    [SerializeField] private bool _separate;
    
    public void OnGrab(CharacterHead characterHead)
    {
        if (characterHead.IsSeparated == _separate) return;
        
        characterHead.CurrentGrabbable = null;
        characterHead.SetSeparation(_separate);
    }

    public void OnUngrab(CharacterHead characterHead)
    {
        
    }

    public void AddForce(Vector3 force)
    {
    }
}