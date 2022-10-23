using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyGunDying : MonoBehaviour
{
    public float maxhealth = 10f;
    public float CurrentHealt;
    public Animator animator;
    public Animator gunLegAnimator;
    public GameObject gunEnemyLight;
    public LayerMask playerLayer;
    public Transform bloodpoint_gun;
    public float bloodarea_radius;
    public bool oneTimeExecutionDarkRedPrint;
    int parametrelegWalk = Animator.StringToHash("legWalk");



    void Start()
    {
        CurrentHealt = maxhealth;
        GetComponent<EnemyShooting>().enabled = true;
        //GetComponent<EnemyShooting>().kola.SetActive(true);
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (oneTimeExecutionDarkRedPrint)
        {
            Collider2D[] bloodarea = Physics2D.OverlapCircleAll(bloodpoint_gun.position, bloodarea_radius, playerLayer);
            foreach (Collider2D player in bloodarea)
            {
                GameObject.FindGameObjectWithTag("Bacak").GetComponent<Bacak_Animation>().oneTimeDarkRedPrint = true;
                oneTimeExecutionDarkRedPrint = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealt = CurrentHealt - damage;

        if (CurrentHealt == 5)
        {
            //Die();
            //StartCoroutine(Die2());
            Die31();
        }
    }
    /*
    public void Die()
    {
        GetComponentInChildren<EnemyGunRandomizerTemp>().SarpKillsGunEnemy();
        Destroy(gameObject);
        Vector3 pos = transform.position;
        pos.z = 0.5f;
        transform.position = pos;
    }
    */
    void anan1()
    {
        Vector3 pos = transform.position;
        pos.z = 0.5f;
        transform.position = pos;
    }
    /*
    IEnumerator Die2()
    {
        StartCoroutine(GunEnemyDeathLight());
        int parametredeath = Animator.StringToHash("Death");
        animator.SetTrigger(parametredeath);
        GetComponentInChildren<EnemyGunRandomizerTemp>().SarpKillsGunEnemy();
        anan1();
        gunLegAnimator.SetBool(parametrelegWalk, false);
        GetComponent<EnemyAI2>().enabled = false;
        GetComponent<EnemyLookDir>().enabled = false;
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyShooting>().stopIEnumerator();
        GetComponent<EnemyShooting>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        oneTimeExecutionDarkRedPrint = true; 
        yield return new WaitForSeconds(2.1f);
    }
    */

    void Die31()
    {
        StartCoroutine(GunEnemyDeathLight());
        int parametredeath = Animator.StringToHash("Death");
        animator.SetTrigger(parametredeath);
        GetComponentInChildren<EnemyGunRandomizerTemp>().SarpKillsGunEnemy();

        Vector3 pos = transform.position;
        pos.z = GameObject.Find("GameController").GetComponent<DeathPosition>().PositionStacker();
        transform.position = pos;

        gunLegAnimator.SetBool(parametrelegWalk, false);
        GetComponent<EnemyAI2>().enabled = false;
        GetComponent<EnemyLookDir>().enabled = false;
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyShooting>().stopIEnumerator();
        GetComponent<EnemyShooting>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Invoke("bloodprintdelayer", 1f);
    }

    private void bloodprintdelayer()
    {
        oneTimeExecutionDarkRedPrint = true;
    }
    IEnumerator GunEnemyDeathLight()
    {
        gunEnemyLight.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(.25f);
        gunEnemyLight.SetActive(false);
    }
}
