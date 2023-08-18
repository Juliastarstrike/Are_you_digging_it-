using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    //description: Generates the world were the player plays the game.
    public GameObject dirtBlock;
    public Vector3 lineSpawnBlock = new Vector3(0, 0, 0);

    //variables
    public float start_y_of_world_pos = 0;
    public float end_y_of_world_pos = 5;
    public float start_x_of_world_pos = -2.75f;
    public float end_x_of_world_pos = 2.95f;
    public float xled;

    void Start()
    {
        Create_world();
    }
    public void Create_world()
    {
        for (float y = start_y_of_world_pos; y < end_y_of_world_pos; y = y + 0.16f) 
        {
            for (float x = start_x_of_world_pos; x < end_x_of_world_pos; x = x + 0.16f) 
            {
             lineSpawnBlock = new Vector3(x, -y, 0);
             Instantiate(dirtBlock, lineSpawnBlock, Quaternion.identity);
             xled = x;
            }
            lineSpawnBlock = new Vector3(xled, -y, 0);
        }
    }
}
