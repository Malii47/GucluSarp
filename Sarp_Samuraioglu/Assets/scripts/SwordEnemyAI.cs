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
    public Animator legAnim;
    public GameObject swordSwing;
    public BoxCollider2D cd;
    public Transform target;
    public Transform attackPoint;
    public LayerMask playerLayer;

    public float attackRadius = 0.5f;
    public float speed;
    public float stoppingDist;
    public float attackDist;
    bool a = true;
    bool oneTimeExecution = true;

    int parametreattackBool = Animator.StringToHash("attackBool");
    int parametrewalkBool = Animator.StringToHash("walkBool");
    int parametrelegWalk = Animator.StringToHash("legWalk");

    void OnEnable()
    {
        oneTimeExecution = true;
        a = true;
    }
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        //cd = GameObject.FindGameObjectWithTag("Sword").GetComponentInChildren<BoxCollider2D>();

        oneTimeExecution = true;
        a = true;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < attackDist)
        {
            if (oneTimeExecution)
            {
                a = true;
                MainAttack();
                oneTimeExecution = false;
            }
        }
        else if (Vector2.Distance(transform.position, target.position) > attackDist)
        {
            anim.SetBool(parametrewalkBool, true);
        }

        if (Vector2.Distance(transform.position, target.position) > stoppingDist)
        {
            legAnim.SetBool(parametrelegWalk, true);
        }
        else if (Vector2.Distance(transform.position, target.position) < stoppingDist)
        {
            legAnim.SetBool(parametrelegWalk, false);
        }
    }
    void MainAttack()
    {
        if (a)
        {
            swordSwing.GetComponent<EnemyDeathSoundRandomizer>().CallEnemyIEnumerator();
            anim.SetBool(parametreattackBool, true);
            anim.SetBool(parametrewalkBool, false);
            StartCoroutine(DeflectTimeThenAttack());
        }
    }
    

    IEnumerator DeflectTimeThenAttack()
    {
        yield return new WaitForSeconds(.1f);
        anim.SetBool(parametreattackBool, false);
        cd.enabled = true;
        yield return new WaitForSeconds(.4f);
        cd.enabled = !cd.enabled;
        yield return null;
        Attack();
        yield return new WaitForSeconds(.27f);
        StartCoroutine(NextAttackDelay());
    }
    void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDie>().DeathbySwordEnemy();
        }
    }

    IEnumerator NextAttackDelay()
    {
        yield return new WaitForSeconds(1);
        oneTimeExecution = true;
        a = false;
    }
    public void stoppingIEnumerators()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
    
}

