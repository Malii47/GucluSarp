using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Combat : MonoBehaviour
{

    [SerializeField] ParticleSystem particle = null;

    public Animator animator;

    public Transform attackPoint;
    public Transform DeflectPoint2;
    
    public float AttackRadius = 0.5f;
    public float damage = 10f;

    public LayerMask EnemyLayer;
    public LayerMask BulletLayer;

    public Vector2 boyut;


    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Deflect_Block();
        }        
    }

    void Attack()
    {
        //animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    void Deflect_Block()
    {
        //animator.SetTrigger("Deflect");


        Collider2D[] deflectBullets = Physics2D.OverlapBoxAll(DeflectPoint2.position, boyut, 0f, BulletLayer);
        foreach (Collider2D bullet in deflectBullets)
        {
            bullet.GetComponent<kola>().OnCollisionEnter();
            Debug.Log("Kursun blok");
            particle.Play();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(DeflectPoint2.position, boyut);
    }

}
