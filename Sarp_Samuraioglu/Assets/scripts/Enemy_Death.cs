using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Death : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Death()
    {
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<SwordEnemyAI>().stoppingIEnumerators();
        GetComponent<SwordEnemyAI>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetBool("walkBool", false);
        anim.SetBool("attackBool", false);
        anim.SetTrigger("deathTrigger");
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyLookDirSword>().enabled = false;
    }
    public void StunDeath()
    {
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponent<SwordEnemyAI>().stoppingIEnumerators();
        GetComponent<EnemyStun>().Ineedsomesleep();
        GetComponent<SwordEnemyAI>().enabled = false;
        GetComponent<EnemyStun>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetBool("walkBool", false);
        anim.SetBool("attackBool", false);
        anim.SetTrigger("stundeathTrigger");
        GetComponent<AIDestinationSetter>().enabled = false;
        GetComponent<EnemyLookDirSword>().enabled = false;
    }
}
