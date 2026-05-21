using UnityEngine;

public class KinematicRigidbodyFollower : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _target;
    [SerializeField] private bool _removeParent;

    private void Start()
    {
        if (_removeParent) transform.SetParent(transform.parent.parent);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_target.position);
        //_rb.velocity = (_target.position - transform.position) * (1 / Time.fixedDeltaTime);
    }
}
