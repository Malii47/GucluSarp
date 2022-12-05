using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Transform dashattackPoint;
    public BoxCollider2D cd;

    public LayerMask playerLayer;
    Vector2 PlayerPosition;
    Vector2 playertrackerPosition;

    public bool startBool;
    bool dashLander;
    bool dashing;
    bool b;
    public bool deflected;

    public float moveSpeed;
    public float dashSpeed;
    public float attackDist;
    public float attackRadius;
    public float dashattackRadius;
    public float landDist; 
    public float maxhealth;
    public float CurrentHealt;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerrb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        b = true;
        deflected = false;
        CurrentHealt = maxhealth;

        moveSpeed = GetComponent<AIPath>().maxSpeed;

        if (Random.value > 0.99) StartCoroutine(Shoot(0,0));
        else startBool = true;
    }

    void Update()
    {
        if (startBool)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > attackDist)
            {
                float a = Random.value;

                if (a >= 0.55)
                {
                    Debug.Log("STOP");
                    Debug.Log("shooting");
                    StopAllCoroutines();
                    StartCoroutine(Shoot(1, 2));
                }
                if (a < 0.55)
                {
                    Debug.Log("STOP");
                    Debug.Log("dashing");
                    StopAllCoroutines();
                    StartCoroutine(Dash(1));
                }

                startBool = false;
            }
        }
        
        if (Vector2.Distance(transform.position, player.transform.position) < attackDist)
        {
            if (b)
            {
                StartCoroutine(Attack(1.5f));
                Debug.Log("attacking");
                b = false;
            }
        }

        if (dashLander)
        {
            if (Vector2.Distance(transform.position, playerTracker.transform.position) <= landDist)
            {
                GetComponent<AIPath>().maxSpeed = moveSpeed;
                GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;

                Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(dashattackPoint.position, dashattackRadius, playerLayer);
                foreach (Collider2D player in hitPlayer)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDie>().DeathbySwordEnemy();
                }

                dashing = false;
                b = true;
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

    public void BossHealth(float damage)
    {
        CurrentHealt = CurrentHealt - damage;

        if (CurrentHealt <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Dash(float entrytime)
    {
        yield return new WaitForSeconds(entrytime);

        GetComponent<AIPath>().maxSpeed = dashSpeed;
        b = false;

        //yield return new WaitForSeconds(0.3f);

        playerTracker.position = player.position;
        GetComponent<AIDestinationSetter>().target = playerTracker.transform;
        dashing = true;
        dashLander = true;

        yield return new WaitForSeconds(1);

        startBool = true;
    }

    IEnumerator Shoot(float entrytime, float exittime)
    {
        yield return new WaitForSeconds(entrytime);

        GameObject bullet2 = ObjectPool.SharedInstance.GetPooledBullets();
        if (bullet2 != null)
        {
            bullet2.transform.position = boss_shootposition.position;
            bullet2.transform.rotation = boss_shootposition.rotation;
            bullet2.SetActive(true);
        }

        b = true;

        yield return new WaitForSeconds(exittime);

        startBool = true;
    }

    IEnumerator Attack(float entrytime)
    {
        yield return new WaitForSeconds(entrytime);

        Debug.Log("DEFLECT!!!!!");
        deflected = false;
        cd.enabled = true;

        yield return new WaitForSeconds(0.5f);

        cd.enabled = false;

        if (deflected == false)
        {
            anim.SetTrigger("attack");

            yield return new WaitForSeconds(.3f);

            Debug.Log("swinging");
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
            foreach (Collider2D player in hitPlayer)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDie>().DeathbySwordEnemy();
            }
        }

        yield return new WaitForSeconds(1f);
        startBool = true;
        b = true;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(attackPoint.position, attackRadius);
        //Gizmos.DrawSphere(dashattackPoint.position, dashattackRadius);
        Gizmos.DrawSphere(transform.position, attackDist);
    }
}
