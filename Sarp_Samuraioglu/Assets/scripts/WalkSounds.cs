using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSounds : MonoBehaviour
{
    public LayerMask playerLayer;
    public LayerMask playerLayer2;
    public Transform roadPoint;
    public Transform grassPoint;
    public Vector2 size;
    public AudioClip[] sounds;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Step1()
    {
            Collider2D[] roads = Physics2D.OverlapBoxAll(roadPoint.position, size, 0f, playerLayer);

            foreach (Collider2D road in roads)
            {
                source.clip = sounds[Random.Range(0, 0)];
                source.PlayOneShot(source.clip);
            }

            Collider2D[] roads2 = Physics2D.OverlapBoxAll(grassPoint.position, size, 0f, playerLayer2);

            foreach (Collider2D road2 in roads2)
            {
                source.clip = sounds[Random.Range(2, 2)];
                source.PlayOneShot(source.clip);
            }

    }
    public void Step2()
    {

            Collider2D[] roads = Physics2D.OverlapBoxAll(roadPoint.position, size, 0f, playerLayer);

            foreach (Collider2D road in roads)
            {
                source.clip = sounds[Random.Range(1, 1)];
                source.PlayOneShot(source.clip);
            }

            Collider2D[] roads2 = Physics2D.OverlapBoxAll(grassPoint.position, size, 0f, playerLayer2);

            foreach (Collider2D road2 in roads2)
            {   
                source.clip = sounds[Random.Range(3, 3)];
                source.PlayOneShot(source.clip);
            }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(roadPoint.position, size);
        //Gizmos.DrawCube(grassPoint.position, size);
    }
}
