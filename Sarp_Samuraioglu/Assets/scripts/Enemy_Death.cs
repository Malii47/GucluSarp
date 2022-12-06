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
    public GameObject KatanaLight;
    public float blood_area_radius;
    public float blood_area_radius2;
    public LayerMask playerLayer;
    public float count;
    public bool oneTimeExecutionDarkRedPrint;
    public bool oneTimeExecutionLightRedPrint;
    

    public bool deadnormal = false;
    public bool deadstun = false;

    int parametreattackBool = Animator.StringToHash("attackBool");
    int parametrewalkBool = Animator.StringToHash("walkBool");
    int parametredeathTrigger = Animator.StringToHash("deathTrigger");
    int parametredeathTrigger2 = Animator.StringToHash("deathTrigger2");
    int parametrestundeathTrigger = Animator.StringToHash("stundeathTrigger");

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        count = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().sarpAttackDirectionCounter;

        if (oneTimeExecutionDarkRedPrint)
        {
            Collider2D[] bloodarea = Physics2D.OverlapCircleAll(blood_point.position, blood_area_radius, playerLayer);
            foreach (Collider2D player in bloodarea)
            {
                GameObject.FindGameObjectWithTag("Bacak").GetComponent<Bacak_Animation>().oneTimeDarkRedPrint = true;
                oneTimeExecutionDarkRedPrint = false;
            }
        }

        if (oneTimeExecutionLightRedPrint)
        {
            Collider2D[] bloodarea2 = Physics2D.OverlapCircleAll(blood_point2.position, blood_area_radius2, playerLayer);
            foreach (Collider2D player in bloodarea2)
            {
                GameObject.FindGameObjectWithTag("Bacak").GetComponent<Bacak_Animation>().oneTimeLightRedPrint = true;
                oneTimeExecutionLightRedPrint = false;
            }
        }
    }
    public void Death()
    {
        StartCoroutine(EnemyDeathLight());
        GetComponentInChildren<EnemyDeathSoundRandomizer>().SarpKillsEnemy();
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<SwordEnemyAI>().stoppingIEnumerators();
        GetComponent<SwordEnemyAI>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        if (count % 2 == 1)
        {
            anim.SetBool(parametrewalkBool, false);
            anim.SetBool(parametreattackBool, false);
            anim.SetTrigger(parametredeathTrigger);
        }
        else if (count % 2 == 0)
        {
            anim.SetBool(parametrewalkBool, false);
            anim.SetBool(parametreattackBool, false);
            anim.SetTrigger(parametredeathTrigger2);
        }
        DeathPosition();
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyLookDirSword>().enabled = false;
        GetComponentInChildren<BagirsakPirt>().headBool = true;
        oneTimeExecutionLightRedPrint = true;
        deadnormal = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().ParticleStopper();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound = false;
        KatanaLight.SetActive(false);
        
    }
    public void StunDeath()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().ParticleStopper();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound = false;
        KatanaLight.SetActive(false);        
        StartCoroutine(EnemyStunDeathLight());
        GetComponentInChildren<EnemyDeathSoundRandomizer>().SarpKillsEnemy();
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<SwordEnemyAI>().stoppingIEnumerators();
        GetComponent<EnemyStun>().Ineedsomesleep();
        GetComponent<SwordEnemyAI>().enabled = false;
        GetComponent<EnemyStun>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetBool(parametrewalkBool, false);
        anim.SetBool(parametreattackBool, false);
        anim.SetTrigger(parametrestundeathTrigger);
        DeathPosition();
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyLookDirSword>().enabled = false;
        Invoke("bagirsakpirt", 0.5f);
        oneTimeExecutionDarkRedPrint = true;
        deadstun = true;        
    }

    void DeathPosition()
    {
        Vector3 pos = transform.position;
        pos.z = GameObject.Find("GameController").GetComponent<DeathPosition>().PositionStacker();
        transform.position = pos;
    }
    void bagirsakpirt()
    {
        GetComponentInChildren<BagirsakPirt>().bowelBool = true;
        GetComponentInChildren<BagirsakPirt>().pantBool = true;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(blood_point.position, blood_area_radius);
        //Gizmos.DrawSphere(blood_point2.position, blood_area_radius2);
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
