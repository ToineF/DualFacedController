using UnityEngine;

public class JointToggler : MonoBehaviour
{
    [SerializeField] private SpringJoint _selfJoint;
    [SerializeField] private Rigidbody _connectedBody;
    [SerializeField] private SpringJoint _targetJoint;

    private void Reset()
    {
        _selfJoint = _selfJoint ? _selfJoint : GetComponent<SpringJoint>();
        if (_selfJoint) _connectedBody = _selfJoint.connectedBody;
        else Debug.LogError("No joint found.", this);
    }

    private void OnEnable()
    {
        _selfJoint = TryGetComponent(out SpringJoint springJoint) ? springJoint : gameObject.AddComponent<SpringJoint>();
        _selfJoint.connectedBody = _connectedBody;
        _selfJoint.spring = _targetJoint.spring;
        _selfJoint.damper = _targetJoint.damper;
        _selfJoint.maxDistance = _targetJoint.maxDistance;
        _selfJoint.tolerance = _targetJoint.tolerance;
    }

    private void OnDisable()
    {
        Destroy(_selfJoint);
    }
}