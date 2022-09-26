using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacak_Animation : MonoBehaviour
{
    public GameObject bloodyfootprint_r;
    public GameObject bloodyfootprint_l;
    public Transform bloodyfootprint_position;
    Animator animator;
    public bool b;
    public bool c;

    public float count;

    void Start()
    {
        animator = GetComponent<Animator>();
        count = 1;
        b = false;
    }

    void Update()
    {
        if (b)
        { 
            if (count < 5)
            {
                if (c)
                {
                    if (count % 2 == 1)
                    {
                        Instantiate(bloodyfootprint_r, bloodyfootprint_position.position, Quaternion.identity);
                        StartCoroutine(bloodystep());
                        b = false;
                    }
                    else if (count % 2 == 0)
                    {
                        Instantiate(bloodyfootprint_l, bloodyfootprint_position.position, Quaternion.identity);
                        StartCoroutine(bloodystep());
                        b = false;
                    }
                }
            }
            if (count >= 5)
            {
                count = 1;
                GameObject.Find("Enemy_Sword").GetComponent<Enemy_Death>().a = true;
                b = false;
            }
        }
    }


    IEnumerator bloodystep()
    {
        yield return new WaitForSeconds(0.5f);
        count++;
        b = true;
    }

    public void Anan(bool a)
    {
        if (a)
        {
            animator.SetBool("IsWalking", true);
            c = true;
        }
        else
        {
            animator.SetBool("IsWalking", false);
            c = false;
        }
    }
}
