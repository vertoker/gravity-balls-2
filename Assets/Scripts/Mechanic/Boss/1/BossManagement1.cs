using UnityEngine;
using System.Collections;

/// Менеджмент 1 босса
public class BossManagement1 : GlobalFunctions
{
    public float hp = 100f;
    public float speed = 0.2f;
    public bool startActivated = false;
    public bool activated = false;
    public bool activatedSaw = false;
    public bool activatedAngle = false;
    public bool activatedCoroutine = true;
    private bool active;
    private float maxhp;
    public Vector2 target;
    public Vector2 targetSaw1;
    public Vector2 targetSaw2;
    public Vector2 minBorder;
    public Vector2 maxBorder;
    public DeadBoss1 deadBoss;
    public GameObject backGround;
    public GameObject healthBar;
    public Transform tr;
    public Transform sawMain;
    public Transform saw1;
    public Transform saw2;
    public Arrow arrow;
    public AudioSet setStart;
    public AudioSet setEnd;
    public Transform player;
    public Power playerPower;
    private Transform bg, hb;
    private float targethp = 0f;
    private Vector2 startMove = new Vector2(-20f, 0f);

    public void Awake()
    {
        maxhp = hp;
        bg = backGround.transform;
        hb = healthBar.transform;
    }

    public void Start()
    {
        Dead(PlayerPrefs.GetString("boss1") == "life");
    }

    public void FixedUpdate()
    {
        if (startActivated && !activatedCoroutine)
        {
            if ((Vector2)tr.position != startMove)
            {
                tr.position = Vector2.MoveTowards(tr.position, startMove, speed);
                saw1.position = Vector2.MoveTowards(saw1.position, startMove, speed);
                saw2.position = Vector2.MoveTowards(saw2.position, startMove, speed);
            }
            else
            {
                activatedCoroutine = true;
                startActivated = false;
                StartCoroutine(ActivatedOn());
            }
        }
        if (activated)/// Передвижение босса
        {
            if ((Vector2)tr.position != target)
            {
                tr.position = Vector2.MoveTowards(tr.position, target, speed);
            }
            else
            {
                activated = false;
                sawMain.localScale = new Vector2(0f, 0f);
                StartCoroutine(TargetRotate());
            }
        }
        if (activatedSaw)/// Движение пил
        {
            if ((Vector2)saw1.position != targetSaw1)
            {
                saw1.position = Vector2.MoveTowards(saw1.position, targetSaw1, speed);
            }
            else
            {
                float x = Random.Range(minBorder.x, maxBorder.x);
                float y = Random.Range(minBorder.y, maxBorder.y);
                targetSaw1 = new Vector2(x, y);
            }

            if ((Vector2)saw2.position != targetSaw2)
            {
                saw2.position = Vector2.MoveTowards(saw2.position, targetSaw2, speed);
            }
            else
            {
                float x = Random.Range(minBorder.x, maxBorder.x);
                float y = Random.Range(minBorder.y, maxBorder.y);
                targetSaw2 = new Vector2(x, y);
            }
        }
        if (activatedAngle)///
        {
            Vector2 dir = player.position - tr.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            tr.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(tr.localEulerAngles.z, angle, 0.1f));
        }
    }

    /// Вращение босса
    public IEnumerator TargetRotate()
    {
        yield return new WaitForSeconds(3f + 3f * hp / maxhp);
        sawMain.localScale = new Vector2(6f, 6f);
        float x = Random.Range(minBorder.x, maxBorder.x);
        float y = Random.Range(minBorder.y, maxBorder.y);
        target = new Vector2(x, y);
        activated = true;
    }

    /// Активация босса
    public IEnumerator ActivatedOn()
    {
        yield return new WaitForSeconds(3f);
        sawMain.localScale = new Vector2(6f, 6f);
        target = new Vector2(Random.Range(minBorder.x, maxBorder.x), Random.Range(minBorder.y, maxBorder.y));
        targetSaw1 = new Vector2(Random.Range(minBorder.x, maxBorder.x), Random.Range(minBorder.y, maxBorder.y));
        targetSaw2 = new Vector2(Random.Range(minBorder.x, maxBorder.x), Random.Range(minBorder.y, maxBorder.y));
        activatedSaw = true;
        activated = true;
        arrow.isActive = true;
    }

    /// 
    public IEnumerator ActivatedCoroutineOff()
    {
        yield return new WaitForSeconds(1f);
        activatedCoroutine = false;
        activatedAngle = true;
    }

    /// Реакция HealthBar на удары игрока
    public void Update()
    {
        if (active == true)
        {
            if (hp != targethp)
            {
                float s = Time.fixedDeltaTime / 0.03f * (Time.deltaTime / 0.03f);
                hp = MoveToward(hp, targethp, speed * s, new Vector2(-0f, maxhp));
            }
            else
            {
                active = false;
                if (targethp == 0f)
                {
                    Dead(true);
                }
            }
        }

        UpdateHP();
    }

    /// Обновление HealthBar
    public void UpdateHP()
    {
        float h = hp / maxhp;
        bg.localScale = new Vector3(5f, 0.9f, 1f);
        hb.localScale = new Vector3(4.8f * h, 0.7f, 1f);
        hb.localPosition = new Vector3(-2.4f + 4.8f * h / 2f, 0f, 0f);
    }

    /// Нанесение урона
    private bool oneTimeMusic = true;
    public void Damage(float damage)
    {
        if (oneTimeMusic == true)/// Первый удар по боссу
        {
            oneTimeMusic = false;
            deadBoss.StartBoss();
            deadBoss.Boom();
            setStart.SetMusic();
            startActivated = true;
            StartCoroutine(ActivatedCoroutineOff());
        }

        if (hp != 0f)/// Увеличение скорости босса
        {
            targethp = Stable2(hp - damage, 0f, maxhp);
            speed += damage * 0.02f;
            active = true;
        }
    }

    /// Смерть
    public void Dead(bool boom)
    {
        active = false;
        activated = false;
        activatedSaw = false;
        startActivated = false;
        activatedAngle = false;
        activatedCoroutine = false;
        backGround.SetActive(false);
        healthBar.SetActive(false);
        sawMain.gameObject.SetActive(false);
        saw1.gameObject.SetActive(false);
        saw2.gameObject.SetActive(false);
        setEnd.SetMusic();
        arrow.obj.SetActive(false);
        PlayerPrefs.SetString("boss1", "death");
        deadBoss.Dead(tr.position, boom);
    }

    /// Получение урона
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Damage(playerPower.power);
        }
    }
}
