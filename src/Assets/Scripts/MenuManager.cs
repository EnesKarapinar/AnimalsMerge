using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public Toggle tglVibrate;
    public Slider sldMusicVol, sldSfxVol;
    public TextMeshProUGUI txtMoney;

    public GameObject pnlSettings, pnlMainMenu, pnlShop;

    AudioManager scriptAudioManager;


    // Start is called before the first frame update
    void Start()
    {
        scriptAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        scriptAudioManager.MusicVolume(PlayerPrefs.GetFloat("MusicVol", 0.060f));
        scriptAudioManager.SfxVolume(PlayerPrefs.GetFloat("SfxVol", 0.15f));
        PlayerPrefs.GetInt("isVibrate", 1);
        //scriptAudioManager.MusicVolume();
        UpdateMoney();
        Debug.LogWarning("Music Volume" + PlayerPrefs.GetFloat("MusicVol"));
    }

    public void Settings()
    {
        pnlMainMenu.SetActive(false);
        pnlSettings.SetActive(true);
        scriptAudioManager.PlaySfx(0);
    }

    public void BackToMainMenu()
    {
        pnlSettings.SetActive(false);
        pnlMainMenu.SetActive(true);
        pnlShop.SetActive(false);
        scriptAudioManager.PlaySfx(0);
    }

    public void Shop()
    {
        pnlMainMenu.SetActive(false);
        pnlShop.SetActive(true);
        scriptAudioManager.PlaySfx(0);
    }

    public void UpdateMoney()
    {
        txtMoney.text = PlayerPrefs.GetInt("money") + "";
    }
}
