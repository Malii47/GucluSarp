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
    public Collider2D col;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Stun()
    {
        col.isTrigger = true;
        GetComponentInChildren<EnemyDeathSoundRandomizer>().stopcorputines();
        GetComponentInChildren<EnemyDeathSoundRandomizer>().SarpStunDeflect();
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<SwordEnemyAI>().stoppingIEnumerators();
        GetComponent<SwordEnemyAI>().enabled = false;
        EnemyLeg.GetComponent<Animator>().enabled = false;
        EnemyLeg.GetComponent<Renderer>().enabled = false;
        int parametreattackBool = Animator.StringToHash("attackBool");
        int parametrewalkBool = Animator.StringToHash("walkBool");
        int parametrestunTrigger = Animator.StringToHash("stunTrigger");

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
        StunLight.GetComponent<Animator>().SetTrigger("FadeIn");
        EnemyLight.GetComponent<Animator>().SetTrigger("FadeOut");
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
        anim.SetTrigger("stunTriggerExit");
        EnemyLight.SetActive(true);
        StunLight.GetComponent<Animator>().SetTrigger("FadeOut");
        EnemyLight.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(.01f);
        StunLight.SetActive(false);
        col.isTrigger = false;
    }
    public void Ineedsomesleep()
    {
        StopAllCoroutines();
    }

        
}
