using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotSounds : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioSource source;
    public GameObject pauseMenu;
    void Start()
    {
        source = GetComponent<AudioSource>();
        pauseMenu = GameObject.Find("PauseMenuManager");
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

    public void EnemyGunShot()
    {
        source.clip = sounds[Random.Range(0, 1)];
        source.PlayOneShot(source.clip);
    }
}
