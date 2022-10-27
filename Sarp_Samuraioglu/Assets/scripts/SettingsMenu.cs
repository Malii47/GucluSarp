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
            soundMixer.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("volume", 1) * 20));
            
        }
        else
        {
            LoadSound();
        }

        if (!PlayerPrefs.HasKey("audio"))
        {
            audioMixer.SetFloat("audio", Mathf.Log10(PlayerPrefs.GetFloat("audio", 1) * 20));
            
        }

        else
        {
            LoadAudio();
        }

        //soundMixer.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("volume", 1) * 20));
        //audioMixer.SetFloat("audio", Mathf.Log10(PlayerPrefs.GetFloat("audio", 1) * 20));

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
    void Update()
    {
        //PlayerPrefs.SetFloat("volume", soundVolumeValue);
        //PlayerPrefs.SetFloat("audio", audioVolumeValue);
        //soundVolumeValue = Mathf.Log10(PlayerPrefs.GetFloat("volume", 1) * 20);

        //PlayerPrefs.Save();
    }

    public void SetSound(float volume)
    {
        
        soundMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        soundVolumeValue = volume;
        PlayerPrefs.Save();
    }

    public void SetAudio(float audio)
    {
        
        audioMixer.SetFloat("audio", audio);
        audioVolumeValue = audio;
        PlayerPrefs.Save();
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
    private void LoadSound()
    {
        //soundVolumeSlider.value = PlayerPrefs.GetFloat("volume", soundVolumeValue);
        soundMixer.SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("volume", soundVolumeValue)));
    }
    private void LoadAudio()
    {
        audioMixer.SetFloat("audio", Mathf.Log10(PlayerPrefs.GetFloat("audio", audioVolumeValue)));
    }

    private void Save()
    {
        PlayerPrefs.SetInt("noVibration", noVibration ? 1 : 0);
    }
    private void SaveSound()
    {
        
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
