using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;

    private const string MASTER_VOLUME = "MasterVolume";
    private const string MUSIC_VOLUME = "MusicVolume";
    private const string SFX_VOLUME = "SFXVolume";
    
    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstLaunch") != 1)
        {
            audioMixer.SetFloat(MASTER_VOLUME, 0);
            audioMixer.SetFloat(MUSIC_VOLUME, -50);
            audioMixer.SetFloat(SFX_VOLUME, -30);
            musicSlider.value = -50;
            masterSlider.value = 0;
            sfxSlider.value = -30;
            PlayerPrefs.SetInt("FirstLaunch",1);
        }
        else
        {
            //Check playerprefs
            audioMixer.SetFloat(MASTER_VOLUME, PlayerPrefs.GetFloat("MasterVolume"));
            audioMixer.SetFloat(MUSIC_VOLUME, PlayerPrefs.GetFloat("MusicVolume"));
            audioMixer.SetFloat(SFX_VOLUME, PlayerPrefs.GetFloat("SFXVolume"));

            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat(MASTER_VOLUME, value);
        PlayerPrefs.SetFloat("MasterVolume",value);
    }
    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(MUSIC_VOLUME, value);
        PlayerPrefs.SetFloat("MusicVolume",value);
    }
    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat(SFX_VOLUME, value);
        PlayerPrefs.SetFloat("SFXVolume",value);
    }
}