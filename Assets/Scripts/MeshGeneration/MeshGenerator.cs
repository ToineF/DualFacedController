using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshRenderer _meshRenderer;

    [Header("Parameters")]
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector3 _gridOffset;
    [SerializeField] private Vector2Int _gridSize;

    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;

    private void Start()
    {
        CreateShape();
    }

    private void Update()
    {
        CreateCube();
        DrawMesh();
    }

    private void CreateShape()
    {
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;
    }

    private void MakeContigousGrid()
    {
        _vertices = new Vector3[(_gridSize.x + 1) * (_gridSize.y + 1)];
        _triangles = new int[_gridSize.x * _gridSize.y * 6];

        var vertexOffset = _cellSize * 0.5f;

        // Create vertices and triangles trackers
        var v = 0;
        var t = 0;

        // Create vertices
        for (int x = 0; x < _gridSize.x + 1; x++)
        {
            for (int y = 0; y < _gridSize.y + 1; y++)
            {
                _vertices[v] = new Vector3(x * _cellSize - vertexOffset, y * _cellSize - vertexOffset);
                v++;
            }
        }

        // Create triangles
        v = 0;

        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                _triangles[t] = v;
                _triangles[t + 1] = _triangles[t + 3] = v + 1;
                _triangles[t + 2] = _triangles[t + 5] = v + (_gridSize.y + 1);
                _triangles[t + 4] = v + (_gridSize.y + 1) + 1;

                v++;
                t += 6;
            }
            v++;
        }
    }

    private void MakeDiscreteGrid()
    {
        _vertices = new Vector3[_gridSize.x * _gridSize.y * 4];
        _triangles = new int[_gridSize.x * _gridSize.y * 6];

        var cellOffset = _cellSize * 0.5f;

        var v = 0;
        var t = 0;

        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                var indexOffset = x * _gridSize.y + y;
                var positionOffset = new Vector3(x * _cellSize, y * _cellSize);

                _vertices[v] = new Vector3(-cellOffset, -cellOffset) + _gridOffset + positionOffset;
                _vertices[v + 1] = new Vector3(-cellOffset, cellOffset) + _gridOffset + positionOffset;
                _vertices[v + 2] = new Vector3(cellOffset, cellOffset) + _gridOffset + positionOffset;
                _vertices[v + 3] = new Vector3(cellOffset, -cellOffset) + _gridOffset + positionOffset;

                _triangles[t] = v;
                _triangles[t + 1] = _triangles[t + 3] = v + 1;
                _triangles[t + 2] = _triangles[t + 5] = v + 3;
                _triangles[t + 4] = v + 2;

                v += 4;
                t += 6;
            }
        }
    }

    private void CreateTube()
    {
        _vertices = new Vector3[]
                {
            new Vector3(0,0,0),
            new Vector3(0,1,0),
            new Vector3(1,1,0),
            new Vector3(1,0,0),
            new Vector3(0,0,1),
            new Vector3(0,1,1),
            new Vector3(1,1,1),
            new Vector3(1,0,1),
                };

        _triangles = new int[]
        {
            0,1,3,
            1,2,3,
            4,0,3,
            4,3,7,
            5,2,1,
            5,6,2,
            4,7,5,
            5,7,6,
        };
    }

    private void CreateCube()
    {
        _vertices = new Vector3[]
                {
            new Vector3(0,0,0),
            new Vector3(0,1,0),
            new Vector3(1,1,0),
            new Vector3(1,0,0),
            new Vector3(0,0,1),
            new Vector3(0,1,1),
            new Vector3(1,1,1),
            new Vector3(1,0,1),
                };

        _triangles = new int[]
        {
            0,1,3,
            1,2,3,
            4,0,3,
            4,3,7,
            5,2,1,
            5,6,2,
            4,7,5,
            5,7,6,
            0,4,1,
            1,4,5,
            3,2,7,
            2,6,7,
        };
    }

    private void DrawMesh()
    {
        _mesh.Clear();

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;

        _mesh.RecalculateNormals();
    }
}
