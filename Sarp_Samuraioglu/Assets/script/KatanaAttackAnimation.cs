using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaAttackAnimation : MonoBehaviour
{


    public Animator animator;
    float count;

    void Start()
    {
        animator = GetComponent<Animator>();
        count = 1;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            count++;
            if (count % 2 == 0)
            {
                Attack1();
            }
            if (count % 2 == 1)
            {
                Attack2();
            }
        }
    }
    void Attack1()
    {
        animator.SetTrigger("isAttack");
    }
    void Attack2()
    {
        animator.SetTrigger("isAttack2");
    }
}
