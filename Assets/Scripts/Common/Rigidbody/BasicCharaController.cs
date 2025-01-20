using UnityEngine;



public class BasicCharaController : MonoBehaviour
{

    [field: Header("Head Properties")]
    [field: SerializeField] public Rigidbody Rigidbody { get; set; }

    [SerializeField] private ForceMode _forceMode;
    [SerializeField] private float _speed;
    [SerializeField, Range(0, 1)] private float _turnLerp;

    [Header("Inputs")]
    [SerializeField] private KeyCode _upKey;
    [SerializeField] private KeyCode _downKey;
    [SerializeField] private KeyCode _rightKey;
    [SerializeField] private KeyCode _leftKey;

    private Vector2 _direction;

    private void Update()
    {
        var targetDirection = Vector2.zero;
        if (Input.GetKey(_upKey)) targetDirection.y++;
        if (Input.GetKey(_downKey)) targetDirection.y--;
        if (Input.GetKey(_rightKey)) targetDirection.x++;
        if (Input.GetKey(_leftKey)) targetDirection.x--;

        _direction = Vector3.Lerp(_direction, targetDirection, _turnLerp);
    }

    private void FixedUpdate()
    {
        var force = new Vector3(_direction.x, 0, _direction.y) * _speed;
        Rigidbody.AddForce(force, _forceMode);
    }
}