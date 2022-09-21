using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SwordEnemyAI : MonoBehaviour
{
    Animator anim;
    public BoxCollider2D cd;
    public Transform target;
    public Transform attackPoint;
    public LayerMask playerLayer;

    public float attackRadius = 0.5f;
    public float speed;
    public float stoppingDist;
    public float attackDist;
    bool a = true;
    bool b = true;

    void OnEnable()
    {
        b = true;
        a = true;
    }
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        //cd = GameObject.FindGameObjectWithTag("Sword").GetComponentInChildren<BoxCollider2D>();

        b = true;
        a = true;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < attackDist)
        {
            if (b)
            {
                a = true;
                MainAttack();
                b = false;
            }
        }
        else if (Vector2.Distance(transform.position, target.position) > attackDist)
        {
            anim.SetBool("walkBool", true);
        }

        
    }
    void MainAttack()
    {
        if (a)
        {
            anim.SetBool("attackBool", true);
            anim.SetBool("walkBool", false);
            StartCoroutine(Attacking());
        }
    }
    void Attack()
    {       
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDie>().DeathbySwordEnemy();
        }
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(.1f);
        cd.enabled = true;
        yield return new WaitForSeconds(.4f);
        cd.enabled = !cd.enabled;
        yield return null;
        Attack();
        yield return new WaitForSeconds(.27f);
        anim.SetBool("attackBool", false);
        StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1);
        b = true;
        a = false;
    }
    public void stoppingIEnumerators()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
    
}

