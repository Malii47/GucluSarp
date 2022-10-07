using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunDying : MonoBehaviour
{
    public float maxhealth = 10f;
    public float CurrentHealt;
    public Animator animator;


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
        GetComponent<EnemyShooting>().enabled = false;
        GetComponent<EnemyShooting>().kola.SetActive(false);
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2.1f);
    }
}
