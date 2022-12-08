using DG.Tweening.Core.Easing;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BloodSplashController : MonoBehaviour
{
    public float count;
    public bool bloodSplashManager;
    public bool bloodSplashManager2;
    int parametreBloodSplash = Animator.StringToHash("BloodSplash");
    int parametreBloodSplash2 = Animator.StringToHash("BloodSplash2");
    int parametreBloodSplash3 = Animator.StringToHash("BloodSplash3");
    int parametreBloodSplashMirror = Animator.StringToHash("BloodSplashMirror");
    int parametreBloodSplashMirror2 = Animator.StringToHash("BloodSplashMirror2");
    int parametreBloodSplashMirror3 = Animator.StringToHash("BloodSplashMirror3");
    Animator anim;
    public Transform splashPoint;
    public Transform temp;
    public Transform player;
    public GameObject R;
    public GameObject L;
    public Vector2 boyut_R;
    public Vector2 boyut_L;
    public LayerMask katanaLayer;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        count = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().sarpAttackDirectionCounter;
        if (bloodSplashManager)
        {
            if (bloodSplashManager2)
            {
                Collider2D[] splash = Physics2D.OverlapBoxAll(R.transform.position, boyut_R, 0f, katanaLayer);
                foreach (Collider2D katana in splash)
                {
                    RandomParticlePlayer0();
                    L.SetActive(false);
                    bloodSplashManager = false;
                }

                Collider2D[] splash2 = Physics2D.OverlapBoxAll(L.transform.position, boyut_L, 0f, katanaLayer);
                foreach (Collider2D katana in splash2)
                {
                    RandomParticlePlayer1();
                    R.SetActive(false);
                    bloodSplashManager = false;
                }
            }
            
            else
            {
                Debug.Log("yarrak1");
                if (count % 2 == 0)
                {
                    RandomParticlePlayer0();
                    bloodSplashManager = false;
                }

                if (count % 2 == 1)
                {
                    RandomParticlePlayer1();
                    bloodSplashManager = false;
                }

            }
        }
    }

    public void SplashPointPositioner()
    {
        temp.position = this.transform.position;
        this.transform.position = splashPoint.position;
    }

    public void SplashPointPositionReverter()
    {
        this.transform.position = temp.position;
    }

    public void RandomParticlePlayer0()
    {
        float splash = Random.value;

        if (splash >= 0 && splash <= 0.33)
        {
            anim.SetTrigger(parametreBloodSplash);
            Debug.Log("0.1");
        }

        if (splash > 0.33 && splash <= 0.66)
        {
            anim.SetTrigger(parametreBloodSplash2);
            Debug.Log("0.2");
        }

        if (splash > 0.66 && splash <= 1)
        {
            anim.SetTrigger(parametreBloodSplash3);
            Debug.Log("0.3");
        }
    }
    public void RandomParticlePlayer1()
    {
        float splash = Random.value;

        if (splash >= 0 && splash <= 0.33)
        {
            anim.SetTrigger(parametreBloodSplashMirror);
            Debug.Log("1.1");
        }

        if (splash > 0.33 && splash <= 0.66)
        {
            anim.SetTrigger(parametreBloodSplashMirror2);
            Debug.Log("1.2");
        }

        if (splash > 0.66 && splash <= 1)
        {
            anim.SetTrigger(parametreBloodSplashMirror3);
            Debug.Log("1.3");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(R.transform.position, boyut_R);
        Gizmos.DrawCube(L.transform.position, boyut_L);
    }
}
