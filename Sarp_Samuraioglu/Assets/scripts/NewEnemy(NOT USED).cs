using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemy : MonoBehaviour
{
    public float maxhealth = 10f;
    float CurrentHealt;


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

        if (CurrentHealt <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
