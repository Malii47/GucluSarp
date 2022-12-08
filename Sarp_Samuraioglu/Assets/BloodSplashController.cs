using DG.Tweening.Core.Easing;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BloodSplashController : MonoBehaviour
{
    public float count;
    public bool bloodSplashManager;
    public bool bloodSplashManager2;
    public bool reverterBool;
    int parametreBloodSplash = Animator.StringToHash("BloodSplash");
    int parametreBloodSplash2 = Animator.StringToHash("BloodSplash2");
    int parametreBloodSplash3 = Animator.StringToHash("BloodSplash3");
    int parametreBloodSplashMirror = Animator.StringToHash("BloodSplashMirror");
    int parametreBloodSplashMirror2 = Animator.StringToHash("BloodSplashMirror2");
    int parametreBloodSplashMirror3 = Animator.StringToHash("BloodSplashMirror3");
    Animator anim;
    public Transform splashPoint;
    public Transform temp;
    public Transform SplashRevertingArea;
    public Vector2 boyut_SplashRevertingArea;
    public LayerMask playerLayer;

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
                Collider2D[] splash = Physics2D.OverlapBoxAll(SplashRevertingArea.position, boyut_SplashRevertingArea, 0f, playerLayer);

                foreach (Collider2D sarpingen in splash)
                {
                    reverterBool = true;
                }

                if (reverterBool)
                {
                    Debug.Log("yarrak2");
                    if (count % 2 == 0)
                    {
                        RandomParticlePlayer1();
                        bloodSplashManager = false;
                    }

                    if (count % 2 == 1)
                    {
                        RandomParticlePlayer0();
                        bloodSplashManager = false;
                    }
                }
                else
                {
                    Debug.Log("yarrak2");
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
}
