using TMPro;
using UnityEngine;

public class GameIntemAd : MonoBehaviour
{

    [SerializeField] private float timer = 0f; // Zamanlayýcý için deðiþken
    [SerializeField] private float adInterval = 20f; // Dakikada bir reklam göstermek için 60 saniye

    [SerializeField] AdMob scriptAdMob;
    [SerializeField] GameManager scriptManager;

    [SerializeField] GameObject timerPnl;
    [SerializeField] TextMeshProUGUI timerTxt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scriptAdMob = GetComponent<AdMob>();
        scriptManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!scriptManager.isDead && !scriptManager.isPause && PlayerPrefs.GetInt("adMax", 1) == 1)
        {
            timer += Time.deltaTime;
            if (adInterval - timer <= 11)
            {
                timerPnl.SetActive(true);
                timerTxt.text = ((int)(adInterval - timer)).ToString();
            }
            else
            {
                timerPnl.SetActive(false);
            }
            if (timer >= adInterval) // 60 saniye dolunca
            {
                scriptAdMob.ShowBetweenAd();
                timer = 0f; // Zamanlayýcýyý sýfýrla
            }
        }
    }
}
