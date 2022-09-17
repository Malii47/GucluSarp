using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaAnimation : MonoBehaviour
{


    public float attackRate = 2f;
    public float deflectRate = 2f;
    float nextAttackTime = 0f;
    float nextDeflectTime = 0f;

    float count;

    public Animator animator;

    Vector2 movement;

    void Start()
    {
        animator = GetComponent<Animator>();
        count = 1;
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

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

        if (Time.time > nextDeflectTime)
        {
            if (Input.GetMouseButtonDown(1))
            {
                count++;
                if (count % 2 == 0)
                {
                    Deflect1();
                    nextDeflectTime = Time.time + 1f / deflectRate;
                }

                if (count % 2 == 1)
                {
                    Deflect2();
                    nextDeflectTime = Time.time + 1f / deflectRate;
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
    void Deflect1()
    {
        animator.SetTrigger("isDeflect");
    }
    void Deflect2()
    {
        animator.SetTrigger("isDeflect2");
    }
}
