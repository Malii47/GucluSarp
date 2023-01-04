using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rigidBody;
    public Animator animator;
    public Camera cam;

    public float activeMoveSpeed;
    public float dashSpeed;
    public float raycastDistance;
    public bool a;
    public float dashLength =.5f, dashCooldown=1f;

    private float dashCounter;
    private float dashCoolCounter;
    private float temp;
    [SerializeField] public ParticleSystem dash;

    Vector2 movement;
    Vector2 mousePos;

    bool walk;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cam = GameObject.FindObjectOfType<Camera>();
        a = true;
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                GetComponentInChildren<SarpSwingsSword>().SarpDash();
                temp = moveSpeed;
                activeMoveSpeed = dashSpeed;
                moveSpeed = activeMoveSpeed;
                dashCounter = dashLength;
                dash.Play();
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                if (a)
                {
                    moveSpeed = temp;
                    activeMoveSpeed = moveSpeed;
                }
                else
                {
                    moveSpeed = 10f;
                }
                dashCoolCounter = dashCooldown;
                dash.Stop();
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        rigidBody.velocity = movement * moveSpeed;


        Vector2 lookDir = mousePos - rigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
        rigidBody.rotation = angle;

        if (movement.x != 0 || movement.y != 0)
        {
            GameObject.FindGameObjectWithTag("Bacak").GetComponent<Bacak_Animation>().Anan(true);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            GameObject.FindGameObjectWithTag("Bacak").GetComponent<Bacak_Animation>().Anan(false);
        }
    }
}
