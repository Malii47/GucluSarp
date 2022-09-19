using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
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

    float timeBtwAttacks;
    public float starttimeBtwAttacks;

    bool a;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        timeBtwAttacks = starttimeBtwAttacks;

        a = true;
    }

    void Update()
    {
            
        /*if (Vector2.Distance(transform.position, target.position) > stoppingDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        else if (Vector2.Distance(transform.position, target.position) < stoppingDist)
        {
            transform.position = this.transform.position;
        }
        */

        if (Vector2.Distance(transform.position, target.position) < attackDist)
        {
            if (a)
            {
                anim.SetBool("attackBool", true);
                Debug.Log("Attacking");
                anim.SetBool("walkBool", false);
                Invoke("Attack", .5f);
                a= false;
            }
            if (!a)
            {
                if (timeBtwAttacks <= 0)
                {
                    Invoke("Attack", .5f);
                    Debug.Log("Attacking");
                    timeBtwAttacks = starttimeBtwAttacks;
                }
                else
                {
                    timeBtwAttacks -= Time.deltaTime;
                }
            }
        }
        else if (Vector2.Distance(transform.position, target.position) > attackDist)
        {
            anim.SetBool("attackBool", false);
            anim.SetBool("walkBool", true);
            a=true;
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
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
}

