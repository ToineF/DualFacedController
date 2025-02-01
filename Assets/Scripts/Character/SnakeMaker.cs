using AntoineFoucault.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cattac.Character
{
    public class SnakeMaker : MonoBehaviour
    {
        [FormerlySerializedAs("_headPrefab")] [Header("References")] [SerializeField]
        private CharacterHead characterHeadPrefab;

        [SerializeField] private CharacterHead _tailPrefab;

        [FormerlySerializedAs("_bodyPrefab")] [SerializeField]
        private CharacterBody characterBodyPrefab;

        [SerializeField] private Rigidbody _bodyPartPrefab;
        [SerializeField] private Transform _snakeParent;

        [Header("Parameters")] [SerializeField]
        private int _bodyPartsCount;

        [SerializeField] private float _partsStartMargin;

        [ContextMenu("UpdateSnake")]
        public void UpdateSnake()
        {
            _snakeParent.ClearImmediate();

            var body = Instantiate(characterBodyPrefab, _snakeParent);

            var head = Instantiate(characterHeadPrefab, body.transform);
            var tail = Instantiate(_tailPrefab, body.transform);
            tail.transform.position += _partsStartMargin * _bodyPartsCount * Vector3.right;

            //body.Head = head;
            //body.Tail = tail;


            Rigidbody[] _parts = new Rigidbody[_bodyPartsCount];
            var bodyPartsParent = new GameObject("Body Parts").transform;
            bodyPartsParent.SetParent(body.transform);
            bodyPartsParent.localPosition = Vector3.zero;
            for (int i = 0; i < _bodyPartsCount; i++)
            {
                _parts[i] = Instantiate(_bodyPartPrefab, bodyPartsParent);
                _parts[i].transform.position += _partsStartMargin * i * Vector3.right;
            }
            //body.BodyParts = _parts;
        }
    }
}