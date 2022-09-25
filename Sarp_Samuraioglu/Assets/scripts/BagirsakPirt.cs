using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagirsakPirt : MonoBehaviour
{
    Animator anim;

    public Transform point_pant;
    public Transform point_bowel;
    public bool a, b;
    public Vector2 boyut_pant;
    public Vector2 boyut_bowel;
    public LayerMask playerLayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        a = false;
        b = false;
    }

    void Update()
    {

        if (a)
        {
            Collider2D[] playerstep = Physics2D.OverlapBoxAll(point_pant.position, boyut_pant, 0f, playerLayer);

            foreach (Collider2D sarpingen in playerstep)
            {
                anim.SetTrigger("pant");
                GetComponent<BagirsakPirt>().enabled = false;
            }
        }
        if (b)
        {
            Collider2D[] playerstep2 = Physics2D.OverlapBoxAll(point_bowel.position, boyut_bowel, 0f, playerLayer);

            foreach (Collider2D sarpingen2 in playerstep2)
            {
                anim.SetTrigger("bowel");
                GetComponent<BagirsakPirt>().enabled = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(point_pant.position, boyut_pant);
        //Gizmos.DrawCube(point_bowel.position, boyut_bowel);
    }
}
