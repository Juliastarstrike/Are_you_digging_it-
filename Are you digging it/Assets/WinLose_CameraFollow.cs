using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose_CameraFollow : MonoBehaviour
{
    public Transform target;
    public float yOffset = 100000f;
    public float velocity = 0;
    public float Followspeed = 2f;
    public GameObject camera1;

    bool touch = false;

    private void Update() 
    {
        if(touch)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, Followspeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Sea")
        {
            Debug.Log("tigger?");
            touch = true;
        } 
    }
}
