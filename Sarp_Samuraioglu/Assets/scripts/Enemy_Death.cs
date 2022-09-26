using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_Death : MonoBehaviour
{
    Animator anim;
    public Transform blood_point;
    public Transform blood_point2;
    public GameObject bagirsak;
    public GameObject StunLight;
    public GameObject EnemyLight;
    public float blood_area_radius;
    public float blood_area_radius2;
    public LayerMask playerLayer;
    float count;
    public bool a;
    public bool e;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        count = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().count;

        if (a)
        {
            Collider2D[] bloodarea = Physics2D.OverlapCircleAll(blood_point.position, blood_area_radius);
            foreach (Collider2D player in bloodarea)
            {
                GameObject.FindGameObjectWithTag("Bacak").GetComponent<Bacak_Animation>().b = true;
                a = false;
            }
        }

        if (e)
        {
            Collider2D[] bloodarea2 = Physics2D.OverlapCircleAll(blood_point2.position, blood_area_radius2);
            foreach (Collider2D player in bloodarea2)
            {
                GameObject.FindGameObjectWithTag("Bacak").GetComponent<Bacak_Animation>().b = true;
                e = false;
            }
        }
    }
    public void Death()
    {
        StartCoroutine(EnemyDeathLight());
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<SwordEnemyAI>().stoppingIEnumerators();
        GetComponent<SwordEnemyAI>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        if (count % 2 == 1)
        {
            anim.SetBool("walkBool", false);
            anim.SetBool("attackBool", false);
            anim.SetTrigger("deathTrigger");
        }
        else if (count % 2 == 0)
        {
            anim.SetBool("walkBool", false);
            anim.SetBool("attackBool", false);
            anim.SetTrigger("deathTrigger2");
        }
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyLookDirSword>().enabled = false;
        e = true;
    }
    public void StunDeath()
    {
        StartCoroutine(EnemyStunDeathLight());
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<SwordEnemyAI>().stoppingIEnumerators();
        GetComponent<EnemyStun>().Ineedsomesleep();
        GetComponent<SwordEnemyAI>().enabled = false;
        GetComponent<EnemyStun>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetBool("walkBool", false);
        anim.SetBool("attackBool", false);
        anim.SetTrigger("stundeathTrigger");
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyLookDirSword>().enabled = false;
        Invoke("bagirsakpirt", 0.5f);
        a = true;
    }

    void bagirsakpirt()
    {
        GetComponentInChildren<BagirsakPirt>().b = true;
        GetComponentInChildren<BagirsakPirt>().a = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(blood_point.position, blood_area_radius);
        Gizmos.DrawSphere(blood_point2.position, blood_area_radius2);
    }
    
    IEnumerator EnemyDeathLight()
    {
        EnemyLight.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(.25f);
        EnemyLight.SetActive(false);
    }

    IEnumerator EnemyStunDeathLight()
    {
        StunLight.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(.25f);
        StunLight.SetActive(false);
    }

}
