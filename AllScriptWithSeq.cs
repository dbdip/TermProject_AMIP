using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllScriptWithSeq : MonoBehaviour
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

    //An array of Objects that stores the results of the Resources.LoadAll() method  
    private Object[] objects;
    //Each returned object is converted to a Texture and stored in this array  
    Texture2D[] textures;
    //Texture2D[] current;
    //With this Material object, a reference to the game object Material can be stored  
    private Material goMaterial;
    //An integer to advance frames  
    private int frameCounter = 0;

    void Start()
    {
        ////Get a reference to the Material of the game object this script is attached to  
        //this.goMaterial = this.GetComponent<Renderer>().material;

        //Get a reference to the Material of the game object this script is attached to  
        this.goMaterial = this.GetComponent<Renderer>().material;

        //Load all textures found on the Sequence folder, that is placed inside the resources folder  
        this.objects = Resources.LoadAll("Sequence", typeof(Texture2D));

        //Initialize the array of textures with the same size as the objects array  
        this.textures = new Texture2D[objects.Length];

        //Cast each Object to Texture and store the result inside the Textures array  
        for (int i = 0; i < objects.Length; i++)
        {
            this.textures[i] = (Texture2D)this.objects[i];
        }

        Mesh startMesh = this.GetComponent<MeshFilter>().mesh;
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        triangles = startMesh.triangles;
        vertices = startMesh.vertices;

        // build facelines
        int i1, i2, i3;
        Vector3 v1, v2, v3;
        faceLines = new LineRenderer[triangles.Length / 3];
        dist = new float[triangles.Length / 3];
        for (int i = 0; i < triangles.Length / 3; i++)
        {

            i1 = triangles[(i * 3) + 0];
            i2 = triangles[(i * 3) + 1];
            i3 = triangles[(i * 3) + 2];
            v1 = vertices[i1];
            v2 = vertices[i2];
            v3 = vertices[i3];
            // Debug.Log(message: $"vertices of {i}th triangle = {triangles[i * 3 + 0]}, {triangles[i * 3 + 1]}, {triangles[i * 3 + 2]}\n");

            Vector3 center = (v1 + v2 + v3) / 3;
            Vector3 normal = GetNormalVectorToTriangle(v1, v2, v3);

            // Debug.Log("center position is " + center);
            // Debug.Log("end Position is " + (center + normal * dist[i]));

            //    for (int i = 0; i < triangles.Length / 3; i++)
            //    {
            LineRenderer line = Instantiate(linePrefab, center, Quaternion.identity);
            faceLines[i] = line;
        }                     
    }

    // Start is called before the first frame update
    void Update()
    {
        //Call the 'PlayLoop' method as a coroutine with a 0.04 delay  
        StartCoroutine("PlayLoop", 0.035f);

        //Set the material's texture to the current value of the frameCounter variable  
        this.goMaterial.mainTexture = textures[frameCounter];

        CenterOfTriangle(triangles, vertices);


    }

    //The following methods return a IEnumerator so they can be yielded:  
    //A method to play the animation in a loop  
    IEnumerator PlayLoop(float delay)
    {
        //Wait for the time defined at the delay parameter  
        yield return new WaitForSeconds(delay);

        //Advance one frame  
        frameCounter = (++frameCounter) % textures.Length;

        //Stop this coroutine  
        StopCoroutine("PlayLoop");
    }

    //A method to play the animation just once  
    IEnumerator Play(float delay)
    {
        //Wait for the time defined at the delay parameter  
        yield return new WaitForSeconds(delay);

        //If the frame counter isn't at the last frame  
        if (frameCounter < textures.Length - 1)
        {
            //Advance one frame  
            ++frameCounter;
        }

        //Stop this coroutine  
        StopCoroutine("PlayLoop");
    }
       // public void FindEachTriangle()
       // {
       //     CenterOfTriangle(triangles, vertices);
       //}

        public void CenterOfTriangle(int[] triangles, Vector3[] vertices)
        {
           
            Vector3 center = new Vector3();
            Vector3 normal = new Vector3();


            Debug.Log($"triangles length = {triangles.Length}");
            //Debug.Log($"vertices length = {vertices.Length}");

            float H, S, V;
            int i1, i2, i3;
            Vector3 v1, v2, v3;

            for (int i = 0; i < triangles.Length / 3; i++)
            {
                i1 = triangles[(i * 3) + 0];
                i2 = triangles[(i * 3) + 1];
                i3 = triangles[(i * 3) + 2];
                v1 = vertices[i1];
                v2 = vertices[i2];
                v3 = vertices[i3];
                center = (v1 + v2 + v3) / 3;
                normal = GetNormalVectorToTriangle(v1, v2, v3);

                int index = frameCounter;
                //current[i] = textures[index];
                Color color = textures[index].GetPixel((int)center.x, (int)center.y);

                Color.RGBToHSV(color, out H, out S, out V);
                // Debug.Log("V: " + V);
                dist[i] = Mathf.Gamma(V, 5, 1/1.5f) + 0.5f;
                //dist[i] = V * 3.5f;

            Vector3 endPosition = center + (normal * dist[i]);
                faceLines[i]?.SetPosition(0, center);
                faceLines[i]?.SetPosition(1, position: endPosition);
                faceLines[i]?.SetWidth(0.04f, 0.02f);


            }

            // CreateLineForEachTriangle(center, normal);

        }

     
    /*    public LineRenderer GetCenterAndNormalVectorToTriangle(Vector3 v1, Vector3 v2, Vector3 v3, out Vector3 center, out Vector3 normal)
        {
            

            //  Debug.Log("center position is " + center);
            // Debug.Log("end Position is " + (center + normal * dist[i]));

            //    for (int i = 0; i < triangles.Length / 3; i++)
            //    {
            LineRenderer line = Instantiate(linePrefab, center, Quaternion.identity);


            //    }
            return line;
        }
        */
        public Vector3 GetNormalVectorToTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            Vector3 v1v2 = v2 - v1;
            Vector3 v2v3 = v3 - v2;
            Vector3 normal = Vector3.Cross(v1v2, v2v3);

            return normal.normalized;

        }
}

