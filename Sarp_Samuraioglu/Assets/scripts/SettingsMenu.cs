using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer soundMixer;
    public AudioMixer audioMixer;
    public float soundVolumeValue;
    public float audioVolumeValue;
    public Slider soundVolumeSlider;
    public Slider audioVolumeSlider;
    [SerializeField] Image vibrationOnIcon;
    [SerializeField] Image vibrationOffIcon;
    private bool noVibration = false;
    void Start()
    {
        //soundVolumeSlider.value = PlayerPrefs.GetFloat("volume");
        //audioVolumeSlider.value = PlayerPrefs.GetFloat("audio");
        if (!PlayerPrefs.HasKey("volume"))
        {
            soundVolumeSlider.value = 1;
        }
        else
        {
            soundVolumeSlider.value = PlayerPrefs.GetFloat("volume");
        }

        if (!PlayerPrefs.HasKey("audio"))
        {
            audioVolumeSlider.value = 1;
        }

        else
        {
            audioVolumeSlider.value = PlayerPrefs.GetFloat("audio");
        }

        if (!PlayerPrefs.HasKey("noVibration"))
        {
            PlayerPrefs.SetInt("noVibration", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
    }
    void OnDisable()
    {
        PlayerPrefs.SetFloat("volume", soundVolumeValue);
        PlayerPrefs.SetFloat("audio", audioVolumeValue);
        PlayerPrefs.Save();
    }

    public void SetSound(float volume)
    {
        soundVolumeValue = volume;
        soundMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetAudio(float audio)
    {
        audioVolumeValue = audio;
        audioMixer.SetFloat("audio", audio);
    }

    public void OnButtonPress()
    {
        if (noVibration == false)
        {
            noVibration = true;
        }
        else
        {
            noVibration = false;
        }        

        Save();
        UpdateButtonIcon();
    }

    private void Load()
    {
        noVibration = PlayerPrefs.GetInt("noVibration") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("noVibration", noVibration ? 1 : 0);
    }

    private void UpdateButtonIcon()
    {
        if(noVibration== false)
        {
            vibrationOnIcon.enabled = true;
            vibrationOffIcon.enabled = false;
        }
        else
        {
            vibrationOnIcon.enabled = false;
            vibrationOffIcon.enabled = true;
        }
    }
}
