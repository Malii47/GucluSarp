using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunRandomizerTemp : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    public GameObject pauseMenu;
    void Start()
    {
        source = GetComponent<AudioSource>();
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

    public void SarpKillsGunEnemy()
    {
        source.clip = sounds[Random.Range(0, 5)];
        source.PlayOneShot(source.clip);
    }
}
