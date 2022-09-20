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
    Camera cam;
    SpriteRenderer sp;
    Collider2D cd;

    void Start()
    {
        cd = GetComponent<Collider2D>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
        playerrb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        target = GameObject.FindObjectOfType<PlayerMovement>();

        PlayerPos = playerrb.position;

        Vector2 LookDir = PlayerPos - rb.position;
        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

        desteroy();
    }


    void Update()
    {

        

    }

    private void desteroy()
    {
        Destroy(gameObject, 10f);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player") || col.transform.CompareTag("Obstacle"))
        {
            cd.enabled = !cd.enabled;
            sp.enabled = !sp.enabled;
            particle.Play();
            Invoke("Die", 1f);
        }

    }
}
