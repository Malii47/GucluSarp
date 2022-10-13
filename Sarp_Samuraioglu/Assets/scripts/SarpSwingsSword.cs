using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarpSwingsSword : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();

    }

    public void SarpSwordSwinging()
    {
        source.clip = sounds[Random.Range(0, 1)];
        source.PlayOneShot(source.clip);
    }

    public void SarpDeflectSwinging()
    {
        source.clip = sounds[Random.Range(2, 3)];
        source.PlayOneShot(source.clip);
    }

    public void SarpDash()
    {
        source.clip = sounds[Random.Range(4, 4)];
        source.PlayOneShot(source.clip);
    }

    public void SarpDeath()
    {
        source.clip = sounds[Random.Range(5, 6)];
        source.PlayOneShot(source.clip);
    }

    public void SarpBulletDeflect()
    {
        source.clip = sounds[Random.Range(7, 9)];
        source.PlayOneShot(source.clip);
    }
}
