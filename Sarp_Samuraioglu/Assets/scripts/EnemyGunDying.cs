using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunDying : MonoBehaviour
{
    public float maxhealth = 10f;
    public float CurrentHealt;
    public Animator animator;
    public LayerMask playerLayer;
    public Transform bloodpoint_gun;
    public float bloodarea_radius;
    public bool oneTimeExecutionDarkRedPrint;



    void Start()
    {
        CurrentHealt = maxhealth;
        GetComponent<EnemyShooting>().enabled = true;
        GetComponent<EnemyShooting>().kola.SetActive(true);
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

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
            StartCoroutine(Die2());
        }
    }

    public void Die()
    {
        GetComponentInChildren<EnemyGunRandomizerTemp>().SarpKillsGunEnemy();
        Destroy(gameObject);
    }
    
    //Temporary Die
    IEnumerator Die2()
    {
        int parametredeath = Animator.StringToHash("Death");
        animator.SetTrigger(parametredeath);
        GetComponentInChildren<EnemyGunRandomizerTemp>().SarpKillsGunEnemy();
        GetComponent<EnemyAI2>().enabled = false;
        GetComponent<EnemyLookDir>().enabled = false;
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyShooting>().stopIEnumerator();
        GetComponent<EnemyShooting>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        oneTimeExecutionDarkRedPrint = true; 
        yield return new WaitForSeconds(2.1f);
    }
}
