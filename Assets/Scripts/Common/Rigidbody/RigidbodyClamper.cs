using UnityEngine;


public class RigidbodyClamper : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _maxVelocity;

    private void LateUpdate()
    {
        _rigidbody.linearVelocity = Vector3.ClampMagnitude(_rigidbody.linearVelocity, _maxVelocity);
    }
}
