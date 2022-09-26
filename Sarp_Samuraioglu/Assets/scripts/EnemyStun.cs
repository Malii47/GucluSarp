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

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Stun()
    {

        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<SwordEnemyAI>().stoppingIEnumerators();
        GetComponent<SwordEnemyAI>().enabled = false;
        GameObject.FindGameObjectWithTag("deneme").GetComponent<Animator>().enabled = false;
        GameObject.FindGameObjectWithTag("deneme").GetComponent<Renderer>().enabled = false;
        anim.SetBool("walkBool", false);
        anim.SetBool("attackBool", false);
        anim.SetTrigger("stunTrigger");
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
        GameObject.FindGameObjectWithTag("deneme").GetComponent<Animator>().enabled = true;
        GameObject.FindGameObjectWithTag("deneme").GetComponent<Renderer>().enabled = true;
        GetComponent<EnemyLookDirSword>().enabled = true;
        yield return null;
        anim.SetTrigger("stunTriggerExit");
        EnemyLight.SetActive(true);
        StunLight.GetComponent<Animator>().SetTrigger("FadeOut");
        EnemyLight.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(.01f);
        
        StunLight.SetActive(false);
    }
    public void Ineedsomesleep()
    {
        StopAllCoroutines();
    }


        
}
