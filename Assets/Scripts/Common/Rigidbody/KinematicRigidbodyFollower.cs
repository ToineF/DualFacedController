using UnityEngine;

public class KinematicRigidbodyFollower : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _target;
    [SerializeField] private bool _removeParent;

    private void Awake()
    {
        if (_removeParent) transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_target.position);
        //_rb.velocity = (_target.position - transform.position) * (1 / Time.fixedDeltaTime);
    }
}
