using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagirsakPirt : MonoBehaviour
{
    Animator anim;

    public Transform point_pant;
    public Transform point_bowel;
    public bool pantBool, bowelBool;
    public Vector2 boyut_pant;
    public Vector2 boyut_bowel;
    public LayerMask playerLayer;
    public GameObject mash;
    void Start()
    {
        anim = GetComponent<Animator>();
        pantBool = false;
        bowelBool = false;
    }

    void Update()
    {

        if (pantBool)
        {
            Collider2D[] playerstep = Physics2D.OverlapBoxAll(point_pant.position, boyut_pant, 0f, playerLayer);

            foreach (Collider2D sarpingen in playerstep)
            {
                mash.GetComponent<EnemyDeathSoundRandomizer>().SarpMashesEnemy();
                int parametrepant = Animator.StringToHash("pant");
                anim.SetTrigger(parametrepant);
                GetComponent<BagirsakPirt>().enabled = false;
                
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
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(point_pant.position, boyut_pant);
        //Gizmos.DrawCube(point_bowel.position, boyut_bowel);
    }
}
