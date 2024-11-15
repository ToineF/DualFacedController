using UnityEngine;

/// <summary>
/// Represents the visual of a unit based on its direction.
/// Handles all of the animations and visual rotation.
/// </summary>
public class HeadAnimator : MonoBehaviour
{
    [SerializeField] private Head _head;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isOrientationInverted;
    [SerializeField] private float _lookAtLerp;
    
    private void LateUpdate()
    {
        RotateDirection();
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        _animator.SetBool("IsWalking", _head.Direction.sqrMagnitude > 0.1f);
    }

    private void RotateDirection()
    {
        Vector3 moveDirection = _head.Direction;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
        int orientation = _isOrientationInverted ? -1 : 1;

        Vector3 point = transform.position - moveDirection * orientation;
        Vector3 direction = point - transform.position;
        if (direction.magnitude < 0.001f) return;
        Quaternion toRotation = Quaternion.LookRotation(direction, transform.up);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, toRotation, _lookAtLerp * Time.deltaTime);
    }
}