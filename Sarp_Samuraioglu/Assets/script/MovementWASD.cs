using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWASD : MonoBehaviour
{

    public float Speed = 5f;

    Rigidbody2D rb;
    private Camera cam;

    Vector2 MousePosition;
    Vector2 Movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    
    void Update()
    {
        MousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 LookDir = MousePosition - rb.position;
        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        rb.MovePosition(rb.position + Movement.normalized * Speed * Time.fixedDeltaTime);
    }
}
