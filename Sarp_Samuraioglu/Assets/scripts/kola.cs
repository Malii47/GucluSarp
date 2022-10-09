using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kola : MonoBehaviour
{
    [SerializeField] ParticleSystem particle = null;

    public float speed = 7f;
    PlayerMovement target;
    Vector2 moveDirection, PlayerPos;
    Rigidbody2D rb, playerrb;
    SpriteRenderer sp;
    BoxCollider2D cd;
    //public GameObject bulletLight;
    bool a = false;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        cd = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        playerrb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        //bulletLight = GameObject.Find("BulletLight");

        target = GameObject.FindObjectOfType<PlayerMovement>();

        PlayerPos = playerrb.position;

        Vector2 LookDir = PlayerPos - rb.position;
        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Invoke("Die",5f);
        a = true;
    }

    private void OnEnable()
    {
        if (a)
        {
            Vector2 LookDir = PlayerPos - rb.position;
            float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;

            moveDirection = (target.transform.position - transform.position).normalized * speed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            Invoke("Die", 5f);
            Debug.Log("yarak");
        }
        
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player") || col.transform.CompareTag("Obstacle"))
        {
            particle.Play();
            gameObject.SetActive(false);
        }
    }
}
