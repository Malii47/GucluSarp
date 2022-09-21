using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer soundMixer;
    public AudioMixer audioMixer;

    public void SetSound(float volume)
    {
        soundMixer.SetFloat("volume", volume);
    }

    public void SetAudio(float audio)
    {
        audioMixer.SetFloat("audio", audio);
    }
}
