using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxhealth = 100f;
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
            Invoke("Die",.3f);
        }
    }

    void Die()
    {
        Debug.Log("ABDUHAMID OLDU");
        Destroy(gameObject);
    }
}
