using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask p2Layer;
    [SerializeField] private LayerMask otherLayer;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        //Flip the model based on direction
        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        if(Input.GetKey(KeyCode.W) && isGrounded())
        {
            Jump();
        }

        anim.SetBool("run", horizontalInput != 0);
        
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    private bool isGrounded()
    {
        LayerMask combinedLayer = p2Layer | otherLayer;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, combinedLayer);
        return raycastHit.collider != null;
    }
}
