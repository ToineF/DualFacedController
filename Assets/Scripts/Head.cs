using UnityEngine;

public class Head : MonoBehaviour
{
    [field:Header("Head Properties")]
    [field:SerializeField] public Rigidbody Rigidbody { get; set; }
    [field:SerializeField] public Rigidbody[] BodyParts { get; set; }
    [SerializeField] private float _speed;
    [SerializeField, Range(0,1)] private float _turnLerp;
    [SerializeField, Range(0,1)] private float _followLerp;
    [SerializeField] private float _targetDistance;

    //public ForceMode ForceMode;
    //public float forceTobODYPARTS = 1f;
    //public int forceiterations = 1;

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
    }

    private void FixedUpdate()
    {
        AddForce();        
    }

    private void AddForce()
    {
        Rigidbody.position += new Vector3(_direction.x, 0, _direction.y) * _speed;
        //Rigidbody.AddForce(new Vector3(_direction.x, 0, _direction.y) * _speed, ForceMode);
        //for (int k = 0; k < forceiterations; k++)
        //{
            for (int i = 0; i < BodyParts.Length; i++)
            {
                if (i == 0) continue;
                var distanceOffset = (BodyParts[i].position - BodyParts[i - 1].position).normalized * _targetDistance;
                BodyParts[i].position = Vector3.Lerp(BodyParts[i].position, BodyParts[i - 1].position + distanceOffset, _followLerp);
                //BodyParts[i].AddForce(distanceOffset * forceTobODYPARTS);
            }
        //}

    }
}
