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
    public float temphp;
    public float countt;
    int parametreattackBool = Animator.StringToHash("attackBool");
    int parametrewalkBool = Animator.StringToHash("walkBool");
    int parametrestunTrigger = Animator.StringToHash("stunTrigger");
    int parametreFadeInTrigger = Animator.StringToHash("FadeIn");
    int parametreFadeOutTrigger = Animator.StringToHash("FadeOut");
    int parametreStunTriggerExit = Animator.StringToHash("stunTriggerExit");

    private void Start()
    {
        anim = GetComponent<Animator>();
        countt = 0;
    }

    public void Stun()
    {
        StunPosition();
        GetComponentInChildren<BloodSplashController>().SplashPointPositioner();
        GetComponentInChildren<BloodSplashController>().bloodSplashManager2 = true;
        GameObject.Find("GameController").GetComponent<StunCounter>().counter++;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().EnemyStunned();
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound == false)
        {
            KatanaLight.SetActive(true);
            KatanaLight.GetComponent<Animator>().SetTrigger(parametreFadeInTrigger);
        }        
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound = true;
        col.isTrigger = true;
        GetComponentInChildren<EnemyDeathSoundRandomizer>().stopcorputines();
        //GetComponentInChildren<EnemyDeathSoundRandomizer>().SarpStunDeflect();

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
        temphp = GetComponent<Enemy>().CurrentHealt;
        GetComponent<Enemy>().CurrentHealt = 1;
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
        if (GetComponent<Enemy>().CurrentPosture >= GetComponent<Enemy>().MaxPosture) GetComponent<Enemy>().CurrentPosture = 0;
        GetComponent<Enemy>().CurrentHealt = temphp;
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
        if (GameObject.Find("GameController").GetComponent<StunCounter>().counter == 1)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().deathblowSound = false;
            KatanaLight.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().ParticleStopper();
        }
        GameObject.Find("GameController").GetComponent<StunCounter>().counter--;
        GetComponentInChildren<BloodSplashController>().SplashPointPositionReverter();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().DamageDecrease();
        GetComponentInChildren<BloodSplashController>().bloodSplashManager2 = false;
        yield return new WaitForSeconds(1.5f);
        countt = 0;
    }

    public void stoppingIEnumerators()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(deathblowAreaCenter.position, deathblowAreaRadius);
    }
    void StunPosition()
    {
        Vector3 pos = transform.position;
        pos.z = GameObject.Find("GameController").GetComponent<DeathPosition>().PositionStacker();
        transform.position = pos;
    }
}
