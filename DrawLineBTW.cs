//using System.Collections;
//using UnityEngine;

//public class DrawLineBTW : MonoBehaviour
//{
//    private LineRenderer lineRenderer;
//    private float counter; //increment or element as we start to draw a line you'll see
//    private float dist; //distance

//    public Transform origin;
//    public Transform destination;

//    public float lineDrawSpeed = 6f;

//    // Start is called before the first frame update
//    void Start()
//    {
//        lineRenderer = GetComponent<LineRenderer>();
//        lineRenderer.SetPosition(0, origin.position);
//        lineRenderer.SetWidth(0.45f, 0.45f);

//        dist = Vector3.Distance(origin.position, destination.position);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (counter < dist)
//        {
//            counter += 0.1f / lineDrawSpeed;
            
//            //linear interpolation between two values based on some type of time
//            float x = Mathf.Lerp(0, dist, counter);

//            Vector3 pointA = origin.position;
//            Vector3 pointB = destination.position;

//            //Get the unit vector in the desired direction, which will be normal direction, multiply by the desired length and add the starting point.
//            Vector3 pointAlongline = x * Vector3.Normalize(pointB - pointA) + pointA;

//            lineRenderer.SetPosition(1, pointAlongline);

//        }

//    }
//}
