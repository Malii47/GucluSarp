using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSounds : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioClip[] sounds;
    public AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = sounds[0];
        source.PlayOneShot(source.clip);
    }

    void Update()
    {
        if (pauseMenu.GetComponent<PauseMenu>().gameIsPaused == false)
        {
            source.Play();
            
        }
        else
        {
            source.Pause();
        }
    }
}
