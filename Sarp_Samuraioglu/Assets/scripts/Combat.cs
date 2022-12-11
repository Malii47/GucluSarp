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
    [SerializeField] ParticleSystem chargedParticle = null;
    [SerializeField] ParticleSystem chargedAttackParticle = null;
    [SerializeField] ParticleSystem chargingParticle = null;

    public Animator animator;

    public GameObject deflectLight;
    public Animator deflectLightanim;

    public Transform attackPoint;
    public Transform DeflectPoint2;

    public float AttackRadius = 0.5f;
    public float hpdamage = 5f;
    public float attackposturedamage = 5f;
    public float deflectposturedamage = 10f;
    public float sarpAttackDirectionCounter;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public float deflectRate = 2f;
    float nextDeflectTime = 0f;
    public bool camZoom;
    public bool deathblowSound;
    float tempdmg;

    public LayerMask SwordenemyLayer;
    public LayerMask GunenemyLayer;
    public LayerMask EnemiesLayer;
    public LayerMask BossLayer;
    public LayerMask BulletLayer;
    public LayerMask SwordLayer;
    public LayerMask BossSwordLayer;

    public Vector2 boyut;



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
        ParticleStopper();
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (deathblowSound)
                {
                    chargedAttackParticle.Play();
                }

                sarpAttackDirectionCounter++;
                DeathblowBeforeAttack();           
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
        Collider2D[] hitswordenemy = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, EnemiesLayer);

        foreach (Collider2D enemy in hitswordenemy)
        {
            if (deathblowSound)
                GetComponentInChildren<SarpSwingsSword>().Deathblow();
        }
    }

    public void Attack()
    {

        Collider2D[] hitswordenemy = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, SwordenemyLayer);

        foreach (Collider2D enemy in hitswordenemy)
        {
            if (0 >= enemy.GetComponent<Enemy>().CurrentHealt - hpdamage)
            {
                enemy.GetComponent<Enemy>().TakeDamage(hpdamage, 0);
            }
            else enemy.GetComponent<Enemy>().TakeDamage(hpdamage, attackposturedamage);
            CameraShaker.Instance.ShakeOnce(7f, 50f, .1f, 1f);
            if (enemy.GetComponent<Enemy>().CurrentPosture >= enemy.GetComponent<Enemy>().MaxPosture && enemy.GetComponent<Enemy>().CurrentHealt != 1 - hpdamage && enemy.GetComponent<EnemyStun>().countt == 0)
            {
                Debug.Log("stun by attack sound");
                enemy.GetComponent<EnemyStun>().countt++;
            }
            //enemy.GetComponentInChildren<BloodSplashController>().bloodSplashManager = true;

        }

        Collider2D[] hitgunenemy = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, GunenemyLayer);

        foreach (Collider2D enemy in hitgunenemy)
        {
            enemy.GetComponent<EnemyGunDying>().TakeDamage(hpdamage,attackposturedamage);
            CameraShaker.Instance.ShakeOnce(7f, 50f, .1f, 1f);
            //enemy.GetComponentInChildren<BloodSplashController>().bloodSplashManager = true;
        }

        Collider2D[] hitboss = Physics2D.OverlapCircleAll(attackPoint.position, AttackRadius, BossLayer);

        foreach (Collider2D boss in hitboss)
        {
            boss.GetComponent<Boss_Shooting>().BossHealth(hpdamage);
            CameraShaker.Instance.ShakeOnce(7f, 50f, .1f, 1f);
            //BloodParticlePlay();
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
            bullet.GetComponent<kola>().trailParticle.Stop();
            bullet.GetComponent<SpriteRenderer>().enabled = false;
            bullet.GetComponent<BoxCollider2D>().enabled = false;
            bullet.GetComponent<kola>().bulletLight.SetActive(false);
            bullet.GetComponent<kola>().DeflectDieCaller();
            //bullet.GetComponent<kola>().Die();
            CameraShaker.Instance.ShakeOnce(2f, 25f, .1f, 1f);
            ParticlePlay();
            StartCoroutine(DeflectLight());
        }

        Collider2D[] deflectSword = Physics2D.OverlapBoxAll(DeflectPoint2.position, boyut, 0f, SwordLayer);

        foreach (Collider2D sword in deflectSword)
        {
            sword.GetComponentInParent<Enemy>().TakeDamage(0, deflectposturedamage);
            ParticlePlay();
            if (sword.GetComponentInParent<Enemy>().CurrentPosture < sword.GetComponentInParent<Enemy>().MaxPosture)
            {
                Debug.Log("normal deflect sound");
            }
            else if (sword.GetComponentInParent<Enemy>().CurrentPosture >= sword.GetComponentInParent<Enemy>().MaxPosture)
            {
                Debug.Log("stun by deflect sound");
            }
        }

        Collider2D[] deflectbossSword = Physics2D.OverlapBoxAll(DeflectPoint2.position, boyut, 0f, BossSwordLayer);

        foreach (Collider2D bosssword in deflectbossSword)
        {
            bosssword.GetComponentInParent<Boss_Shooting>().BossHealth(hpdamage);
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
    }

    IEnumerator DeflectChargedParticle()
    {
        chargingParticle.Play();
        yield return new WaitForSeconds(.25f);
        chargedParticle.Play();
    }


    /*void BloodParticlePlay()
    {
        if (sarpAttackDirectionCounter % 2 == 0) bloodParticle1.Play();
        if(sarpAttackDirectionCounter % 2 == 1) bloodParticle2.Play();
    }*/

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

    public void ParticleStopper()
    {
        if (deathblowSound == false)
            chargedParticle.Stop();
    }

    public void EnemyStunned()
    {
        camZoom = true;
        CameraShaker.Instance.ShakeOnce(2f, 25f, .1f, 1f);
        StartCoroutine(DeflectChargedParticle());
        StartCoroutine(CamZoom());
        StartCoroutine(DeflectLight());
        DamageIncrease(15f);
    }

    public void DamageIncrease(float plusdamage)
    {
        tempdmg = hpdamage;
        hpdamage = hpdamage + plusdamage;
    }

    public void DamageDecrease()
    {
        hpdamage = tempdmg;
    }
}
