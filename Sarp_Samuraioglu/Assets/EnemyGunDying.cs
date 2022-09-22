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

        if (CurrentHealt == 25)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
