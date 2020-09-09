using System.Collections;
using UnityEngine;

/// Скрипт лифта (начало и конец игры)
public class Elevator : GlobalFunctions
{
    public Vector2 endPos;
    public Vector2 startPos;
    public int nextScene = 1;
    public int nextElevatorSave = 0;
    public float speed = 0.1f;

    public bool isFirst = true;
    public bool isActive = true;
    public bool isReverse = false;
    public bool isMake = false;
    private GameObject player;
    private Rigidbody2D rb;
    private Transform tr;
    private Transform trp;
    private GameUI gameui;
    private AudioBase audioBase;
    private Transform cam;

    private void Start()
    {
        audioBase = GameObject.FindWithTag("MainCamera").GetComponent<AudioBase>();
        gameui = GameObject.FindWithTag("Canvas").GetComponent<GameUI>();
        player = gameui.m.player;
        rb = player.GetComponent<Rigidbody2D>();
        trp = player.GetComponent<Transform>();
        tr = GetComponent<Transform>();
        cam = gameui.m.transform;

        startPos = tr.position;
        if (isFirst)
        {
            trp.position = startPos;
            rb.velocity = new Vector2();
            rb.gravityScale = 0f;
            gameui.m.Set();
        }
        else
        {
            tr.position = endPos;
            isMake = true;
        }
        isActive = isFirst;
        isReverse = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isMake == true)
        {
            isReverse = true;
            isActive = true;
            rb.velocity = new Vector2();
            rb.gravityScale = 0f;
            gameui.block.gameObject.SetActive(true); 
            PlayerPrefs.SetInt("elevatorsave", nextElevatorSave);
            gameui.animatorBlackScreenGame.SetBool("isActive", true);
            audioBase.LowerSound(0.05f, 16, 0, TypePlaying.Music);
            StartCoroutine(NumSaveRotate());
            StartCoroutine(gameui.StartGame(1.5f, nextScene));
        }
    }

    private IEnumerator NumSaveRotate()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetFloat("rotatenextlevel", Stable(cam.localEulerAngles.z, -180f, 180f));
    }

    private void FixedUpdate()
    {
        if (isActive == true)
        {
            float s = Time.fixedDeltaTime / 0.03f;
            if (isReverse == false)
            {
                rb.velocity = new Vector2();
                tr.position = Vector2.MoveTowards(tr.position, endPos, speed * s);
                trp.position = tr.position;
                if ((Vector2)tr.position == endPos)
                {
                    isMake = true;
                    isActive = false;
                    rb.gravityScale = 1f;
                    gameui.block.gameObject.SetActive(false);
                }
            }
            else if (isReverse == true)
            {
                tr.position = Vector2.MoveTowards(tr.position, startPos, speed * s);
                trp.position = tr.position;
                if (tr.position == (Vector3)startPos)
                {
                    isActive = false;
                    rb.gravityScale = 1f;
                }
            }
        }
    }
}
