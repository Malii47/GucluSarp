using Newtonsoft.Json.Bson;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStun : MonoBehaviour
{
    Animator anim;
    public GameObject StunLight;
    public GameObject EnemyLight;
    public GameObject EnemyLeg;
    public GameObject KatanaLight;
    public Collider2D col;
    public Transform deathblowAreaCenter;
    public float deathblowAreaRadius;
    public LayerMask playerLayer;
    public float damage = 5f;
    int parametreattackBool = Animator.StringToHash("attackBool");
    int parametrewalkBool = Animator.StringToHash("walkBool");
    int parametrestunTrigger = Animator.StringToHash("stunTrigger");
    int parametreFadeInTrigger = Animator.StringToHash("FadeIn");
    int parametreFadeOutTrigger = Animator.StringToHash("FadeOut");
    int parametreStunTriggerExit = Animator.StringToHash("stunTriggerExit");

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    public void Stun()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().chargeBool = true;
        KatanaLight.SetActive(true);
        KatanaLight.GetComponent<Animator>().SetTrigger(parametreFadeInTrigger);
        col.isTrigger = true;
        GetComponentInChildren<EnemyDeathSoundRandomizer>().stopcorputines();
        GetComponentInChildren<EnemyDeathSoundRandomizer>().SarpStunDeflect();
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<SwordEnemyAI>().stoppingIEnumerators();
        GetComponent<SwordEnemyAI>().enabled = false;
        EnemyLeg.GetComponent<Animator>().enabled = false;
        EnemyLeg.GetComponent<Renderer>().enabled = false;

        anim.SetBool(parametreattackBool, false);
        anim.SetBool(parametrewalkBool, false);
        anim.SetTrigger(parametrestunTrigger);

        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyLookDirSword>().enabled = false;
        StartCoroutine(StunTime());
    }


    IEnumerator StunTime()
    {
        yield return new WaitForSeconds(.2f);
        StunLight.SetActive(true);

        

        StunLight.GetComponent<Animator>().SetTrigger(parametreFadeInTrigger);
        EnemyLight.GetComponent<Animator>().SetTrigger(parametreFadeOutTrigger);
        yield return new WaitForSeconds(.15f);
        EnemyLight.SetActive(false);
        yield return new WaitForSeconds(2.55f);
        GetComponent<Enemy>().CurrentHealt += 10;
        GetComponent<AIDestinationSetter>().enabled = true;
        GetComponent<SwordEnemyAI>().enabled = true;
        EnemyLeg.GetComponent<Animator>().enabled = true;
        EnemyLeg.GetComponent<Renderer>().enabled = true;
        GetComponent<EnemyLookDirSword>().enabled = true;
        yield return null;
       
        anim.SetTrigger(parametreStunTriggerExit);
        EnemyLight.SetActive(true);

        StunLight.GetComponent<Animator>().SetTrigger(parametreFadeOutTrigger);
        EnemyLight.GetComponent<Animator>().SetTrigger(parametreFadeInTrigger);
        yield return new WaitForSeconds(.01f);
        StunLight.SetActive(false);
        col.isTrigger = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound = false;
        KatanaLight.SetActive(false);
    }
    public void Ineedsomesleep()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(deathblowAreaCenter.position, deathblowAreaRadius);
    }
}
