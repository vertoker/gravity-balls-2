using System.Collections;
using UnityEngine;

/// Общий скрипт для бомбы
public class Explosion : GlobalFunctions
{
    public float power = 1f;
    public float radius = 5f;
    public float health = 20f;
    public float timeOffsetExplosion = 1f;
    public GameObject[] contacts = new GameObject[0];
    public Animator expAnim;
    public bool writeContacs = true;
    public AudioClip setClip;

    private float timeOffsetExplosionCount;
    private float alphaTimer;
    private bool isTimerOn = false;
    private bool firstAPEvirtual = true;
    private Collider2D cl;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioBase audioBase;
    private Explosion explosion;

    private void Awake()
    {
        audioBase = GameObject.FindWithTag("MainCamera").GetComponent<AudioBase>();
        cl = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        explosion = GetComponent<Explosion>();
    }

    private void Start()
    {
        alphaTimer = sr.color.a;
        StartCoroutineTimerOffsetExplosion();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (writeContacs == true)
        {
            int cont = contacts.Length;
            GameObject[] n = new GameObject[cont + 1];

            if (cont != 0)
            {
                for (int i = 0; i < cont; i++)
                {
                    n[i] = contacts[i];
                }
            }

            n[cont] = collision.gameObject;
            contacts = n;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (writeContacs == true)
        {
            int cont = contacts.Length;
            if (cont != 1)
            {
                int counter = 0;
                GameObject[] n = new GameObject[cont - 1];

                for (int i = 0; i < cont; i++)
                {
                    if (contacts[i] != collision.gameObject)
                    {
                        n[counter] = contacts[i];
                        counter++;
                    }
                }

                contacts = n;
            }
            else
            {
                contacts = new GameObject[0];
            }
        }
    }

    public void ActionExplosionEmulation(GameObject obj)
    {
        float damage = 0f;
        if (obj.CompareTag("Laser"))
        {
            damage = obj.GetComponent<Damage>().senDamage;
        }
        else
        {
            damage = obj.GetComponent<Power>().power;
        }

        health = health - damage;
        StartCoroutineTimerOffsetExplosion();
        return;
    }

    public void StartCoroutineTimerOffsetExplosion()
    {
        if (health <= 0f && isTimerOn == false)
        {
            isTimerOn = true;
            timeOffsetExplosionCount = timeOffsetExplosion;
            StartCoroutine(TimerOffsetExplosion(0.1f));
        }
    }

    private IEnumerator TimerOffsetExplosion(float timeTick)
    {
        yield return new WaitForSeconds(timeTick);
        timeOffsetExplosionCount = timeOffsetExplosionCount - timeTick;
        if (timeOffsetExplosionCount > 0f)
        {
            float c = timeOffsetExplosionCount / timeOffsetExplosion;
            sr.color = new Color(1f, c, c, alphaTimer);
            StartCoroutine(TimerOffsetExplosion(timeTick));
        }
        else
        {
            ExplosionAction();
        }
    }

    private void ExplosionAction()
    {
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        audioBase.SetSound(setClip, 2, 1f, TypePlaying.Sound, false);
        Destroy(cl);
        CircleCollider2D c = gameObject.AddComponent<CircleCollider2D>();
        c.isTrigger = true;
        c.radius = radius;
        tag = "Explosion";
        if (PlayerPrefs.GetString("graphicsquality") != "low")
        {
            expAnim.enabled = true;
            StartCoroutine(Off2High());
        }
        else
        {
            Destroy(sr);
            StartCoroutine(Off());
        }
    }

    public IEnumerator Off()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        gameObject.SetActive(false);
    }

    public IEnumerator OffHigh(CircleCollider2D c)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        c.enabled = false;
    }

    public IEnumerator Off2High()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        gameObject.SetActive(false);
    }

    public void APEvirtual()
    {
        int cont = contacts.Length;
        if (cont != 0 && firstAPEvirtual == true)
        {
            firstAPEvirtual = false;
            for (int i = 0; i < cont; i++)
            {
                if (contacts[i] != null)
                {
                    if (contacts[i].GetComponent<PhysicsEmulation>())
                    {
                        contacts[i].GetComponent<PhysicsEmulation>().ExplosionPhysicsEmulation(explosion);
                    }
                }
            }
        }
    }

    public void AnimFull()
    {
        sr.color = new Color(1f, 1f, 1f, 1f);
        sr.size = new Vector2(3f * radius, 3f * radius);
        return;
    }
}