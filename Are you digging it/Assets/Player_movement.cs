using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    public Joystick joystick;
    Rigidbody2D rb;
    Vector2 move;
    public float drillspeed = 5;
    bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

        drillspeed = 5;
        if (move.y > 0)
        {
            drillspeed = 0;
        }
        
        if (move.x > 0)
        {
            Flip();
        }


    }
    private void FixedUpdate() 
    {
    rb.MovePosition (rb.position + move * drillspeed * Time.fixedDeltaTime);
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

}
