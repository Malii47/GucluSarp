using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float CurrentHealt;
    public float CurrentPosture;
    public float MaxPosture;

    public void TakeDamage(float hpdamage, float posturedamage)
    {

        CurrentHealt = CurrentHealt - hpdamage;
        CurrentPosture = CurrentPosture + posturedamage;

        if (CurrentHealt <= 0 && CurrentPosture >= MaxPosture)
        {
            GetComponent<Enemy_Death>().StunDeath();
        }

        else if (CurrentHealt <= 0)
        {
            GetComponent<Enemy_Death>().Death();
        }     
        
        else if (CurrentPosture >= MaxPosture)
        {
            GetComponent<EnemyStun>().Stun();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
