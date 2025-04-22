using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    AudioManager scriptAudioManager;

    public Slider sldMusicVol, sldSfxVol;
    public Toggle tglVibrate;


    // Start is called before the first frame update
    void Start()
    {
        scriptAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        sldMusicVol.value = PlayerPrefs.GetFloat("MusicVol", 0.060f);
        sldSfxVol.value = PlayerPrefs.GetFloat("SfxVol", 0.15f);
        if (PlayerPrefs.GetInt("isVibrate", 1) == 1) Vibrate();
    }

    public void MusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVol", sldMusicVol.value);
        scriptAudioManager.musicSource.volume = PlayerPrefs.GetFloat("MusicVol");
        scriptAudioManager.MusicVolume(PlayerPrefs.GetFloat("MusicVol"));
    }

    public void SfxVolume()
    {
        PlayerPrefs.SetFloat("SfxVol", sldSfxVol.value);
        scriptAudioManager.sfxSource.volume = PlayerPrefs.GetFloat("SfxVol");
        scriptAudioManager.MusicVolume(PlayerPrefs.GetFloat("MusicVol"));
    }

    public void Vibrate()
    {
        if (tglVibrate.isOn)
        {
            PlayerPrefs.SetInt("isVibrate", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isVibrate", 0);
        }
    }


}
