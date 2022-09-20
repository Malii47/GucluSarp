using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordEnemyAI : MonoBehaviour
{
    Animator anim;
    public Transform target;
    public Transform attackPoint;
    public LayerMask playerLayer;

    public float attackRadius = 0.5f;
    public float speed;
    public float stoppingDist;
    public float attackDist;

    bool a;
    bool b;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();


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
            anim.SetBool("walkBool", false);
            anim.SetBool("attackBool", true);
            Debug.Log("Attacking");
            StartCoroutine(Attacking());
        }
    }
    void Attack()
    {       
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("Hit by Enemy");
        }
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(.5f);
        Attack();
        yield return new WaitForSeconds(.28f);
        anim.SetBool("attackBool", false);
        StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1);
        b = true;
        a = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
}

