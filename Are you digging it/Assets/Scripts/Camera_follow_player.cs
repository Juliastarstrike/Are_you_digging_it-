using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow_player : MonoBehaviour
{
    //description: this script make the Main camera follow the player.
    public float Followspeed = 2f;
    public float yOffset =0f;
    public float xOffset =1f;
    public Transform target;
    public GameObject timer_Panel;
    void Start()
    {
        Timer didPlayerWon = timer_Panel.GetComponent<Timer>();
    }
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, Followspeed * Time.deltaTime);
    }
}
