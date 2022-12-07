using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplashController : MonoBehaviour
{
    public float count;
    public bool bloodSplashManager;
    int parametreBloodSplash = Animator.StringToHash("BloodSplash");
    int parametreBloodSplash2 = Animator.StringToHash("BloodSplash2");
    int parametreBloodSplash3 = Animator.StringToHash("BloodSplash3");
    int parametreBloodSplashMirror = Animator.StringToHash("BloodSplashMirror");
    int parametreBloodSplashMirror2 = Animator.StringToHash("BloodSplashMirror2");
    int parametreBloodSplashMirror3 = Animator.StringToHash("BloodSplashMirror3");
    Animator anim;
    public Transform splashPoint;
    public Transform temp;

    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        count = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>().sarpAttackDirectionCounter;
        if (bloodSplashManager)
        {
            float splash = Random.value;

            if (count % 2 == 0)
            {
                if (splash >= 0 && splash <= 0.33)
                    anim.SetTrigger(parametreBloodSplash);

                if (splash > 0.33 && splash <= 0.66)
                    anim.SetTrigger(parametreBloodSplash2);

                if (splash > 0.66 && splash <= 1)
                    anim.SetTrigger(parametreBloodSplash3);
            }

            if(count % 2 == 1)
            {
                if (splash >= 0 && splash <= 0.33)
                    anim.SetTrigger(parametreBloodSplashMirror);

                if (splash > 0.33 && splash <= 0.66)
                    anim.SetTrigger(parametreBloodSplashMirror2);

                if (splash > 0.66 && splash <= 1)
                    anim.SetTrigger(parametreBloodSplashMirror3);
            }

            bloodSplashManager = false;
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
}
