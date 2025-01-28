using Unity.VisualScripting;
using UnityEngine;

public class BreakableJoint : MonoBehaviour, IGrabbable
{
    [Header("References")]
    [SerializeField] private Joint _joint;
    
    private void Reset()
    {
        if (transform.parent.TryGetComponent(out Joint joint)) _joint = joint;
        else Debug.LogError("Parent does not contain Joint component", transform.parent);
    }

    public void OnGrab(Head head)
    {
        head.CurrentGrabbable = null;
        Destroy(_joint);
        Destroy(this);
    }

    public void OnUngrab(Head head)
    {
    }

    public void AddForce(Vector3 force)
    {
    }
}