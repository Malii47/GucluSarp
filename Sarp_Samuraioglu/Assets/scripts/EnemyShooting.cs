using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform enemy_attackposition;
    //public GameObject kola;
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
            StartCoroutine(MuzzleFlash());
        }


    }


    IEnumerator MuzzleFlash()
    {
        yield return new WaitForSeconds(1.6f);

        GameObject bullet = ObjectPool.SharedInstance.GetPooledBullets();
        if (bullet != null)
        {
            bullet.transform.position = enemy_attackposition.position;
            bullet.transform.rotation = enemy_attackposition.rotation;
            bullet.SetActive(true);
        }

        muzzleFlash.SetActive(true);
        muzzleAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        muzzleFlash.SetActive(false);
    }

    public void stopIEnumerator()
    {
        StopAllCoroutines();
    }
}
