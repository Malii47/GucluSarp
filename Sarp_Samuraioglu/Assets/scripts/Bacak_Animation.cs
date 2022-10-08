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
                        Instantiate(bloodyfootprint_r, bloodyfootprint_position.position, Quaternion.identity);
                        StartCoroutine(bloodystep());
                        oneTimeDarkRedPrint = false;
                    }
                    else if (bloodyStepCounter % 2 == 0)
                    {
                        Instantiate(bloodyfootprint_l, bloodyfootprint_position.position, Quaternion.identity);
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
                        Instantiate(bloodyfootprint_r2, bloodyfootprint_position.position, Quaternion.identity);
                        StartCoroutine(bloodystep2());
                        oneTimeLightRedPrint = false;
                    }
                    else if (bloodyStepCounter % 2 == 0)
                    {
                        Instantiate(bloodyfootprint_l2, bloodyfootprint_position.position, Quaternion.identity);
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
