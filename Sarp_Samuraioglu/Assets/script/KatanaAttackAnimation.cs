using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaAttackAnimation : MonoBehaviour
{
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public Animator animator;
    float count;

    void Start()
    {
        animator = GetComponent<Animator>();
        count = 1;
    }


    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                count++;
                if (count % 2 == 0)
                {
                    Attack1();
                    nextAttackTime = Time.time + 1f / attackRate;

                }
                if (count % 2 == 1)
                {
                    Attack2();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
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
