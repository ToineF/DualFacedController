using System.Collections.Generic;
using UnityEngine;


public class CircleMeshGenerator : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private bool _isHollow;
    [SerializeField] private int _sides;
    [SerializeField] private float _outerRadius;
    [SerializeField] private float _innerRadius;

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
        DrawTube();
        DrawMesh();
    }

    private void DrawTube()
    {
        if (_isHollow)
            DrawHollow(_sides, _outerRadius, _innerRadius);
        else
            DrawFilled(_sides, _outerRadius);
    }

    

    private void DrawFilled(int sides, float radius)
    {
        _vertices = GetCircumferencePoints(sides, radius).ToArray();
        _triangles = DrawFilledTriangles(_vertices);
    }

    private void DrawHollow(int sides, float outerRadius, float innerRadius)
    {
        var pointsList = new List<Vector3>();
        var outerPoints = GetCircumferencePoints(sides, outerRadius);
        var innerPoints = GetCircumferencePoints(sides, innerRadius);
        pointsList.AddRange(outerPoints);
        pointsList.AddRange(innerPoints);
        _vertices = pointsList.ToArray();
        _triangles = DrawHollowTriangles(_vertices);
    }

    private void DrawMesh()
    {
        _mesh.Clear();

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;

        _mesh.RecalculateNormals();
    }

    private List<Vector3> GetCircumferencePoints(int sides, float radius)
    {
        var points = new List<Vector3>();
        var circumferenceProgressPerStep = (float)1f / sides;
        var fullCircle = 2 * Mathf.PI;
        var radiantProgressPerStep = circumferenceProgressPerStep * fullCircle;

        for (int i = 0; i < sides; i++)
        {
            var currentRadian = radiantProgressPerStep * i;
            points.Add(new Vector3(Mathf.Cos(currentRadian), Mathf.Sin(currentRadian)) * radius);
        }

        return points;
    }

    private int[] DrawFilledTriangles(Vector3[] vertices)
    {
        var trianglesAmount = vertices.Length - 2;
        var newTriangles = new List<int>();
        for (int i = 0; i < trianglesAmount; i++)
        {
            newTriangles.Add(0);
            newTriangles.Add(i + 2);
            newTriangles.Add(i + 1);
        }
        return newTriangles.ToArray();
    }

    private int[] DrawHollowTriangles(Vector3[] vertices)
    {
        var sides = _vertices.Length / 2;
        var trianglesAmount = sides * 2;

        var newTriangles = new List<int>();
        for (int i = 0; i < sides; i++)
        {
            var outerIndex = i;
            var innerIndex = i + sides;

            // First triangle starting at outer edge i
            newTriangles.Add(outerIndex);
            newTriangles.Add(innerIndex);
            newTriangles.Add((i + 1) % sides);

            // Second triangle starting at outer edge i
            newTriangles.Add(outerIndex);
            newTriangles.Add(sides + ((sides + i - 1) % sides));
            newTriangles.Add(outerIndex + sides);
        }
        return newTriangles.ToArray();
    }
}
