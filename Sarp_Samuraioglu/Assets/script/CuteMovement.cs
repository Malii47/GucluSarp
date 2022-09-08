using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteMovement : MonoBehaviour
{
    public float moveSpeed = 0f;
    public Animator animator;

    private Rigidbody2D rigidBody2D;
    private Vector3 moveDir;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("A", true);
            moveX = -1f;
        }
        else
        {
            animator.SetBool("A", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }
        

        moveDir = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        rigidBody2D.velocity = moveDir * moveSpeed;
    }
}