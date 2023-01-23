using System.Collections;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public Sprite tile;
    public int worldSize = 10;
    public float noiceFreq = 0.05f;
    public float seed;
    public Texture2D noiceTexture;
    
    private void Start() 
    {
         seed = Random.Range(-10000,10000);
         GenerateNoiceTexture();
         generateworld();
    }

    public void generateworld()
    {
        for (int x = -20; x < worldSize; x++)
        {
            for (int y = -20; y < (worldSize-5); y++)
            {
                GameObject newTile = new GameObject();
                newTile.AddComponent<SpriteRenderer>();
                newTile.GetComponent<SpriteRenderer>().sprite = tile;
                newTile.transform.position = new Vector2(x,y);
            }
        }
    }

    public void GenerateNoiceTexture()
    {
       
        noiceTexture = new Texture2D(worldSize, worldSize);

        for (int x = -20; x < noiceTexture.width; x++)
        {
            for (int y = -20; y < noiceTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * noiceFreq, (y + seed) * noiceFreq);
                noiceTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }
        noiceTexture.Apply();
    }
}
