using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    Vector3 mousePos;
    Transform leftWall, rightWall;

    public Ball scriptBall;
    GameManager scriptManager;

    // Start is called before the first frame update
    void Start()
    {
        leftWall = GameObject.Find("Wall Left").GetComponent<Transform>();
        rightWall = GameObject.Find("Wall Right").GetComponent<Transform>();
        scriptManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!scriptManager.isPause && !scriptManager.isDead)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector2(Mathf.Clamp(mousePos.x, leftWall.position.x + scriptBall.width, rightWall.position.x - scriptBall.width), 1.75f);
            //transform.position = new Vector2(Mathf.Clamp(mousePos.x, leftWall.position.x + 100, rightWall.position.x - 100), 1.75f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        scriptBall = collision.gameObject.GetComponent<Ball>();
    }
}
