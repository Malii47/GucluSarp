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
    public float temphp;
    float Timer;
    public Animator animator;
    public Animator gunLegAnimator;
    public GameObject gunEnemyLight;
    public GameObject KatanaLight;
    public GameObject EnemyLeg;
    public LayerMask playerLayer;
    public Transform bloodpoint_gun;
    public float bloodarea_radius;
    public bool oneTimeExecutionDarkRedPrint;
    public bool posturedecrease;
    int parametrelegWalk = Animator.StringToHash("legWalk");
    int parametrestun = Animator.StringToHash("Stun");
    int parametreStunTriggerExit = Animator.StringToHash("StunExit");
    int parametreFadeInTrigger = Animator.StringToHash("FadeIn");
    public Collider2D col;
    bool a;



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
        DeathPosition();
        GetComponentInChildren<GunBloodSplashController>().SplashPointPositioner();
        //GetComponentInChildren<GunBloodSplashController>().bloodSplashManager2 = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().EnemyStunned();
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound == false)
        {
            KatanaLight.SetActive(true);
            KatanaLight.GetComponent<Animator>().SetTrigger(parametreFadeInTrigger);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound = true;
        col.isTrigger = true;
        //GetComponentInChildren<EnemyGunRandomizerTemp>().stopcorputines();

        //GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<EnemyAI2>().enabled = false;
        EnemyLeg.GetComponent<Animator>().enabled = false;
        EnemyLeg.GetComponent<Renderer>().enabled = false;
        animator.SetTrigger(parametrestun);
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyLookDir>().enabled = false;
        GetComponent<EnemyShooting>().enabled = false;
        temphp = CurrentHealt;
        CurrentHealt = 1;
        StartCoroutine(StunTime31());

        Debug.Log("STUNNED");
    }

    IEnumerator StunTime31()
    {
        if (a)
        {
            yield return new WaitForSeconds(.2f);
            //StunLight.SetActive(true);
            //StunLight.GetComponent<Animator>().SetTrigger(parametreFadeInTrigger);
            //EnemyLight.GetComponent<Animator>().SetTrigger(parametreFadeOutTrigger);
            yield return new WaitForSeconds(.15f);
            //EnemyLight.SetActive(false);
            yield return new WaitForSeconds(2.55f);
            if (CurrentPosture >= maxPosture) CurrentPosture -= 10;
            CurrentHealt = temphp;
            GetComponent<AIDestinationSetter>().enabled = true;
            GetComponent<EnemyAI2>().enabled = true;
            GetComponent<EnemyShooting>().enabled = false;
            EnemyLeg.GetComponent<Animator>().enabled = true;
            EnemyLeg.GetComponent<Renderer>().enabled = true;
            GetComponent<EnemyLookDir>().enabled = true;
            yield return null;

            animator.SetTrigger(parametreStunTriggerExit);
            //EnemyLight.SetActive(true);

            //StunLight.GetComponent<Animator>().SetTrigger(parametreFadeOutTrigger);
            //EnemyLight.GetComponent<Animator>().SetTrigger(parametreFadeInTrigger);
            yield return new WaitForSeconds(.01f);
            //StunLight.SetActive(false);
            col.isTrigger = false;
            if (GameObject.Find("GameController").GetComponent<StunCounter>().counter == 1)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound = false;
                KatanaLight.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().ParticleStopper();
            }
            GameObject.Find("GameController").GetComponent<StunCounter>().counter--;
            GetComponentInChildren<GunBloodSplashController>().SplashPointPositionReverter();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().DamageDecrease();
            GetComponentInChildren<GunBloodSplashController>().bloodSplashManager2 = false;
            yield return new WaitForSeconds(1.5f);
            GetComponent<EnemyGunDying>().posturedecrease = true;
            countt = 0;
            Debug.Log("yarrrrrak");
        }
    }

    void Die31()
    {
        //StopCoroutine("StunTime31");
        a = false;
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
