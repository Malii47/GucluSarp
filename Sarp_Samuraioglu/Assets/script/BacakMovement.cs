using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacakMovement : MonoBehaviour
{

    public float Speed = 5f;

    Rigidbody2D rb;
    Camera cam;
    Animator animator;

    Vector2 MousePosition;
    Vector2 Movement;

    bool Walk;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        Vector2 lookDir = MousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        rb.MovePosition(rb.position + Movement.normalized * Speed * Time.fixedDeltaTime);

        if (Movement.x != 0 || Movement.y != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else animator.SetBool("IsWalking", false);
    }
}
