using UnityEngine;

public class GrassInteractibleInfo : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private GameObject _gameObject1;
    [SerializeField] private GameObject _gameObject2;
    
    private void Update()
    {
        if (_gameObject1 == null || _gameObject2 == null || _material == null) return;
        _material.SetVector("_Target", _gameObject1.transform.position);
        _material.SetVector("_Target2", _gameObject2.transform.position);
    }
}
