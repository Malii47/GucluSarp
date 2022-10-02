using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxhealth = 30f;
    public float CurrentHealt;
    public GameObject soundRandomizer;


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

        if (CurrentHealt == 25)
        {
            GetComponent<Enemy_Death>().Death();
            soundRandomizer.GetComponent<EnemyDeathSoundRandomizer>().oneTimeExecution = true;

        }       
        if (CurrentHealt == 15)
        {
            GetComponent<Enemy_Death>().StunDeath();
            soundRandomizer.GetComponent<EnemyDeathSoundRandomizer>().oneTimeExecution = true;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
