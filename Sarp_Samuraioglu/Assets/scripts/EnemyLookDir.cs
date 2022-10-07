using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookDir : MonoBehaviour
{

    Rigidbody2D rb;
    Rigidbody2D playerrb;

    Vector2 PlayerPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerrb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        

        
    }

    void FixedUpdate()
    {
        PlayerPosition = playerrb.position;

        Vector2 LookDir = PlayerPosition - rb.position;
        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg + 90f ;
        rb.rotation = angle;

    }
}
