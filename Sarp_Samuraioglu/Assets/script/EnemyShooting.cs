using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform enemy_attackposition;
    public GameObject kola;
    public Animator animator;

    float Timer;
    float Timer2;
    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Timer = Time.time;
        if (Timer % 2.8 == 1)
        {
            Shoot();
        }


    }

    void Shoot()
    {
        animator.SetTrigger("Gun");
        Timer2 = Time.time;
        if (Timer2 % 3 == 1)
        {
            Instantiate(kola, enemy_attackposition.position, Quaternion.identity);
        }
        
        
    }

}
