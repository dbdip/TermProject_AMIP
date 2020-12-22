/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 normals;
    private float length = 5f;
    string triangleIdx;

//    public Transform origins;
    Mesh originalMesh;
    MeshFilter meshFilter;
    int[] triangles;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, originalMesh.triangles);
        lineRenderer.SetWidth(0.45f, 0.45f);

        normals = new Vector3(originalMesh.triangles.);
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/