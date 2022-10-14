using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bacak_Animation : MonoBehaviour
{
    public GameObject bloodyfootprint_r;
    public GameObject bloodyfootprint_l;
    public GameObject bloodyfootprint_r2;
    public GameObject bloodyfootprint_l2;
    public Transform bloodyfootprint_position;
    Animator animator;
    public bool oneTimeDarkRedPrint;
    public bool PlayerWalking;
    public bool oneTimeLightRedPrint;


    public float bloodyStepCounter;

    void Start()
    {
        animator = GetComponent<Animator>();
        bloodyStepCounter = 1;
        oneTimeDarkRedPrint = false;
        oneTimeLightRedPrint = false;
    }

    void Update()
    {
        if (oneTimeDarkRedPrint)
        { 
            if (bloodyStepCounter < 5)
            {
                if (PlayerWalking)
                {
                    if (bloodyStepCounter % 2 == 1)
                    {

                        GameObject foot_r = ObjectPool.SharedInstance.GetPooledFoot_r();
                        if (foot_r != null)
                        {
                            foot_r.transform.position = bloodyfootprint_position.position;
                            foot_r.transform.rotation = bloodyfootprint_position.rotation;
                            foot_r.SetActive(true);
                        }

                        StartCoroutine(bloodystep());
                        oneTimeDarkRedPrint = false;
                    }
                    else if (bloodyStepCounter % 2 == 0)
                    {

                        GameObject foot_l = ObjectPool.SharedInstance.GetPooledFoot_l();
                        if (foot_l != null)
                        {
                            foot_l.transform.position = bloodyfootprint_position.position;
                            foot_l.transform.rotation = bloodyfootprint_position.rotation;
                            foot_l.SetActive(true);
                        }

                        StartCoroutine(bloodystep());
                        oneTimeDarkRedPrint = false;
                    }
                }
            }
            if (bloodyStepCounter >= 5)
            {
                bloodyStepCounter = 1;
                //GameObject.Find("Enemy_Sword").GetComponent<Enemy_Death>().oneTimeExecutionDarkRedPrint = true;
                GameObject [] enemys = GameObject.FindGameObjectsWithTag("SwordEnemy");
                foreach (GameObject enemy in enemys)
                {
                    if (enemy.GetComponent<Enemy_Death>().deadstun == true)
                    {
                        enemy.GetComponent<Enemy_Death>().oneTimeExecutionDarkRedPrint = true;
                        oneTimeDarkRedPrint = false;
                    }
                        
                }

                GameObject[] gunenemys = GameObject.FindGameObjectsWithTag("GunEnemy");
                foreach (GameObject gunenemy in gunenemys)
                {
                    gunenemy.GetComponent<EnemyGunDying>().oneTimeExecutionDarkRedPrint = true;
                    oneTimeDarkRedPrint = false;   
                }
            }
        }
        if (oneTimeLightRedPrint)
        {
            if (bloodyStepCounter < 5)
            {
                if (PlayerWalking)
                {
                    if (bloodyStepCounter % 2 == 1)
                    {

                        GameObject foot_r2 = ObjectPool.SharedInstance.GetPooledLightFoot_r();
                        if (foot_r2 != null)
                        {
                            foot_r2.transform.position = bloodyfootprint_position.position;
                            foot_r2.transform.rotation = bloodyfootprint_position.rotation;
                            foot_r2.SetActive(true);
                        }

                        StartCoroutine(bloodystep2());
                        oneTimeLightRedPrint = false;
                    }
                    else if (bloodyStepCounter % 2 == 0)
                    {

                        GameObject foot_l2 = ObjectPool.SharedInstance.GetPooledLightFoot_l();
                        if (foot_l2 != null)
                        {
                            foot_l2.transform.position = bloodyfootprint_position.position;
                            foot_l2.transform.rotation = bloodyfootprint_position.rotation;
                            foot_l2.SetActive(true);
                        }

                        StartCoroutine(bloodystep2());
                        oneTimeLightRedPrint = false;
                    }
                }
            }
            if (bloodyStepCounter >= 5)
            {
                bloodyStepCounter = 1;
                //GameObject.Find("Enemy_Sword").GetComponent<Enemy_Death>().oneTimeExecutionLightRedPrint = true;
                GameObject[] enemys2 = GameObject.FindGameObjectsWithTag("SwordEnemy");
                foreach (GameObject enemy2 in enemys2)
                {
                    if (enemy2.GetComponent<Enemy_Death>().deadnormal == true)
                    {
                        enemy2.GetComponent<Enemy_Death>().oneTimeExecutionLightRedPrint = true;
                        oneTimeLightRedPrint = false;
                    }
                }
            }
        }
    }


    IEnumerator bloodystep()
    {
        yield return new WaitForSeconds(0.5f);
        bloodyStepCounter++;
        oneTimeDarkRedPrint = true;
    }
    IEnumerator bloodystep2()
    {
        yield return new WaitForSeconds(0.5f);
        bloodyStepCounter++;
        oneTimeLightRedPrint = true;
    }

    public void Anan(bool a)
    {
        int parametreisWalking = Animator.StringToHash("isWalking");

        if (a)
        {
            animator.SetBool(parametreisWalking, true);
            PlayerWalking = true;
        }
        else
        {
            animator.SetBool(parametreisWalking, false);
            PlayerWalking = false;
        }
    }
}
