using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using EZCameraShake;
using Unity.VisualScripting;

public class Combat : MonoBehaviour
{

    [SerializeField] ParticleSystem particle = null;
    [SerializeField] ParticleSystem particle2 = null;
    [SerializeField] ParticleSystem particle3 = null;
    [SerializeField] ParticleSystem particle4 = null;

    public Animator animator;

    public Transform attackPoint;
    public Transform DeflectPoint2;

    public float AttackRadius = 0.5f;
    public float damage = 5f;
    public float count;
    public float count2;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public float deflectRate = 2f;
    float nextDeflectTime = 0f;
    public float x = 0f;

    

    public LayerMask SwordenemyLayer;
    public LayerMask GunenemyLayer;
    public LayerMask BulletLayer;
    public LayerMask SwordLayer;

    public Vector2 boyut;

    void ParticlePlay()
    {
        particle.Play();
        particle2.Play();
        particle3.Play();
        particle4.Play();
    }

    private void OnEnable()
    {
        x = 0f;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        count = 1;
        x = 0f;
    }

    void FixedUpdate()
    {

    }
    
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {

                count++;
                if (count % 2 == 0)
                {
                    animator.SetTrigger("isAttack");
                    Invoke("Attack1", .3f);
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                if (count % 2 == 1)
                {
                    animator.SetTrigger("isAttack2");
                    Invoke("Attack2", .3f);
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }

        if (Time.time > nextDeflectTime)
        {
            if (Input.GetMouseButtonDown(1))
            {
                count++;
                if (count % 2 == 0)
                {
                    animator.SetTrigger("isDeflect");
                    Deflect1();
                    nextDeflectTime = Time.time + 1f / deflectRate;
                }

                if (count % 2 == 1)
                {
                    animator.SetTrigger("isDeflect2");
                    Deflect2();

                    nextDeflectTime = Time.time + 1f / deflectRate;
                }
            }
        }
    }

    void Attack1()
    {

        Collider2D[] hitswordenemy = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, SwordenemyLayer);

        foreach (Collider2D enemy in hitswordenemy)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            CameraShaker.Instance.ShakeOnce(7f, 50f, .1f, 1f);
        }

        Collider2D[] hitgunenemy = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, GunenemyLayer);

        foreach (Collider2D enemy in hitgunenemy)
        {
            enemy.GetComponent<EnemyGunDying>().TakeDamage(damage);
            CameraShaker.Instance.ShakeOnce(7f, 50f, .1f, 1f);
        }

    }
    void Attack2()
    {

        Collider2D[] hitswordenemy = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, SwordenemyLayer);

        foreach (Collider2D enemy in hitswordenemy)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            CameraShaker.Instance.ShakeOnce(7f, 50f, .1f, 1f);
        }

        Collider2D[] hitgunenemy = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, GunenemyLayer);

        foreach (Collider2D enemy in hitgunenemy)
        {
            enemy.GetComponent<EnemyGunDying>().TakeDamage(damage);
            CameraShaker.Instance.ShakeOnce(7f, 50f, .1f, 1f);
        }

    }

    void Deflect1()
    {

        Collider2D[] deflectBullets = Physics2D.OverlapBoxAll(DeflectPoint2.position, boyut, 0f, BulletLayer);

        foreach (Collider2D bullet in deflectBullets)
        {
            bullet.GetComponent<kola>().Die();
            CameraShaker.Instance.ShakeOnce(2f, 25f, .1f, 1f);
            ParticlePlay();
        }

        Collider2D[] deflectSword = Physics2D.OverlapBoxAll(DeflectPoint2.position, boyut, 0f, SwordLayer);

        foreach (Collider2D sword in deflectSword)
        {
            x = 1;
            sword.GetComponentInParent<Enemy>().TakeDamage(10);
            sword.GetComponentInParent<EnemyStun>().Stun();
            CameraShaker.Instance.ShakeOnce(2f, 25f, .1f, 1f);
            ParticlePlay();
        }
    }

    void Deflect2()
    {
        x = 1;
        Collider2D[] deflectBullets = Physics2D.OverlapBoxAll(DeflectPoint2.position, boyut, 0f, BulletLayer);

        foreach (Collider2D bullet in deflectBullets)
        {
            bullet.GetComponent<kola>().Die();
            CameraShaker.Instance.ShakeOnce(2f, 25f, .1f, 1f);
            ParticlePlay();
        }

        Collider2D[] deflectSword = Physics2D.OverlapBoxAll(DeflectPoint2.position, boyut, 0f, SwordLayer);

        foreach (Collider2D sword in deflectSword)
        {
            sword.GetComponentInParent<Enemy>().TakeDamage(10);
            sword.GetComponentInParent<EnemyStun>().Stun();
            CameraShaker.Instance.ShakeOnce(2f, 25f, .1f, 1f);
            ParticlePlay();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(DeflectPoint2.position, boyut);
        //Gizmos.DrawSphere(attackPoint.position, AttackRadius);
    }
}
