using UnityEngine;


public class MovableGrabbableJoint : MonoBehaviour, IGrabbable
{
    //[SerializeField] private Rigidbody _selfRigidbody;
    //[SerializeField] private Rigidbody _rigidbodyToApproach;
    [SerializeField] private Rigidbody _jointObject;
    private Joint _joint;

    public void OnGrab(Head head)
    {
        _joint = _jointObject.gameObject.AddComponent<HingeJoint>();
        _joint.connectedBody = head.Rigidbody;
        //_joint.autoConfigureConnectedAnchor = false;
        //_joint.connectedAnchor = Vector3.back * 2f;
    }

    public void OnUngrab(Head head)
    {
        _joint.connectedBody = null;
        Destroy(_joint);
    }

    public void AddForce(Vector3 force)
    {
        //.AddForce(force, ForceMode.Impulse);
    }
}