using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFromScreen : MonoBehaviour
{
    public LineRenderer linePrefab;
    LineRenderer[] faceLines;
    Mesh startMesh;
    MeshFilter meshFilter;
    int[] triangles;
    Transform position;

    [HideInInspector]
    public Vector3[] vertices;

    private LineRenderer lineRenderer;
    public float dist = 2.0f; //distance

    // Start is called before the first frame update
    void Start()
    {
        Mesh startMesh = this.GetComponent<MeshFilter>().mesh;
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        triangles = startMesh.triangles;
        vertices = startMesh.vertices;
        faceLines = new LineRenderer[triangles.Length / 3];

        FindEachTriangle();
    }

    public void FindEachTriangle()
    {
        CenterOfTriangle(triangles, vertices);
    }

    public void CenterOfTriangle(int[] triangles, Vector3[] vertices)
    {
        Vector3 v1;
        Vector3 v2;
        Vector3 v3;
        Vector3 center;
        Vector3 normal;

        Debug.Log($"triangles length = {triangles.Length}");
        Debug.Log($"vertices length = {vertices.Length}");


        for (int i = 0; i < triangles.Length / 3; i++)
        {
            int i1, i2, i3;
            i1 = triangles[(i * 3) + 0];
            i2 = triangles[(i * 3) + 1];
            i3 = triangles[(i * 3) + 2];
            v1 = vertices[i1];
            v2 = vertices[i2];
            v3 = vertices[i3];

            Debug.Log($"{i}'s triangle index = {i}, i1={i1}, i2={i2}, i3={i3}");
            Debug.Log($"v1={v1}, v2={v2}, v3={v3}");


            Debug.Log(message: $"vertices of {i}th triangle = {triangles[i * 3 + 0]}, {triangles[i * 3 + 1]}, {triangles[i * 3 + 2]}\n");

            GetCenterAndNormalVectorToTriangle(v1, v2, v3, out center, out normal);
            CreateLineForTriangle(center, normal);

        }

    }

    public void GetCenterAndNormalVectorToTriangle(Vector3 v1, Vector3 v2, Vector3 v3, out Vector3 center, out Vector3 normal)
    {
        center = (v1 + v2 + v3) / 3;
        normal = GetNormalVectorToTriangle(v1, v2, v3);


        for (int i = 0; i < triangles.Length / 3; i++)
        {
            //faceLines[i]?.AddComponent<LineRenderer>(); //라인추가 //NUllExeption:https://stackify.com/nullreferenceexception-object-reference-not-set/
            faceLines[i] = Instantiate(linePrefab, center, Quaternion.identity);

        }

        Debug.Log("center position is " + center);
        Debug.Log("end Position is " + (center + normal * dist));
    }

    public void CreateLineForTriangle(Vector3 center, Vector3 normal)//삼각형갯수만큼 호출
    {
        Vector3 endPosition = center + normal * dist;
        linePrefab?.SetPosition(0, center);
        linePrefab?.SetPosition(1, endPosition);
 //       linePrefab?.SetWidth(0.03f, 0.0f);

    }

    public Vector3 GetNormalVectorToTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3 v1v2 = v2 - v1;
        Vector3 v2v3 = v3 - v2;
        Vector3 normal = Vector3.Cross(v1v2, v2v3);

        return normal.normalized;

    }
}