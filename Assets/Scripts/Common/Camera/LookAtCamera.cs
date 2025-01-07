using UnityEngine;


public class LookAtCamera : MonoBehaviour
{
    private Transform _target;

    void Start()
    {
        _target = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(transform.position + _target.rotation * Vector3.forward, _target.rotation * Vector3.up);
    }
}
