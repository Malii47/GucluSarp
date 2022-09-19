using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SwordEnemyAI : MonoBehaviour
{
    Animator anim;
    public Transform target;


    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) < 5)
        {
            anim.Play("Attack");
        }   
    }
}

