using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public GameObject dirtBlock;
    public GameObject goldblock;
    public GameObject stone_outline;

    public Vector3 lineSpawnBlock = new Vector3(0, 0, 0);

    ////////////////////////////
    // World length and width //
    ////////////////////////////
    public float start_y_of_world_pos = 0;
    public float end_y_of_world_pos = 5;
    public float start_x_of_world_pos = -2.75f;
    public float end_x_of_world_pos = 2.95f;
    public int chance_of_getting_gold = Random.Range(1, 100);
    public float xled;



    void Start()
    {
        CreateMapFirstLayerDirt();
        CreateMap_stoneouteline();
    }

    ///////////////
    // functions //
    ///////////////

    public void CreateMapFirstLayerDirt()
    {
        for (float y = start_y_of_world_pos; y < end_y_of_world_pos; y = y + 0.16f) 
        {
            for (float x = start_x_of_world_pos; x < end_x_of_world_pos; x = x + 0.16f) 
            {
             lineSpawnBlock = new Vector3(x, -y, 0);
    
             if (chance_of_getting_gold == 2)
             {
                 Instantiate(goldblock, lineSpawnBlock, Quaternion.identity);
             }
             else
             {
                Instantiate(dirtBlock, lineSpawnBlock, Quaternion.identity);
             }
             
             xled = x;
             chance_of_getting_gold = Random.Range(1, 100);
            }
            lineSpawnBlock = new Vector3(xled, -y, 0);
        }
    }
    public void CreateMap_stoneouteline()
    {
        lineSpawnBlock = new Vector3(0, 0, 0);
        for (float y = start_y_of_world_pos; y < end_y_of_world_pos; y = y + 0.16f) 
        {
                lineSpawnBlock = new Vector3(end_y_of_world_pos, -y, 0);
                Instantiate(stone_outline, lineSpawnBlock, Quaternion.identity);
                lineSpawnBlock = new Vector3(end_y_of_world_pos, -y, 0);
        }
    }
}
