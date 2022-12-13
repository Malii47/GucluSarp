using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitSplasherScript : MonoBehaviour
{

    Animator animator;
    bool a;
    bool b;

    void Start()
    {
        b = true;
        animator = GetComponent<Animator>();
        if (b)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().sarpAttackDirectionCounter % 2 == 0)
            {
                animator.SetTrigger("HitBloodSplash");
            }
            else if (GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().sarpAttackDirectionCounter % 2 == 1)
            {
                animator.SetTrigger("HitBloodSplashMirror");
            }
            Invoke("Die", 0.4f);
            b = false;
        }
        a = true;
    }

    private void OnEnable()
    {
        if (a)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().sarpAttackDirectionCounter % 2 == 0)
            {
                animator.SetTrigger("HitBloodSplash");
            }
            else if (GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().sarpAttackDirectionCounter % 2 == 1)
            {
                animator.SetTrigger("HitBloodSplashMirror");
            }
            Invoke("Die", 0.4f);
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
