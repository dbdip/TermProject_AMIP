using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AllScriptTest : MonoBehaviour
{
    Mesh startMesh;
    MeshFilter meshFilter;
    int[] triangles;
    Transform position;

    [HideInInspector]
    public Vector3[] vertices;

    public LineRenderer linePrefab;
    private float[] dist; //distance
    LineRenderer[] faceLines;

    [HideInInspector]
    public Color[] colors;
    public Texture2D imageMap;

    // Start is called before the first frame update
    void Start()
    {
        Mesh startMesh = this.GetComponent<MeshFilter>().mesh;
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        

        triangles = startMesh.triangles;
        vertices = startMesh.vertices;
        faceLines = new LineRenderer[triangles.Length / 3];
        dist = new float[triangles.Length / 3];

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
        Vector3 center = new Vector3();
        Vector3 normal = new Vector3();


        //Debug.Log($"triangles length = {triangles.Length}");
        //Debug.Log($"vertices length = {vertices.Length}");

        float H, S, V;

        for (int i = 0; i < triangles.Length / 3; i++)
        {

            int i1, i2, i3;
            i1 = triangles[(i * 3) + 0];
            i2 = triangles[(i * 3) + 1];
            i3 = triangles[(i * 3) + 2];
            v1 = vertices[i1];
            v2 = vertices[i2];
            v3 = vertices[i3];
            // v2 = vertices[i *3 + 1];
            // v3 = vertices[i *3 + 2];

           

            //Debug.Log(message: $"vertices of {i}th triangle = {triangles[i * 3 + 0]}, {triangles[i * 3 + 1]}, {triangles[i * 3 + 2]}\n");

            faceLines[i] = GetCenterAndNormalVectorToTriangle(v1, v2, v3, out center, out normal);

            //  dist[i] = GetDistanceFromTexture(faceLines[i]);

            Color color = imageMap.GetPixel((int) center.x, (int) center.y);

            Color.RGBToHSV(color, out H, out S, out V);
            dist[i] = (V * 2 + 0.5f) *2;

            Vector3 endPosition = center + (normal * dist[i]);
            faceLines[i]?.SetPosition(0, center);
            faceLines[i]?.SetPosition(1, position: endPosition);
            faceLines[i]?.SetWidth(0.05f, 0.02f);

           
        }
    
        // CreateLineForEachTriangle(center, normal);

    }

    public LineRenderer GetCenterAndNormalVectorToTriangle(Vector3 v1, Vector3 v2, Vector3 v3, out Vector3 center, out Vector3 normal)
    {
        center = (v1 + v2 + v3) / 3;
        normal = GetNormalVectorToTriangle(v1, v2, v3);

      //  Debug.Log("center position is " + center);
       // Debug.Log("end Position is " + (center + normal * dist[i]));

    //    for (int i = 0; i < triangles.Length / 3; i++)
    //    {
           LineRenderer line = Instantiate(linePrefab, center, Quaternion.identity);


        //    }
        return line;
    }

    /*
public void CreateLineForEachTriangle(Vector3 center, Vector3 normal)
    {
        for (int i = 0; i < triangles.Length/3; i++)
        {

            // LineRenderer lineRenderer = GetComponent<LineRenderer>();
            LineRenderer lineRenderer = faceLines[i];
            Vector3 endPosition = center + (normal * dist[i]);
            lineRenderer?.SetPosition(0, center);
            lineRenderer?.SetPosition(1, position: endPosition);
    
        }
    }
    */

    public Vector3 GetNormalVectorToTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3 v1v2 = v2 - v1;
        Vector3 v2v3 = v3 - v2;
        Vector3 normal = Vector3.Cross(v1v2, v2v3);

        return normal.normalized;

    }

    /*
    public float GetDistanceFromTexture(LineRenderer line)
    {

         //    Renderer renderer = this.transform.GetComponent<MeshRenderer>();
          //  Texture2D texture = renderer.material.mainTexture as Texture2D;
        //    Vector2 pixelUV = new Vector2();

        //    pixelUV.x *= texture.width;
        //    pixelUV.y *= texture.height;
            //  Color color = imageMap.GetPixel(Mathf.FloorToInt(pixelUV.x ), Mathf.FloorToInt(pixelUV.y));
            Debug.Log("faceline vector: "+ line.GetPosition(0));
            Color color = imageMap.GetPixel((int)line.GetPosition(0).x, (int)line.GetPosition(0).y);
            float H, S, V;

    
            Color.RGBToHSV(color, out H, out S, out V);

            Debug.Log("V value: " + V);

        
            return  5* V + 0.5f;
     
     
}
*/

}

