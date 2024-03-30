using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Vector2 movementInput = Vector2.zero;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");

        if (movementInput.x != 0)
        {
            transform.position += (Vector3)movementInput * speed * Time.deltaTime;
        }
    }
}
