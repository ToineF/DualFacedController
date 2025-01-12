using AntoineFoucault.Utilities;
using UnityEngine;

public class SnakeMaker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Head _headPrefab;
    [SerializeField] private Head _tailPrefab;
    [SerializeField] private Body _bodyPrefab;
    [SerializeField] private Rigidbody _bodyPartPrefab;
    [SerializeField] private LineRenderer _linePrefab;
    [SerializeField] private Transform _snakeParent;

    [Header("Parameters")]
    [SerializeField] private int _bodyPartsCount;
    [SerializeField] private float _partsStartMargin;
    
    [ContextMenu("UpdateSnake")]
    public void UpdateSnake()
    {
        _snakeParent.ClearImmediate();
        
        var body = Instantiate(_bodyPrefab, _snakeParent);

        var head = Instantiate(_headPrefab, body.transform);
        var tail = Instantiate(_tailPrefab, body.transform);
        tail.transform.position += _partsStartMargin * _bodyPartsCount * Vector3.right;

        body.Head = head;
        body.Tail = tail;


        Rigidbody[] _parts = new Rigidbody[_bodyPartsCount];
        var bodyPartsParent = new GameObject("Body Parts").transform;
        bodyPartsParent.SetParent(body.transform);
        bodyPartsParent.localPosition = Vector3.zero;
        for (int i = 0; i < _bodyPartsCount; i++)
        {
            _parts[i] = Instantiate(_bodyPartPrefab, bodyPartsParent);
            _parts[i].transform.position += _partsStartMargin * i * Vector3.right;
        }
        body.BodyParts = _parts;

        var lines = new LineRenderer[_bodyPartsCount+1];
        var linesParent = new GameObject("Lines").transform;
        linesParent.SetParent(body.transform);
        linesParent.localPosition = Vector3.zero;
        for (int i = 0; i < _bodyPartsCount+1; i++)
        {
            lines[i] = Instantiate(_linePrefab, linesParent);
        }
        linesParent.gameObject.SetActive(false);
        body.Lines = lines;
    }
}
