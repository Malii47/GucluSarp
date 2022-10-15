using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject Object1;
    public GameObject Object2;
    public GameObject Object3;

    void Start()
    {
        StartCoroutine(ObstaclesOnStart());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Respawn")
        {
            for(int i = 0; i < collision.transform.childCount; i++)
            {
                collision.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Respawn")
        {
            for (int i = 0; i < collision.transform.childCount; i++)
            {
                collision.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    IEnumerator ObstaclesOnStart()
    {
        Object1.SetActive(true);
        Object2.SetActive(true);
        yield return null;
        AstarPath.active.Scan();
        yield return null;
        Object1.SetActive(false);
        Object2.SetActive(false);
    }
}
