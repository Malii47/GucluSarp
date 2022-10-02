using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunDying : MonoBehaviour
{
    public float maxhealth = 30f;
    public float CurrentHealt;


    void Start()
    {
        CurrentHealt = maxhealth;
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
        GetComponentInChildren<EnemyGunRandomizerTemp>().SarpKillsGunEnemy();
        GetComponent<EnemyShooting>().enabled = false;
        GetComponent<EnemyShooting>().kola.SetActive(false);
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2.1f);
        Destroy(gameObject);
    }
}
