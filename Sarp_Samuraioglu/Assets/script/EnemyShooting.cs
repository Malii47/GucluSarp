using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform enemy_attackposition;
    public GameObject kola;
    public Animator animator;

    float Timer;
    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Timer = Time.time;
        if (Timer % 3 == 1)
        {
            Shoot();
        }


    }

    void Shoot()
    {
        Instantiate(kola, enemy_attackposition.position, Quaternion.identity);
        animator.SetTrigger("Gun");
    }

}
