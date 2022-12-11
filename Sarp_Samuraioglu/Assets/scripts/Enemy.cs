using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float CurrentHealt;
    public float CurrentPosture;
    public float MaxPosture;
    public bool posturedecrease;
    float Timer;

    public void TakeDamage(float hpdamage, float posturedamage)
    {
        posturedecrease = false;
        CurrentHealt = CurrentHealt - hpdamage;
        CurrentPosture = CurrentPosture + posturedamage;
        if(CurrentPosture < MaxPosture) Invoke("A", 1.5f);

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
    private void FixedUpdate()
    {
        if (posturedecrease)
        {
            Timer = Time.time;

            if (Timer % 1 == 0)
            {
                if (CurrentPosture == MaxPosture)
                {
                    posturedecrease = false;
                }
                CurrentPosture--;
            }
        }
    }

    public void A()
    {
        posturedecrease = true;
    }
}
