using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueToLength : MonoBehaviour
{
    public float rayDistance;

    public Color[] colors;
    public string[] texts;

    public Texture2D imageMap;

    private int FindIndexFromColor(Color color)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (colors[i] == color)
            {
                return i;
            }
        }

        return -1;

    }

    private void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, rayDistance))
        {
            Renderer renderer = hit.transform.GetComponent<MeshRenderer>();
            Texture2D texture = renderer.material.mainTexture as Texture2D;
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= texture.width;
            pixelUV.y *= texture.height;
            Vector2 tiling = renderer.material.mainTextureScale;
            Color color = imageMap.GetPixel(Mathf.FloorToInt(pixelUV.x * tiling.x), Mathf.FloorToInt(pixelUV.y * tiling.y));

            int index = FindIndexFromColor(color);
            if (index >= 0)
            {
                Debug.Log(texts[index]);
            }
        }
    }
}
