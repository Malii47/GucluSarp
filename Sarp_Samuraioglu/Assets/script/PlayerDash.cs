using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed;
    public float startDashTime;

    public Rigidbody2D rigidBody;

    private float dashTime;
    private int direction;
    private float moveInput;
    // Start is called before the first frame update
    void Start()
    {
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if(moveInput < 0)
                {
                    direction = 1;
                }
                else if (moveInput > 0)
                {
                    direction = 2;
                }
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rigidBody.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    rigidBody.velocity = Vector2.left * dashSpeed;
                }
                else if(direction == 2)
                {
                    rigidBody.velocity = Vector2.right * dashSpeed;
                }
            }
        }
    }
}
