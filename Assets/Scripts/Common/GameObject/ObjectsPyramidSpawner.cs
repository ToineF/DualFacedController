using System.Collections.Generic;
using AntoineFoucault.Utilities;
using NaughtyAttributes;
using UnityEngine;

public class ObjectsPyramidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private int _inside = 0;

    private List<GameObject> _spawnedObjects = new List<GameObject>();

    [Button]
    public void Create()
    {
        Debug.Log("Create");
        Clear();
        BuildPyramid();
    }

    [Button]
    public void Clear()
    {
        if (_spawnedObjects == null) return;
        foreach (var item in _spawnedObjects)
        {
            DestroyImmediate(item);
        }

        _spawnedObjects.Clear();
    }

    public void ClearBuildingChildren()
    {
        Clear();
        transform.ClearImmediate();
    }

    private void BuildPyramid()
    {
        for (int y = _height - 1; y >= 0; y--)
        {
            var centerOffset = y * _positionOffset / 2;
            for (int x = 0; x < y + 1; x++)
            {
                for (int z = 0; z < y + 1; z++)
                {
                    centerOffset.y = 0;
                    if (x <= _inside || x >= y - _inside || z <= _inside || z >= y - _inside)
                        SpawnAtPosition(transform.position + Vector3.Scale(new Vector3(x, -y, z), _positionOffset) - centerOffset);
                }
            }
        }


        // for (int y = 0; y < _height; y++)
        // {
        //     float t = (_height <= 1) ? 0 : (float)y / (_height - 1);
        //
        //     // Radius decreases from base to top
        //     float radius = Mathf.Lerp(_width * 0.5f, 0.5f, t);
        //
        //     int max = Mathf.CeilToInt(radius);
        //
        //     for (int x = -max; x <= max; x++)
        //     {
        //         for (int z = -max; z <= max; z++)
        //         {
        //             float distance = Mathf.Sqrt(x * x + z * z);
        //
        //             if (distance > radius) continue;
        //
        //             Vector3 pos = transform.position + _positionOffset + new Vector3(x, y, z);
        //
        //             var obj = Instantiate(_prefabToSpawn, pos, Quaternion.identity, transform);
        //
        //             _spawnedObjects.Add(obj);
        //         }
        //     }
        // }
    }

    private void SpawnAtPosition(Vector3 pos)
    {
        var obj = Instantiate(_prefabToSpawn, pos, Quaternion.identity, transform);
        _spawnedObjects.Add(obj);
    }
}