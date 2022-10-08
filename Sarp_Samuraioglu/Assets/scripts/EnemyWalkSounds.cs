using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkSounds : MonoBehaviour
{
    public LayerMask whoIsWalking;
    public AudioClip[] sounds;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void EnemyStep1()
    {
        source.clip = sounds[Random.Range(0, 0)];
        source.PlayOneShot(source.clip);
    }

    public void EnemyStep2()
    {
        source.clip = sounds[Random.Range(1, 1)];
        source.PlayOneShot(source.clip);
    }
}
