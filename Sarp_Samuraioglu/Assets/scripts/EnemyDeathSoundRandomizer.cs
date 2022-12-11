using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSoundRandomizer : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    public GameObject PauseMenu;
    void Start()
    {
        source = GetComponent<AudioSource>();
        PauseMenu = GameObject.Find("PauseMenuManager");
    }

    void Update()
    {
        if (PauseMenu.GetComponent<PauseMenu>().gameIsPaused == false)
        {
            source.Play();
        }
        else
        {
            source.Pause();
        }
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

    public void SarpNormalDeflect()
    {
        source.clip = sounds[Random.Range(17, 19)];
        source.PlayOneShot(source.clip);
    }

    public void SarpAttackStun()
    {
        source.clip = sounds[Random.Range(14, 16)];
        source.PlayOneShot(source.clip);
    }

    public void SarpHitEnemy()
    {
        source.clip = sounds[Random.Range(20, 27)];
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
