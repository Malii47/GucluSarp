using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxhealth = 5f;
    public float posture = 0f;
    public float CurrentHealt;
    public float CurrentPosture;


    void Start()
    {
        CurrentHealt = maxhealth;
        CurrentPosture = posture;
    }

   /*
    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
   */
    public void TakeDamage(float hpdamage, float posturedamage)
    {

        CurrentHealt = CurrentHealt - hpdamage;
        CurrentPosture = CurrentPosture + posturedamage;

        if (CurrentHealt <= 0 && CurrentPosture >= 10)
        {
            GetComponent<Enemy_Death>().StunDeath();
        }

        else if (CurrentHealt <= 0)
        {
            GetComponent<Enemy_Death>().Death();
        }     
        
        else if (CurrentPosture == 10)
        {
            GetComponent<EnemyStun>().Stun();
        }

        
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
