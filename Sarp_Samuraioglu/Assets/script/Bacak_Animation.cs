using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacak_Animation : MonoBehaviour
{

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void Anan(bool a)
    {
        if(a) animator.SetBool("IsWalking", true);
        else animator.SetBool("IsWalking", false);
    }
}
