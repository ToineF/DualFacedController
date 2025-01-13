

using UnityEngine;

public interface IGrabbable
{
    public void OnGrab(Head head);
    public void OnUngrab(Head head);
    public void AddForce(Vector3 force);
}