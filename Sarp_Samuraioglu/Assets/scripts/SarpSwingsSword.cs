using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarpSwingsSword : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    public float enemyHealth;
    public bool oneTimeExecution = true;
    void Start()
    {
        source = GetComponent<AudioSource>();

    }

    public void CallIEnumerator()
    {
        StartCoroutine(SarpSwordSwinging());
    }

    public IEnumerator SarpSwordSwinging()
    {
        source.clip = sounds[Random.Range(0, 1)];
        yield return new WaitForSeconds(.2f);
        source.PlayOneShot(source.clip);
        oneTimeExecution = false;
    }

    public void SarpDeflectSwinging()
    {
        source.clip = sounds[Random.Range(2, 3)];
        source.PlayOneShot(source.clip);
        oneTimeExecution = false;
    }


}
