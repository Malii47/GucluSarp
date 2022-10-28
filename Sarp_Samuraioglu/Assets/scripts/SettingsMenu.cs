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
    public bool noVibration = false;
    void Start()
    {
        soundVolumeSlider.value = PlayerPrefs.GetFloat("volumeValue", 0.75f);
        audioVolumeSlider.value = PlayerPrefs.GetFloat("audioValue", 0.75f);

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

    public void SetSound(float volume)
    {
        soundMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volumeValue", volume);
    }

    public void SetAudio(float audio)
    {
        
        audioMixer.SetFloat("audio", Mathf.Log10(audio)*20);
        PlayerPrefs.SetFloat("audioValue", audio);
    }

    public void OnButtonPress()
    {
        if (noVibration == false)
            noVibration = true;

        else
            noVibration = false;

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
        if (noVibration == false)
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
