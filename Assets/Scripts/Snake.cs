using System;
using AntoineFoucault.Utilities;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Head _headPrefab;
    [SerializeField] private Head _tailPrefab;
    [SerializeField] private Rigidbody _bodyPartPrefab;
    [SerializeField] private LineRenderer _linePrefab;
    [SerializeField] private int _bodyPartsCount;
    [SerializeField] private float _partsStartMargin;
    
    [ContextMenu("UpdateSnake")]
    public void UpdateSnake()
    {
        transform.ClearImmediate();
        
        Rigidbody[] _parts = new Rigidbody[_bodyPartsCount + 2];
        
        var head = Instantiate(_headPrefab, transform.position, Quaternion.identity, transform);
        _parts[0] = head.Rigidbody;
        for (int i = 0; i < _bodyPartsCount; i++)
        {
            _parts[i+1] = Instantiate(_bodyPartPrefab, transform.position, Quaternion.identity, transform);
        }
        var tail = Instantiate(_tailPrefab, transform.position, Quaternion.identity, transform);
        _parts[_bodyPartsCount+1] = tail.Rigidbody;

        head.BodyParts = new Rigidbody[_bodyPartsCount+2];
        tail.BodyParts = new Rigidbody[_bodyPartsCount+2];
        for (int i = 0; i < _bodyPartsCount+2; i++)
        {
            _parts[i].transform.position += _partsStartMargin * i * Vector3.right;
            head.BodyParts[i] = _parts[i];
            tail.BodyParts[_bodyPartsCount+1-i] = _parts[i];
        }

        var lines = new LineRenderer[_bodyPartsCount+1];
        for (int i = 0; i < _bodyPartsCount+1; i++)
        {
            lines[i] = Instantiate(_linePrefab, transform.position, Quaternion.identity, transform);
        }
        head.Lines = lines;
        tail.Lines = lines;
    }
}
