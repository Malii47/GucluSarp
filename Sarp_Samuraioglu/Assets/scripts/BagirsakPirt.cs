using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagirsakPirt : MonoBehaviour
{
    Animator anim;
    public Animator animA;
    public Transform point_pant;
    public Transform point_bowel;
    public Transform point_head;
    public bool pantBool, bowelBool, headBool;
    public Vector2 boyut_pant;
    public Vector2 boyut_bowel;
    public Vector2 boyut_head;
    public LayerMask playerLayer;
    public GameObject mash;
    float count;
    public float count2;
    bool a = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        pantBool = false;
        bowelBool = false;
        headBool = false;
    }

    void Update()
    {
        count = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().sarpAttackDirectionCounter;

        if (pantBool)
        {
            Collider2D[] playerstep = Physics2D.OverlapBoxAll(point_pant.position, boyut_pant, 0f, playerLayer);

            foreach (Collider2D sarpingen in playerstep)
            {
                mash.GetComponent<EnemyDeathSoundRandomizer>().SarpMashesEnemy();
                int parametrepant = Animator.StringToHash("pant");
                anim.SetTrigger(parametrepant);
                GetComponent<BagirsakPirt>().enabled = false;
                animA.SetTrigger("empty");
            }
        }
        if (bowelBool)
        {
            Collider2D[] playerstep2 = Physics2D.OverlapBoxAll(point_bowel.position, boyut_bowel, 0f, playerLayer);

            foreach (Collider2D sarpingen2 in playerstep2)
            {
                mash.GetComponent<EnemyDeathSoundRandomizer>().SarpMashesEnemy();
                int parametrebowel = Animator.StringToHash("bowel");
                anim.SetTrigger(parametrebowel);
                GetComponent<BagirsakPirt>().enabled = false;
                animA.SetTrigger("empty");
            }
        }
        if (headBool)
        {
            if (a)
            {
                count2 = count;
                a = false;
            }
            Collider2D[] playerstep3 = Physics2D.OverlapBoxAll(point_head.position, boyut_head, 0f, playerLayer);
            foreach (Collider2D sarpingen3 in playerstep3)
            {
                
                mash.GetComponent<EnemyDeathSoundRandomizer>().SarpMashesEnemy();
                int parametrehead = Animator.StringToHash("head");
                int parametrehead2 = Animator.StringToHash("head2"); 
                if (count2 % 2 == 1)
                {
                    anim.SetTrigger(parametrehead2);
                }
                else if (count2 % 2 == 0)
                {
                    anim.SetTrigger(parametrehead);
                }
                GetComponent<BagirsakPirt>().enabled = false;

                animA.SetTrigger("empty");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(point_pant.position, boyut_pant);
        Gizmos.DrawCube(point_bowel.position, boyut_bowel);
        Gizmos.DrawCube(point_head.position, boyut_head);
    }
}
