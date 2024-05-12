using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    private Animator anim;

    private Vector2 movementInput = Vector2.zero;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        if (movementInput != Vector2.zero)
        {
            movementInput.Normalize();

            anim.SetFloat("x", movementInput.x);
            anim.SetFloat("y", movementInput.y);
            anim.Play("Walk");

            transform.position += (Vector3)movementInput * speed * Time.deltaTime;
        }
        else
        {
            anim.Play("Idle");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Transform"))
        {
            GameManager.Instance.Phase2();
        }
    }
}