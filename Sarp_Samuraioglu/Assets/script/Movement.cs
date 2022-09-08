using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float FollowSpeed = 5f;

    public Rigidbody2D rb;
    public Camera am;
        
    Vector2 MousePosition;
    Vector2 Position;
    

    
    void Update()
    {
        MousePosition = am.ScreenToWorldPoint(Input.mousePosition);

        Position = Vector2.MoveTowards(rb.position, MousePosition, FollowSpeed);

    }

    private void FixedUpdate()
    {
        rb.MovePosition(Position);

        Vector2 LookDir = MousePosition - rb.position;
        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg -90f;
        rb.rotation = angle;
    }
}
