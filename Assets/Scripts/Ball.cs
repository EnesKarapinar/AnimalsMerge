using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject move;

    Spawner scriptSpawner;
    GameManager scriptManager;
    AudioManager scriptAudioManager;

    public bool isDrop;
    public int ID, IDCls;
    public float width, deathTimer = 0.5f, attentionTimer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        move = GameObject.Find("Move");
        scriptSpawner = GameObject.Find("GameManager").GetComponent<Spawner>();
        scriptManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ID = GetInstanceID();
        width = gameObject.transform.localScale.x;
        scriptAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDrop)
        {
            transform.position = new Vector2(move.transform.position.x, move.transform.position.y);
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            scriptSpawner.spawnObject = scriptSpawner.nextSpawnObject;
            if (Input.GetMouseButton(0))
            {
                move.GetComponent<LineRenderer>().enabled = true;
            }
            else
            {
                move.GetComponent<LineRenderer>().enabled = false;
            }
        }
        if (Input.GetMouseButtonUp(0) && !isDrop && !scriptManager.isPause && !scriptManager.isDead)
        {
            scriptAudioManager.PlaySfx(0);
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, -2f);
            isDrop = true;
            scriptSpawner.isSpawn = true;
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D cls)
    {
        if (!scriptManager.isPause)
        {
            scriptManager.Merge(cls.gameObject, this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDrop && !scriptManager.isPause && !scriptManager.isDead)
        {
            if (collision.gameObject.name.Equals("Wall Top"))
            {
                deathTimer -= Time.deltaTime;
                if (deathTimer <= 0)
                    scriptManager.GameOver();
            }
            else if (collision.gameObject.name.Equals("Wall Attention"))
            {
                attentionTimer -= Time.deltaTime;
                if (attentionTimer <= 0)
                    scriptManager.isAttention = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wall Top")) deathTimer = 1f;
        else if (collision.gameObject.name.Equals("Wall Attention")) scriptManager.isAttention = false; attentionTimer = 0.5f;
    }
}
