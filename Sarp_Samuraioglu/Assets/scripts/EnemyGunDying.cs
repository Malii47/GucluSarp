using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyGunDying : MonoBehaviour
{
    public float CurrentHealt;
    public float maxPosture;
    public float CurrentPosture;
    public float countt;
    float Timer;
    public Animator animator;
    public Animator gunLegAnimator;
    public GameObject gunEnemyLight;
    public GameObject KatanaLight;
    public LayerMask playerLayer;
    public Transform bloodpoint_gun;
    public float bloodarea_radius;
    public bool oneTimeExecutionDarkRedPrint;
    public bool posturedecrease;
    int parametrelegWalk = Animator.StringToHash("legWalk");



    void Start()
    {
        countt = 0;
        GetComponent<EnemyShooting>().enabled = true;
        //GetComponent<EnemyShooting>().kola.SetActive(true);
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (oneTimeExecutionDarkRedPrint)
        {
            Collider2D[] bloodarea = Physics2D.OverlapCircleAll(bloodpoint_gun.position, bloodarea_radius, playerLayer);
            foreach (Collider2D player in bloodarea)
            {
                GameObject.FindGameObjectWithTag("Bacak").GetComponent<Bacak_Animation>().oneTimeDarkRedPrint = true;
                oneTimeExecutionDarkRedPrint = false;
            }
        }
    }

    public void TakeDamage(float hpdamage, float posturedamage)
    {
        posturedecrease = false;
        CurrentHealt = CurrentHealt - hpdamage;
        CurrentPosture = CurrentPosture + posturedamage;
        if (CurrentPosture < maxPosture) Invoke("A", 1.5f);
        /*
        if (CurrentHealt <= 0 && CurrentPosture >= maxPosture)
        {
            //StunDeath
        }
        */
        if (CurrentHealt <= 0)
        {
            Die31();
        }

        else if (CurrentPosture >= maxPosture)
        {
            Stun31();
        }
    }

    void Stun31()
    {
        Debug.Log("STUNNED");
    }

    void Die31()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().ParticleStopper();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound = false;
        KatanaLight.SetActive(false);
        StartCoroutine(GunEnemyDeathLight());
        int parametredeath = Animator.StringToHash("Death");
        animator.SetTrigger(parametredeath);
        GetComponentInChildren<EnemyGunRandomizerTemp>().SarpKillsGunEnemy();
        DeathPosition();
        gunLegAnimator.SetBool(parametrelegWalk, false);
        GetComponentInChildren<BagirsakPirt2>().headBool = true;
        GetComponent<EnemyAI2>().enabled = false;
        GetComponent<EnemyLookDir>().enabled = false;
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyShooting>().stopIEnumerator();
        GetComponent<EnemyShooting>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Invoke("bloodprintdelayer", 1f);
    }
    void DeathPosition()
    {
        Vector3 pos = transform.position;
        pos.z = GameObject.Find("GameController").GetComponent<DeathPosition>().PositionStacker();
        transform.position = pos;
    }

    private void bloodprintdelayer()
    {
        oneTimeExecutionDarkRedPrint = true;
    }

    IEnumerator GunEnemyDeathLight()
    {
        gunEnemyLight.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(.25f);
        gunEnemyLight.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (posturedecrease)
        {
            Timer = Time.time;

            if (Timer % 1 == 0)
            {
                if (CurrentPosture == 0)
                {
                    posturedecrease = false;
                }
                else CurrentPosture--;
            }
        }
    }

    public void A()
    {
        posturedecrease = true;
    }
}
