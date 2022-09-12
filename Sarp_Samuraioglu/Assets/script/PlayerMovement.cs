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

    public float dashLength =.5f, dashCooldown=1f;

    private float dashCounter;
    private float dashCoolCounter;
    private float temp;
    [SerializeField] private TrailRenderer trailRenderer;

    Vector2 movement;
    Vector2 mousePos;

    bool walk;

    void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cam = GameObject.FindObjectOfType<Camera>();
        trailRenderer = GetComponent<TrailRenderer>();

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
                temp = moveSpeed;
                activeMoveSpeed = dashSpeed;
                moveSpeed = activeMoveSpeed;
                dashCounter = dashLength;
                trailRenderer.emitting = true;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                moveSpeed = temp;
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
                trailRenderer.emitting = false;
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
            GameObject.FindGameObjectWithTag("Bacak").GetComponent<Bacak_Animation>().Anan(false); }


    }
}
