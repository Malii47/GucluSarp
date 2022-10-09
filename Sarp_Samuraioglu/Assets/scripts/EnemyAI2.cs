using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI2 : MonoBehaviour
{
    public float speed;
    public float retreatDistance;
    public float stoppingDistance;
    public Animator legAnim;

    int parametrelegWalk = Animator.StringToHash("legWalk");

    public Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

        if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            legAnim.SetBool(parametrelegWalk, false);
            Debug.Log("stopping");
        }

        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            legAnim.SetBool(parametrelegWalk, true);
            Debug.Log("retreat");
        }
        else
        {
            legAnim.SetBool(parametrelegWalk, true);
        }
    }
}
