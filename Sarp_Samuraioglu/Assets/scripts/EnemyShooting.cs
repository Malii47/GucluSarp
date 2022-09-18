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
        Timer2 = Time.time;
        Timer = Time.time;

        if (Timer % 3 == 1)
        {
            animator.SetTrigger("Gun");
            Invoke("Shoot", 1.6f);
        }


    }

    void Shoot()
    {       
        Instantiate(kola, enemy_attackposition.position, Quaternion.identity);
    }

}
