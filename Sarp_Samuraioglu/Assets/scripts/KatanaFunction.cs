using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaFunction : MonoBehaviour
{

    public Animator animator;

    public float attackRate = 2f;
    public float deflectRate = 2f;
    float nextAttackTime = 0f;
    float nextDeflectTime = 0f;
    float sarpKatanaAttackDirectionCounter;

    int parametreisWalking = Animator.StringToHash("isWalking");

    Vector2 movement;

    void Start()
    {
        animator = GetComponent<Animator>();
        sarpKatanaAttackDirectionCounter = 1;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetBool(parametreisWalking, true);
        }
        else
        {
            animator.SetBool(parametreisWalking, false);
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                sarpKatanaAttackDirectionCounter++;
                nextAttackTime = Time.time + 1f / attackRate;
                if (sarpKatanaAttackDirectionCounter % 2 == 0)
                {
                    int parametreisAttack = Animator.StringToHash("isAttack");
                    animator.SetTrigger(parametreisAttack);
                }
                if (sarpKatanaAttackDirectionCounter % 2 == 1)
                {
                    int parametreisAttack2 = Animator.StringToHash("isAttack2");
                    animator.SetTrigger(parametreisAttack2);
                }
            }
        }

        if (Time.time > nextDeflectTime)
        {
            if (Input.GetMouseButtonDown(1))
            {
                sarpKatanaAttackDirectionCounter++;
                nextDeflectTime = Time.time + 1f / deflectRate;
                if (sarpKatanaAttackDirectionCounter % 2 == 0)
                {
                    int parametreisDeflect = Animator.StringToHash("isDeflect");
                    animator.SetTrigger(parametreisDeflect);
                }

                if (sarpKatanaAttackDirectionCounter % 2 == 1)
                {
                    int parametreisDeflect2 = Animator.StringToHash("isDeflect2");
                    animator.SetTrigger(parametreisDeflect2);
                }
            }
        }

    }
}
