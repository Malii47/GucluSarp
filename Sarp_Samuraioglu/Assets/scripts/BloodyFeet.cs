using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyFeet : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D playerrb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerrb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        rb.rotation = playerrb.rotation;
    }
}
