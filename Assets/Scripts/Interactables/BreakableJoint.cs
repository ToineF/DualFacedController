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

    public void OnGrab(CharacterHead characterHead)
    {
        characterHead.CurrentGrabbable = null;
        Destroy(_joint);
        Destroy(this);
    }

    public void OnUngrab(CharacterHead characterHead)
    {
    }

    public void AddForce(Vector3 force)
    {
    }
}