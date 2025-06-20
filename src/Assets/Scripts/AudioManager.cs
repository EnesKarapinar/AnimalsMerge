using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    static bool isHaveMusic;
    public AudioClip musicSounds;
    public AudioClip[] sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Start()
    {
        if (!isHaveMusic)
        {
            isHaveMusic = true;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }


    public void PlaySfx(int n)
    {
        sfxSource.PlayOneShot(sfxSounds[n], PlayerPrefs.GetFloat("SfxVol"));
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}
