using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaDeflectAnimation : MonoBehaviour
{
    public float deflectRate = 2f;
    float nextDeflectTime = 0f;
    float count;

    public Animator animator;

    void Deflect1()
    {
        animator.SetTrigger("isDeflect");
    }
    void Deflect2()
    {
        animator.SetTrigger("isDeflect2");
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        count = 2;
    }

    void Update()
    {
        if(Time.time > nextDeflectTime)
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
}
