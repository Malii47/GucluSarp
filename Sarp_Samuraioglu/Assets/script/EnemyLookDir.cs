using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookDir : MonoBehaviour
{

    Rigidbody2D rb;
    Camera cam;
    Rigidbody2D playerrb;

    Vector2 PlayerPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
        playerrb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        PlayerPosition = playerrb.position;

        
    }

    void FixedUpdate()
    {
        Vector2 LookDir = PlayerPosition - rb.position;
        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

    }
}
