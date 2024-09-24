using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [Header("Head Properties")]
    [SerializeField] private float _speed;
    [SerializeField, Range(0,1)] private float _turnLerp;
    [SerializeField] private Transform[] _bodyParts;
    [SerializeField, Range(0,1)] private float _followLerp;
    [SerializeField] private float _targetDistance;

    [Header("Input Properties")]
    [SerializeField] private KeyCode _rightKey;
    [SerializeField] private KeyCode _leftKey;
    [SerializeField] private KeyCode _upKey;
    [SerializeField] private KeyCode _downKey;

    private Vector2 _direction;

    private void Update()
    {
        var targetDirection = Vector3.zero;
        if (Input.GetKey(_rightKey)) targetDirection.x++;
        if (Input.GetKey(_leftKey)) targetDirection.x--;
        if (Input.GetKey(_upKey)) targetDirection.y++;
        if (Input.GetKey(_downKey)) targetDirection.y--;
        _direction = Vector3.Lerp(_direction, targetDirection, _turnLerp);

        AddForce();
    }

    private void AddForce()
    {
        transform.position += new Vector3(_direction.x, 0, _direction.y) * _speed;

        for (int i = 0; i < _bodyParts.Length; i++)
        {
            if (i == 0) continue;
            var distanceOffset = (_bodyParts[i].position - _bodyParts[i - 1].position).normalized * _targetDistance;
            _bodyParts[i].position = Vector3.Lerp(_bodyParts[i].position, _bodyParts[i - 1].position + distanceOffset, _followLerp);
        }
    }
}
