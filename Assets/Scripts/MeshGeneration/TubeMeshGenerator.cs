using AntoineFoucault.Utilities;
using System.Collections.Generic;
using UnityEngine;


public class TubeMeshGenerator : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;

    [Header("Circle Parameters")]
    [SerializeField] private int _sides;
    [SerializeField] private float _outerRadius;
    [SerializeField] private float _innerRadius;

    [Header("Tube Parameters")]
    [SerializeField] private int _length;
    [SerializeField] private float _density;

    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;

    private void Start()
    {
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;
    }

    private void Update()
    {
        var totalVertices = new List<Vector3>();
        var totalTriangles = new List<int>();

        var currentVertices = 0;

        // Create inner circles
        for (int i = 0; i < _length; i++)
        {
            var positionOffset = _density * i * Vector3.forward;
            var vertices = new Vector3[] { };
            var triangles = new int[] { };
            var drawTriangles = i == 0 || i == _length - 1;
            var inverseNormal = i == 0;

            DrawFilled(positionOffset, _sides, _outerRadius, out vertices, out triangles, currentVertices, drawTriangles, inverseNormal);

            totalVertices.AddRange(vertices);
            totalTriangles.AddRange(triangles);

            currentVertices += vertices.Length;
        }

        // Create links between circles
        for (int i = 0; i < _length - 1; i++)
        {
            //if (_isHollow)
            //    DrawHollowTriangles(positionOffset, _sides, _outerRadius, _innerRadius, out vertices, out triangles, currentVertices);
            //else
            //var vertices = totalVertices.GetRange(i * _sides, _sides).ToArray();
            totalTriangles.AddRange(DrawFilledTrianglesInCircle(_sides, _length));
        }

        _vertices = totalVertices.ToArray();
        _triangles = totalTriangles.ToArray();

        DrawMesh();
    }

    private void DrawFilled(Vector3 positionOffset, int sides, float radius, out Vector3[] vertices, out int[] triangles, int verticesOffset, bool drawTriangles, bool inverseNormal)
    {
        vertices = GetCircumferencePoints(positionOffset, sides, radius).ToArray();
        triangles = drawTriangles ? DrawFilledTriangles(vertices, verticesOffset, inverseNormal) : new int[0];
    }

    private void DrawMesh()
    {
        _mesh.Clear();

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;

        _mesh.RecalculateNormals();
    }

    private List<Vector3> GetCircumferencePoints(Vector3 positionOffset, int sides, float radius)
    {
        var points = new List<Vector3>();
        var circumferenceProgressPerStep = (float)1f / sides;
        var fullCircle = 2 * Mathf.PI;
        var radiantProgressPerStep = circumferenceProgressPerStep * fullCircle;

        for (int i = 0; i < sides; i++)
        {
            var currentRadian = radiantProgressPerStep * i;
            points.Add(new Vector3(Mathf.Cos(currentRadian), Mathf.Sin(currentRadian)) * radius + positionOffset);
        }

        return points;
    }

    private int[] DrawFilledTriangles(Vector3[] vertices, int verticesOffset, bool inverseNormal)
    {
        var trianglesAmount = vertices.Length - 2;
        var newTriangles = new List<int>();
        for (int i = 0; i < trianglesAmount; i++)
        {
            newTriangles.Add(verticesOffset + 0);
            newTriangles.Add(verticesOffset + i + 1);
            newTriangles.Add(verticesOffset + i + 2);
            if (inverseNormal) newTriangles.ExchangeAt(newTriangles.Count - 1, newTriangles.Count - 2);
        }
        return newTriangles.ToArray();
    }

    private int[] DrawFilledTriangles(int vertex1, int vertex2, int vertex3)
    {
        var newTriangles = new int[3];

        newTriangles[0] = vertex1;
        newTriangles[1] = vertex2;
        newTriangles[2] = vertex3;

        return newTriangles;
    }

    private int[] DrawFilledTrianglesInCircle(int sides, int length)
    {
        var newTriangles = new List<int>();

        for (int i = 0; i < length - 1; i++)
        {
            var indexOffset = i * sides;
            var nextCircle = (i + 1) * sides;

            for (int j = 0; j < sides; j++)
            {
                var v1 = j + indexOffset;
                var v2 = (j + 1) % sides + indexOffset;
                var v3 = j + nextCircle;
                var v4 = (j + 1) % sides + nextCircle;

                newTriangles.AddRange(DrawFilledTriangles(v1, v2, v3));
                newTriangles.AddRange(DrawFilledTriangles(v2, v4, v3));
            }
        }
        return newTriangles.ToArray();
    }
}
