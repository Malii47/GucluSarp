using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    public GameObject settingsMenu;
    bool vibrationChecker;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        vibrationChecker = settingsMenu.GetComponent<SettingsMenu>().noVibration;
    }

    public void ButtonClickEnter()
    {
        source.clip = sounds[0];
        source.PlayOneShot(source.clip);
    }
    public void ButtonClickExit()
    {
        source.clip = sounds[1];
        source.PlayOneShot(source.clip);
    }

    public void VibrationToggle()
    {
        if (vibrationChecker)
            ButtonClickEnter();
        else
        {
            ButtonClickExit();
        }

    }
}
