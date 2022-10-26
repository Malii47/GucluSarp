using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Shooting : MonoBehaviour
{
    Rigidbody2D rb;
    Rigidbody2D playerrb;
    public Animator anim;

    public Transform boss_shootposition;
    public Transform player;
    public Transform playerTracker;
    public Transform attackPoint;
    public BoxCollider2D cd;

    public LayerMask playerLayer;
    Vector2 PlayerPosition;
    Vector2 playertrackerPosition;

    bool startBool;
    bool dashLander;
    bool dashing;
    bool b;
    public bool deflected;

    public float moveSpeed;
    public float dashSpeed;
    public float attackDist;
    public float attackRadius;
    public float landDist;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerrb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        b = true;
        deflected = false;

        moveSpeed = GetComponent<AIPath>().maxSpeed;

        if (Random.value > 0.25) StartCoroutine(Shoot(2));
        else startBool = true;
    }

    void Update()
    {
        if (startBool)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > attackDist)
            {
                float a = Random.value;
                if (a >= 0.50) StartCoroutine(Shoot(0.4f));
                if (a < 0.50) StartCoroutine(Dash());
                startBool = false;
            }
        }
        
        if (Vector2.Distance(transform.position, player.transform.position) < attackDist)
        {
            if (b)
            {
                StartCoroutine(Attack());
                b = false;
            }
        }
        
        

        if (dashLander)
        {
            if (Vector2.Distance(transform.position, playerTracker.transform.position) <= landDist)
            {
                GetComponent<AIPath>().maxSpeed = moveSpeed;
                GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;

                Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
                foreach (Collider2D player in hitPlayer)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDie>().DeathbySwordEnemy();
                }

                dashing = false;
                if (Vector2.Distance(transform.position, player.position) > attackDist)
                {
                    StartCoroutine(Shoot(0));
                }
                dashLander = false;
            }
        }
    }

    void FixedUpdate()
    {
        PlayerPosition = playerrb.position;
        playertrackerPosition = playerTracker.position;

        if (!dashing)
        {
            Vector2 LookDir = PlayerPosition - rb.position;
            float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg + 90f;
            rb.rotation = angle;
        }
        else
        {
            Vector2 LookDir = playertrackerPosition - rb.position;
            float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg + 90f;
            rb.rotation = angle;
        }
    }

    IEnumerator Dash()
    {
        //Debug.Log("Dashing");
        yield return new WaitForSeconds(0.5f);

        GetComponent<AIPath>().maxSpeed = dashSpeed;

        //yield return new WaitForSeconds(0.3f);

        playerTracker.position = player.position;
        GetComponent<AIDestinationSetter>().target = playerTracker.transform;
        dashing = true;
        dashLander = true;

        yield return new WaitForSeconds(1.2f);

        startBool = true;
    }

    IEnumerator Shoot(float time)
    {
        //Debug.Log("Shooting");
        yield return new WaitForSeconds(time);

        GameObject bullet2 = ObjectPool.SharedInstance.GetPooledBullets();
        if (bullet2 != null)
        {
            bullet2.transform.position = boss_shootposition.position;
            bullet2.transform.rotation = boss_shootposition.rotation;
            bullet2.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);

        startBool = true;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);

        Debug.Log("DEFLECT!!!!!");
        deflected = false;
        cd.enabled = true;

        yield return new WaitForSeconds(0.5f);

        cd.enabled = !cd.enabled;

        if (deflected == false)
        {
            anim.SetTrigger("attack");

            yield return new WaitForSeconds(.3f);

            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
            foreach (Collider2D player in hitPlayer)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDie>().DeathbySwordEnemy();
            }
        }

        yield return new WaitForSeconds(2f);

        b = true;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
}
