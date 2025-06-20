using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{

    GameManager scriptManager;
    AudioManager scriptAudioManager;
    AdMob scriptAdMob;

    private void Start()
    {
        scriptManager = gameObject.GetComponent<GameManager>();
        scriptAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        scriptAdMob = GetComponent<AdMob>();
    }

    public void Play()
    {
        scriptAudioManager.PlaySfx(0);
        scriptAdMob.ShowInterstitialAd("Game");
    }

    public void QuitGame()
    {
        scriptAudioManager.PlaySfx(0);
        Application.Quit();
    }

    public void Pause()
    {
        scriptAudioManager.PlaySfx(0);
        scriptManager.isPause = true;
        scriptManager.PnlPause.SetActive(true);
    }

    public void MainMenu()
    {
        scriptAudioManager.PlaySfx(0);
        scriptManager.GameOver();
        scriptAdMob.ShowInterstitialAd("Home");
    }

    public void Resume()
    {
        scriptAudioManager.PlaySfx(0);
        scriptManager.isPause = false;
        scriptManager.PnlPause.SetActive(false);
    }



}
