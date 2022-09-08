using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform enemy_attackposition;
    public GameObject kola;

    float Timer;

    void Start()
    {
        kola = GameObject.Find("kolaa 1");
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Timer = Time.time;
        if (Timer % 3 == 0)
        {
            Shoot();
        }


    }

    void Shoot()
    {
        Instantiate(kola, enemy_attackposition.position, Quaternion.identity);
    }

}
