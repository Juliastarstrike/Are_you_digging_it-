using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow_player : MonoBehaviour
{
    public float Followspeed = 2f;
    public float yOffset =1f;
    public float xOffset =1f;
    public Transform target;
    void Start()
    {
        
    }
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, Followspeed * Time.deltaTime);
    }
}
