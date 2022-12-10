using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyGunDying : MonoBehaviour
{
    public float maxhealth = 10f;
    public float CurrentHealt;
    public float maxPosture;
    public float CurrentPosture;
    public Animator animator;
    public Animator gunLegAnimator;
    public GameObject gunEnemyLight;
    public GameObject KatanaLight;
    public LayerMask playerLayer;
    public Transform bloodpoint_gun;
    public float bloodarea_radius;
    public bool oneTimeExecutionDarkRedPrint;
    int parametrelegWalk = Animator.StringToHash("legWalk");



    void Start()
    {
        CurrentHealt = maxhealth;
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

    public void TakeDamage(float damage, float posture)
    {
        CurrentHealt = CurrentHealt - damage;
        CurrentPosture = CurrentPosture - posture;

        if (CurrentHealt <= 0)
            Die31();

        if(CurrentPosture >= maxPosture)
        {
            //Stun
        }
    }
    /*void anan1()
    {
        Vector3 pos = transform.position;
        pos.z = 0.5f;
        transform.position = pos;
    }*/

    void Die31()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().ParticleStopper();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound = false;
        KatanaLight.SetActive(false);
        StartCoroutine(GunEnemyDeathLight());
        int parametredeath = Animator.StringToHash("Death");
        animator.SetTrigger(parametredeath);
        GetComponentInChildren<EnemyGunRandomizerTemp>().SarpKillsGunEnemy();

        Vector3 pos = transform.position;
        pos.z = GameObject.Find("GameController").GetComponent<DeathPosition>().PositionStacker();
        transform.position = pos;

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
}
