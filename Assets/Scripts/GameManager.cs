using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    class Ambiance
    {
        public Sprite[] imgAnimals = new Sprite[11];
        public Sprite imgBackground, imgTableCloth, imgBox, imgCycle, imgPauseBtn;
        public Material mergeMaterial;
        public AudioClip[] sfx = new AudioClip[3];
        public AudioClip music;
    }

    [SerializeField] List<Ambiance> ambiance;

    Spawner scriptSpawner;


    Vector2 mergePos;
    public AudioClip fxMerge, fxDrop;
    public GameObject goPoint, mergeEffect, PnlGameOver, PnlPause, pnlAttention;
    public Image imgNextSpawn;
    public Canvas canvas;
    public TextMeshProUGUI txtPoint, txtHighPoint, txtOverPoint, txtOverHightPoint, txtOverMoney;
    AudioManager scriptAudioManager;

    static int point = 0, money = 0;
    int temporaryPoint = 0, temporaryMoney = 0;
    public bool isPause = false, isDead = false, isAttention = false;



    [SerializeField] private Collider2D leftBoundary;  // Sol sýnýr
    [SerializeField] private Collider2D rightBoundary; // Sað sýnýr

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("Equipped", 1);
        Application.targetFrameRate = 61;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        scriptSpawner = GetComponent<Spawner>();
        txtPoint.text = point + "";
        txtHighPoint.text = PlayerPrefs.GetInt("topPoint") + "";
        scriptAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        money = PlayerPrefs.GetInt("money");
        WhichOneAmbience();

    }

    // Update is called once per frame
    void Update()
    {
        if (isAttention) pnlAttention.SetActive(true);
        else pnlAttention.SetActive(false);
    }

    public void Merge(GameObject cls, GameObject go)
    {
        if (cls.gameObject.CompareTag(go.tag))
        {
            if (go.GetComponent<Ball>().ID < cls.gameObject.GetComponent<Ball>().ID) { return; }
            GameObject effect = Instantiate(mergeEffect, go.transform.position, Quaternion.identity);
            effect.GetComponent<ParticleSystem>().startSize = (go.transform.localScale.x * 0.3f);
            AddPoint(int.Parse(go.tag), go);
            scriptAudioManager.PlaySfx(1);
            mergePos = new Vector2(((cls.gameObject.transform.position.x + go.transform.position.x) / 2), ((cls.gameObject.transform.position.y + go.transform.position.y) / 2));
            if (!go.tag.Equals("11"))
            {
                GameObject go2 = Instantiate(scriptSpawner.spawnObjects[int.Parse(cls.gameObject.tag)], mergePos, Quaternion.identity);

                // Çakýþmayý çöz
                ResolvePenetrationWithBoundaries(go2);

                if (!go2.GetComponent<Ball>().isDrop)
                    go2.GetComponent<Ball>().isDrop = true;
            }
            Destroy(effect, 1f);
            Destroy(cls);
            Destroy(go);
            if (PlayerPrefs.GetInt("isVibrate").Equals(1))
                Handheld.Vibrate();
        }

    }

    private void ResolvePenetrationWithBoundaries(GameObject obj)
    {
        Collider2D objCollider = obj.GetComponent<Collider2D>();

        if (objCollider == null)
        {
            Debug.LogWarning($"Collider2D bileþeni eksik: {obj.name}");
            return;
        }

        // Sol sýnýr ile çakýþma kontrolü
        if (Physics2D.Distance(objCollider, leftBoundary).isOverlapped)
        {
            var collisionInfo = Physics2D.Distance(objCollider, leftBoundary);
            Vector2 correction = collisionInfo.normal * collisionInfo.distance;
            obj.transform.position += (Vector3)correction; // Nesneyi dýþarý iter
        }

        // Sað sýnýr ile çakýþma kontrolü
        if (Physics2D.Distance(objCollider, rightBoundary).isOverlapped)
        {
            var collisionInfo = Physics2D.Distance(objCollider, rightBoundary);
            Vector2 correction = collisionInfo.normal * collisionInfo.distance;
            obj.transform.position += (Vector3)correction; // Nesneyi dýþarý iter
        }
    }



    public void AddPoint(int p, GameObject go)
    {
        GameObject go2 = Instantiate(goPoint, new Vector2(go.transform.position.x + 0.4f, go.transform.position.y + 0.4f), Quaternion.identity);
        switch (p)
        {
            case 1:
                point += 1;
                go2.GetComponent<TextMeshPro>().text = "+" + 1;
                break;
            case 2:
                point += 3;
                go2.GetComponent<TextMeshPro>().text = "+" + 3;
                break;
            case 3:
                point += 6;
                go2.GetComponent<TextMeshPro>().text = "+" + 6;
                break;
            case 4:
                point += 10;
                go2.GetComponent<TextMeshPro>().text = "+" + 10;
                AddMoney(1);
                break;
            case 5:
                point += 15;
                go2.GetComponent<TextMeshPro>().text = "+" + 15;
                AddMoney(1);
                break;
            case 6:
                point += 21;
                go2.GetComponent<TextMeshPro>().text = "+" + 21;
                AddMoney(2);
                break;
            case 7:
                point += 28;
                go2.GetComponent<TextMeshPro>().text = "+" + 28;
                AddMoney(3);
                break;
            case 8:
                point += 36;
                go2.GetComponent<TextMeshPro>().text = "+" + 36;
                AddMoney(3);
                break;
            case 9:
                point += 45;
                go2.GetComponent<TextMeshPro>().text = "+" + 45;
                AddMoney(4);
                break;
            case 10:
                point += 55;
                go2.GetComponent<TextMeshPro>().text = "+" + 55;
                AddMoney(5);
                break;
            case 11:
                point += 100;
                go2.GetComponent<TextMeshPro>().text = "+" + 100;
                AddMoney(100);
                break;
        }
        Destroy(go2, 0.5f);
        txtPoint.text = point + "";
    }

    public void AddMoney(int count)
    {
        money += count;
        temporaryMoney += count;
        PlayerPrefs.SetInt("money", money);
        money = PlayerPrefs.GetInt("money");
        PlayerPrefs.Save();
    }

    public void GameOver()
    {
        if (PlayerPrefs.GetInt("topPoint") < point)
        {
            PlayerPrefs.SetInt("topPoint", point);
        }
        scriptAudioManager.PlaySfx(2);
        PnlGameOver.SetActive(true);
        temporaryPoint = point;
        txtOverMoney.text = "+" + temporaryMoney;
        txtOverPoint.text = temporaryPoint + "";
        txtOverHightPoint.text = PlayerPrefs.GetInt("topPoint") + "";
        isDead = true;
        isAttention = false;
        point = 0;
        money = 0;
    }

    public void WhichOneAmbience()
    {
        switch (PlayerPrefs.GetInt("Equipped", 0))
        {
            case 0:
                PlacementAmbient(0);
                break;
            case 1:
                PlacementAmbient(1);
                break;
            case 2:
                PlacementAmbient(2);
                break;
            case 3:
                PlacementAmbient(3);
                break;
            default:
                PlacementAmbient(0);
                break;
        }
    }

    public void PlacementAmbient(int j)
    {
        for (int i = 0; i < ambiance[j].imgAnimals.Length; i++)
        {
            scriptSpawner.spawnObjects[i].gameObject.GetComponent<SpriteRenderer>().sprite = ambiance[j].imgAnimals[i];
        }
        //GameObject.Find("1Winter").GetComponent<SpriteRenderer>().sprite = ambiance[j].imgAnimals[0];
        GameObject.Find("1").GetComponent<SpriteRenderer>().sprite = ambiance[j].imgAnimals[0];
        GameObject.Find("Bg").GetComponent<SpriteRenderer>().sprite = ambiance[j].imgBackground;
        GameObject.Find("Table").GetComponent<SpriteRenderer>().sprite = ambiance[j].imgTableCloth;
        GameObject.Find("Ad").GetComponent<SpriteRenderer>().sprite = ambiance[j].imgTableCloth;
        GameObject.Find("Box").GetComponent<SpriteRenderer>().sprite = ambiance[j].imgBox;
        GameObject.Find("ImgCycle").GetComponent<Image>().sprite = ambiance[j].imgCycle;
        GameObject.Find("BtnPause").GetComponent<Image>().sprite = ambiance[j].imgPauseBtn;
        for (int i = 0; i < ambiance[j].sfx.Length; i++)
        {
            scriptAudioManager.sfxSounds[i] = ambiance[j].sfx[i];
        }
        scriptAudioManager.musicSource.clip = ambiance[j].music;
        scriptAudioManager.musicSource.Play();
        mergeEffect.GetComponent<ParticleSystemRenderer>().material = ambiance[j].mergeMaterial;
    }
}
