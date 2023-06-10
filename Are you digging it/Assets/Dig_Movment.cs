using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dig_Movment : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "DirtBlock")
        {
            Destroy(other.gameObject);
            Score_manager.instance.addPoint();
        }
    } 
}
