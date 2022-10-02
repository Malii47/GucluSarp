using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSoundRandomizer : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    public float enemyHealth;
    public bool oneTimeExecution = true;
    public bool oneTimeExecution2 = true;
    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        enemyHealth = GameObject.Find("Enemy_Sword").GetComponent<Enemy>().CurrentHealt;
        if (oneTimeExecution)
        {
            if (enemyHealth == 25f || enemyHealth == 15f)
            {
                source.clip = sounds[Random.Range(0, 5)];
                source.PlayOneShot(source.clip);
                oneTimeExecution = false;
            }
        }
        if (oneTimeExecution2)
        {
            if (enemyHealth == 20f)
            {
                source.clip = sounds[Random.Range(6, 9)];
                source.PlayOneShot(source.clip);
                oneTimeExecution2 = false;
            }
        }
       
    }
}
