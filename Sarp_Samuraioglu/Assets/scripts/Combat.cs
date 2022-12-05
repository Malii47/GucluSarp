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
    [SerializeField] ParticleSystem bloodParticle1 = null;
    [SerializeField] ParticleSystem bloodParticle2 = null;
    [SerializeField] ParticleSystem chargedParticle = null;
    [SerializeField] ParticleSystem chargedAttackParticle = null;
    [SerializeField] ParticleSystem chargingParticle = null;

    public Animator animator;

    public GameObject deflectLight;
    public Animator deflectLightanim;

    public Transform attackPoint;
    public Transform DeflectPoint2;

    public float AttackRadius = 0.5f;
    public float damage = 5f;
    public float sarpAttackDirectionCounter;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public float deflectRate = 2f;
    float nextDeflectTime = 0f;
    public bool camZoom;
    public bool deathblowSound;

    public LayerMask SwordenemyLayer;
    public LayerMask GunenemyLayer;
    public LayerMask BossLayer;
    public LayerMask BulletLayer;
    public LayerMask SwordLayer;
    public LayerMask BossSwordLayer;

    public Vector2 boyut;

    public bool chargeBool;



    private void OnEnable()
    {
        camZoom = false;
}
    void Start()
    {

        animator = GetComponent<Animator>();
        sarpAttackDirectionCounter = 1;
        camZoom = false;
    }

    
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (chargeBool)
                {
                    chargedAttackParticle.Play();
                    chargingParticle.Stop();
                    chargedParticle.Stop();
                    chargeBool = false;
                }

                sarpAttackDirectionCounter++;
                DeathblowBeforeAttack();
                StartCoroutine(Attack());                
                nextAttackTime = Time.time + 1f / attackRate;
                if (sarpAttackDirectionCounter % 2 == 0)
                {
                    int parametreisAttack = Animator.StringToHash("isAttack");
                    animator.SetTrigger(parametreisAttack);
                }
                if (sarpAttackDirectionCounter % 2 == 1)
                {
                    int parametreisAttack2 = Animator.StringToHash("isAttack2");
                    animator.SetTrigger(parametreisAttack2);
                }
            }
        }

        if (Time.time > nextDeflectTime)
        {
            if (Input.GetMouseButtonDown(1))
            {
                GetComponentInChildren<SarpSwingsSword>().SarpDeflectSwinging();
                sarpAttackDirectionCounter++;
                Deflect();
                nextDeflectTime = Time.time + 1f / deflectRate;
                if (sarpAttackDirectionCounter % 2 == 0)
                {
                    int parametreisDeflect = Animator.StringToHash("isDeflect");
                    animator.SetTrigger(parametreisDeflect);
                }

                if (sarpAttackDirectionCounter % 2 == 1)
                {
                    int parametreisDeflect2 = Animator.StringToHash("isDeflect2");
                    animator.SetTrigger(parametreisDeflect2);
                }
            }
        }
    }

    void DeathblowBeforeAttack()
    {
        Collider2D[] hitswordenemy = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, SwordenemyLayer);

        foreach (Collider2D enemy in hitswordenemy)
        {
            if (deathblowSound)
                GetComponentInChildren<SarpSwingsSword>().Deathblow();
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(.3f);

        Collider2D[] hitswordenemy = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, SwordenemyLayer);

        foreach (Collider2D enemy in hitswordenemy)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            CameraShaker.Instance.ShakeOnce(7f, 50f, .1f, 1f);
            BloodParticlePlay();
        }

        Collider2D[] hitgunenemy = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, GunenemyLayer);

        foreach (Collider2D enemy in hitgunenemy)
        {
            enemy.GetComponent<EnemyGunDying>().TakeDamage(damage);
            CameraShaker.Instance.ShakeOnce(7f, 50f, .1f, 1f);
            BloodParticlePlay();
        }

        Collider2D[] hitboss = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, BossLayer);

        foreach (Collider2D boss in hitboss)
        {
            boss.GetComponent<Boss_Shooting>().BossHealth(damage);
            CameraShaker.Instance.ShakeOnce(7f, 50f, .1f, 1f);
            BloodParticlePlay();
        }

    }

    public void AttackSoundCaller()
    {
        GetComponentInChildren<SarpSwingsSword>().SarpSwordSwinging();
    }

    void Deflect()
    {
        Collider2D[] deflectBullets = Physics2D.OverlapBoxAll(DeflectPoint2.position, boyut, 0f, BulletLayer);

        foreach (Collider2D bullet in deflectBullets)
        {
            GetComponentInChildren<SarpSwingsSword>().SarpBulletDeflect();
            bullet.GetComponent<kola>().Die();
            CameraShaker.Instance.ShakeOnce(2f, 25f, .1f, 1f);
            ParticlePlay();
            StartCoroutine(DeflectLight());
        }

        Collider2D[] deflectSword = Physics2D.OverlapBoxAll(DeflectPoint2.position, boyut, 0f, SwordLayer);

        foreach (Collider2D sword in deflectSword)
        {
            camZoom = true;
            sword.GetComponentInParent<Enemy>().TakeDamage(10);
            sword.GetComponentInParent<EnemyStun>().Stun();
            CameraShaker.Instance.ShakeOnce(2f, 25f, .1f, 1f);
            ParticlePlay();
            StartCoroutine(CamZoom());
            StartCoroutine(DeflectLight());
        }

        Collider2D[] deflectbossSword = Physics2D.OverlapBoxAll(DeflectPoint2.position, boyut, 0f, BossSwordLayer);

        foreach (Collider2D bosssword in deflectbossSword)
        {
            bosssword.GetComponentInParent<Boss_Shooting>().BossHealth(damage);
            bosssword.GetComponentInParent<Boss_Shooting>().deflected = true;
            //camZoom = true;
            CameraShaker.Instance.ShakeOnce(2f, 25f, .1f, 1f);
            ParticlePlay();
            //StartCoroutine("CamZoom");
            StartCoroutine(DeflectLight());
        }
    }

    void ParticlePlay()
    {
        particle.Play();
        chargedParticle.Play();
        chargingParticle.Play();
    }

    void BloodParticlePlay()
    {
        if (sarpAttackDirectionCounter % 2 == 0) bloodParticle1.Play();
        if(sarpAttackDirectionCounter % 2 == 1) bloodParticle2.Play();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(DeflectPoint2.position, boyut);
        Gizmos.DrawSphere(attackPoint.position, AttackRadius);
    }

    IEnumerator CamZoom()
    {
        yield return new WaitForSeconds(.35f);
        camZoom = false;
    }

    IEnumerator DeflectLight()
    {
        deflectLight.SetActive(true);
        int parametreFadeInTrigger = Animator.StringToHash("FadeIn");
        deflectLightanim.SetTrigger(parametreFadeInTrigger);
        yield return new WaitForSeconds(1f);
        deflectLight.SetActive(false);
    }
}
