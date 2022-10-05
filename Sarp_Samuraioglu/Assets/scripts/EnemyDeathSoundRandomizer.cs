using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSoundRandomizer : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    public void SarpKillsEnemy()
    {
        source.clip = sounds[Random.Range(0, 5)];
        source.PlayOneShot(source.clip);
    }

    public void SarpStunDeflect()
    {
        source.clip = sounds[Random.Range(6, 9)];
        source.PlayOneShot(source.clip);
    }

    public void SarpMashesEnemy()
    {
        source.clip = sounds[Random.Range(10, 11)];
        source.PlayOneShot(source.clip);
    }

    public void CallEnemyIEnumerator()
    {
        StartCoroutine(EnemySwordSwing());
    }

    public IEnumerator EnemySwordSwing()
    {
        yield return new WaitForSeconds(.5f);
        source.clip = sounds[Random.Range(12, 13)];
        source.PlayOneShot(source.clip);
    }
    public void stopcorputines()
    {
        StopAllCoroutines();
    }
}
