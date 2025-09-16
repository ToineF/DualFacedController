using System.Text;
using Cattac.Interactables.Collectibles;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Cattac.Character.Credits
{
    public class TextToMesh : MonoBehaviour
    {
        [SerializeField] private string _text;
        [SerializeField] private GameObject _mesh;
        [SerializeField] private CheeseCollectible _cheeseCollectiblePrefab;
        [SerializeField] private Vector3 _targetEulerAngles;
        [SerializeField] private float _charEspacement;

        [Button]
        public void SpawnText()
        {
            var childCount = _mesh.transform.childCount - 1;
            var parent = new GameObject();
            parent.transform.SetParent(transform);
            parent.AddComponent<CreditsTextDoScale>();
            StringBuilder parentName = new StringBuilder();
            for (int i = 0; i < _text.Length; i++)
            {
                if (_text[i] == '_' || _text[i] == ' ')
                {
                    parentName.Append(' ');
                    continue;
                }

                int index = childCount - (_text[i] - 'A');
                var cheese = PrefabUtility.InstantiatePrefab(_cheeseCollectiblePrefab.gameObject).GameObject();
                cheese.transform.position = transform.position - Vector3.right * i * _charEspacement;
                cheese.transform.SetParent(parent.transform);
                var letter = Instantiate(_mesh.transform.GetChild(index), cheese.transform);
                letter.localPosition = Vector3.zero;
                parentName.Append(_text[i]);
            }
            parent.name = parentName.ToString();
            parent.transform.localEulerAngles =_targetEulerAngles;
        }
    }
}