using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BagirsakPirt2 : MonoBehaviour
{
    public Animator anim;
    Animator animo;
    public Transform point_head;
    public bool headBool;
    bool mashActivateBool;
    public Vector2 boyut_head;
    public LayerMask playerLayer;
    public GameObject mash;

    void Start()
    {
        animo = GetComponent<Animator>();
        headBool = false;
    }

    private void Update()
    {
        if (headBool)
        {
            StartCoroutine(DeathMasher());
            if (mashActivateBool)
            {
                Collider2D[] playerstep = Physics2D.OverlapBoxAll(point_head.position, boyut_head, 0f, playerLayer);
                foreach (Collider2D sarpingen in playerstep)
                {
                    //mash.GetComponent<EnemyDeathSoundRandomizer>().SarpMashesEnemy();
                    int parametrehead = Animator.StringToHash("head");
                    anim.SetTrigger(parametrehead);
                }
            }
        }
    }

    IEnumerator DeathMasher()
    {
        yield return new WaitForSeconds(2.1f);
        animo.SetBool("lake", true);
        mashActivateBool = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(point_head.position, boyut_head);
    }

}
