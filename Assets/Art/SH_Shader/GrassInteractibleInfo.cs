using Cattac.Character;
using UnityEngine;

[ExecuteInEditMode]
public class GrassInteractibleInfo : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private GameObject _gameObject;

    private Material _material;
    private void OnEnable()
    {
        _material = _meshRenderer.sharedMaterial;
    }

    private void Update()
    {
        if (_gameObject == null || _material == null) return;
        _material.SetVector("_Target", _gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterHead>() == false) return;
        _gameObject = other.gameObject;
    }
}
