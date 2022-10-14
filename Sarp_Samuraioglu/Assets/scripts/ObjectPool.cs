using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    public List<GameObject> pooledBullets;
    public List<GameObject> pooledFoot_r;
    public List<GameObject> pooledFoot_l;
    public List<GameObject> pooledLightFoot_r;
    public List<GameObject> pooledLightFoot_l;

    public GameObject bullets;
    public GameObject bloodyFootPrint_r;
    public GameObject bloodyFootPrint_l;
    public GameObject bloodyLightFootPrint_r;
    public GameObject bloodyLightFootPrint_l;

    public int foot_rToPool;
    public int foot_lToPool;
    public int lightfoot_rToPool;
    public int lightfoot_lToPool;
    public int bulletsToPool;

    int count = 0;
    int count2 = 0;
    int count3 = 0;
    int count4 = 0;

    bool a;
    bool b;
    bool c;
    bool d;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledBullets = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < bulletsToPool; i++)
        {
            tmp = Instantiate(bullets);
            tmp.SetActive(false);
            pooledBullets.Add(tmp);
        }

        pooledFoot_r = new List<GameObject>();
        GameObject tmp2;
        for (int j = 0; j < foot_rToPool; j++)
        {
            tmp2 = Instantiate(bloodyFootPrint_r);
            tmp2.SetActive(false);
            pooledFoot_r.Add(tmp2);
        }

        pooledFoot_l = new List<GameObject>();
        GameObject tmp3;
        for (int k = 0; k < foot_lToPool; k++)
        {
            tmp3 = Instantiate(bloodyFootPrint_l);
            tmp3.SetActive(false);
            pooledFoot_l.Add(tmp3);
        }

        pooledLightFoot_r = new List<GameObject>();
        GameObject tmp4;
        for (int l = 0; l < lightfoot_rToPool; l++)
        {
            tmp4 = Instantiate(bloodyLightFootPrint_r);
            tmp4.SetActive(false);
            pooledLightFoot_r.Add(tmp4);
        }

        pooledLightFoot_l = new List<GameObject>();
        GameObject tmp5;
        for (int m = 0; m < lightfoot_lToPool; m++)
        {
            tmp5 = Instantiate(bloodyLightFootPrint_l);
            tmp5.SetActive(false);
            pooledLightFoot_l.Add(tmp5);
        }
    }

    public GameObject GetPooledBullets()
    {
        for (int i = 0; i < bulletsToPool; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }

    public GameObject GetPooledFoot_r()
    {
        for (int j = 0; j < foot_rToPool; j++)
        {
            if (!pooledFoot_r[j].activeInHierarchy)
            {
                if (a)
                {
                    if (count == foot_rToPool) count = 0;

                    pooledFoot_r[count].SetActive(false);
                    count++;
                }
                if (j == foot_rToPool - 2)
                {
                    a = true;
                }
                return pooledFoot_r[j];
            }
            
        }
        return null;
    }
    public GameObject GetPooledFoot_l()
    {
        for (int k = 0; k < foot_rToPool; k++)
        {
            if (!pooledFoot_l[k].activeInHierarchy)
            {
                if (b)
                {
                    if (count2 == foot_lToPool) count2 = 0;
                    pooledFoot_l[count2].SetActive(false);
                    count2++;
                }
                if (k == foot_lToPool - 2)
                {
                    b = true;
                }
                return pooledFoot_l[k];
            }
            
        }
        return null;
    }

    public GameObject GetPooledLightFoot_r()
    {
        for (int l = 0; l < lightfoot_rToPool; l++)
        {
            if (!pooledLightFoot_r[l].activeInHierarchy)
            {
                if (c)
                {
                    if (count3 == lightfoot_rToPool) count3 = 0;
                    pooledLightFoot_r[count3].SetActive(false);
                    count3++;
                }
                if (l == lightfoot_rToPool - 2)
                {
                    c = true;
                }
                return pooledLightFoot_r[l];
            }

        }
        return null;
    }

    public GameObject GetPooledLightFoot_l()
    {
        for (int m = 0; m < lightfoot_lToPool; m++)
        {
            if (!pooledLightFoot_l[m].activeInHierarchy)
            {
                if (d)
                {
                    if (count4 == lightfoot_lToPool) count4 = 0;
                    pooledLightFoot_l[count4].SetActive(false);
                    count4++;
                }
                if (m == lightfoot_lToPool - 2)
                {
                    d = true;
                }
                return pooledLightFoot_l[m];
            }

        }
        return null;
    }
}
