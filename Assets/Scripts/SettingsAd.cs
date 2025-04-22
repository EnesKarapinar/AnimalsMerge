using UnityEngine;
using UnityEngine.UI;

public class SettingsAd : MonoBehaviour
{
    public Toggle tglAdMax;
    public Toggle tglAdMin;

    private void Awake()
    {
        // Toggle'larý ayarla
        if (PlayerPrefs.GetInt("adMax", 1) == 1)
        {
            tglAdMax.isOn = true;
            tglAdMin.isOn = false;
        }
        else
        {
            tglAdMax.isOn = false;
            tglAdMin.isOn = true;
        }

        // Toggle deðiþikliklerini dinle
        tglAdMax.onValueChanged.AddListener(delegate { OnMaxToggleChanged(); });
        tglAdMin.onValueChanged.AddListener(delegate { OnMinToggleChanged(); });
    }

    private void OnMaxToggleChanged()
    {
        if (tglAdMax.isOn)
        {
            tglAdMin.isOn = false; // Min toggle'ý devre dýþý býrak
            PlayerPrefs.SetInt("adMax", 1); // adMax deðerini kaydet
            PlayerPrefs.Save();
        }
    }

    private void OnMinToggleChanged()
    {
        if (tglAdMin.isOn)
        {
            tglAdMax.isOn = false; // Max toggle'ý devre dýþý býrak
            PlayerPrefs.SetInt("adMax", 0); // adMax deðerini kaydet
            PlayerPrefs.Save();
        }
    }
}
