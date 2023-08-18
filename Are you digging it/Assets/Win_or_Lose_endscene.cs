using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_or_Lose_endscene : MonoBehaviour
{
    public GameObject lose_target;
    public GameObject win_target;
    public GameObject IdleState;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    public float jumpHeight;
    public float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpHeight);
        //rb.velocity = new Vector2(0, speed);
    }
}
