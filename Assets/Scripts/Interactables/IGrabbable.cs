

using UnityEngine;

public interface IGrabbable
{
    public void OnGrab(CharacterHead characterHead);
    public void OnUngrab(CharacterHead characterHead);
    public void AddForce(Vector3 force);
}