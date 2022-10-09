using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledBullets;
    public List<GameObject> pooledFoot_r;
    public List<GameObject> pooledFoot_l;
    public GameObject bloodyFootPrint_r;
    public GameObject bloodyFootPrint_l;
    public GameObject bullets;
    public int foot_rToPool;
    public int foot_lToPool;
    public int bulletsToPool;

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
                return pooledFoot_l[k];
            }
        }
        return null;
    }

}
