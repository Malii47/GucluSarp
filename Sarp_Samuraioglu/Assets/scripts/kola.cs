using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kola : MonoBehaviour
{
    /*[SerializeField]*/public  ParticleSystem particle = null;
    public ParticleSystem trailParticle;

    public float speed = 7f;
    PlayerMovement target;
    Vector2 moveDirection, PlayerPos;
    Rigidbody2D rb, playerrb;
    SpriteRenderer sp;
    BoxCollider2D cd;
    public GameObject bulletLight;
    bool a = false;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        cd = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        playerrb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        target = GameObject.FindObjectOfType<PlayerMovement>();

        PlayerPos = playerrb.position;

        Vector2 LookDir = PlayerPos - rb.position;
        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Invoke("Die",5f);
        trailParticle.Play();
        a = true;
    }

    private void OnEnable()
    {
        if (a)
        {
            
            PlayerPos = playerrb.position;
            this.sp.enabled = true;
            this.cd.enabled = true;
            bulletLight.SetActive(true);
            trailParticle.Play();

            Vector2 LookDir = PlayerPos - rb.position;
            float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;

            moveDirection = (target.transform.position - transform.position).normalized * speed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            Invoke("Die", 5f);
        }
    }

    public void Die()
    {
        bulletLight.SetActive(false);
        gameObject.SetActive(false);
    }

    public IEnumerator DeflectDie()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    public void DeflectDieCaller()
    {
        StartCoroutine(DeflectDie());
    }

    
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player") || col.transform.CompareTag("Obstacle"))
        {
            particle.Play();
            trailParticle.Stop();
            this.sp.enabled = false;
            this.cd.enabled = false;
            bulletLight.SetActive(false);
            Invoke("BackToPool", 1f);
        }
    }

    void BackToPool()
    {
        gameObject.SetActive(false);
    }
}
