

using UnityEngine;

public interface IGrabbable
{
    public void OnGrab(Rigidbody rb);
    public void OnUngrab(Rigidbody rb);
    public void AddForce(Vector3 force);
}