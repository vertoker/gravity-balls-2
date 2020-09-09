using System.Collections;
using UnityEngine;

/// Общий скрипт телепорта
public class Teleport : GlobalFunctions
{
    public Teleport teleportDestination;
    public Animator anim;
    public GameObject act;
    public SpriteRenderer sr;
    public AudioClip setClip;
    public bool active = true;
    private bool isMake = true;
    private bool graphics;
    private bool graphicsLow;

    private AudioBase audioBase;
    private Transform trTD;
    private Transform trp;
    private TrailRenderer tr;

    public void Awake()
    {
        trTD = teleportDestination.transform;
        GameObject cam = GameObject.FindWithTag("MainCamera");
        trp = cam.GetComponent<Management>().player.transform;
        audioBase = cam.GetComponent<AudioBase>();
        tr = trp.GetComponent<TrailRenderer>();
        string graphicsquality = PlayerPrefs.GetString("graphicsquality");
        graphicsLow = graphicsquality == "low";
        graphics = graphicsquality == "high";
        act.SetActive(graphics);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isMake == true && active == true)
        {
            teleportDestination.IsMakeTeleporting();
            IsMakeTeleporting();
            audioBase.SetSound(setClip, 3, 0.2f, TypePlaying.Sound, false);
            trp.position = trTD.position;
            if (graphicsLow) { return; }
            tr.enabled = false;
            StartCoroutine(TrailRP());
        }
    }

    public void IsMakeTeleporting()
    {
        isMake = false;
        anim.enabled = graphics;
        StartCoroutine(IsMakeTrue());
        return;
    }

    private IEnumerator TrailRP()
    {
        yield return new WaitForSeconds(0.25f);
        tr.enabled = true;
    }

    private IEnumerator IsMakeTrue()
    {
        yield return new WaitForSeconds(0.5f);
        anim.enabled = false;
        isMake = true;
    }
}