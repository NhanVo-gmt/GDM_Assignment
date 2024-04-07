using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);


        //Flip the model based on direction
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.UpArrow) && grounded)
        {
            Jump();
        }

        anim.SetBool("run", horizontalInput != 0);
        
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
