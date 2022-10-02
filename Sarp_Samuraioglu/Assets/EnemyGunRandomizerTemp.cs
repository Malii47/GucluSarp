using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunRandomizerTemp : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();

    }

    public void SarpKillsGunEnemy()
    {
        source.clip = sounds[Random.Range(0, 5)];
        source.PlayOneShot(source.clip);
    }

}
