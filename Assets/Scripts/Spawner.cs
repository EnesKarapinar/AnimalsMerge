using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    public GameObject[] spawnObjects;

    GameManager scriptManager;


    public bool isSpawn = false;
    float spawnTimer = 1f;
    public int spawnObject, nextSpawnObject, generateTimer;

    // Start is called before the first frame update
    void Start()
    {
        scriptManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawn && !scriptManager.isPause && !scriptManager.isDead)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                Instantiate(spawnObjects[spawnObject]);
                spawnTimer = 1f;
                isSpawn = false;
                generateTimer = 0;
            }
        }
        else
        {
            if (generateTimer <= 0)
            {
                nextSpawnObject = Random.Range(0, 4);
                scriptManager.imgNextSpawn.sprite = spawnObjects[nextSpawnObject].gameObject.GetComponent<SpriteRenderer>().sprite;
                spawnObject = nextSpawnObject;
                generateTimer = 1;
            }
        }
    }
}
