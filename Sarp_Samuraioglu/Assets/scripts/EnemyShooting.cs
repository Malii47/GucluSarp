using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform enemy_attackposition;
    public GameObject kola;
    public Animator animator;
    public GameObject muzzleFlash;
    public Animator muzzleAnimator;

    float Timer;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Timer = Time.time;

        if (Timer % 3 == 1)
        {
            animator.SetTrigger("Gun");
            //Invoke("Shoot", 1.6f);
            StartCoroutine(MuzzleFlash());
        }


    }

    /*void Shoot()
    {       
        Instantiate(kola, enemy_attackposition.position, Quaternion.identity);       
    }*/ 

    IEnumerator MuzzleFlash()
    {
        yield return new WaitForSeconds(1.6f);
        Instantiate(kola, enemy_attackposition.position, Quaternion.identity);
        muzzleFlash.SetActive(true);
        muzzleAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        muzzleFlash.SetActive(false);
    }

}
