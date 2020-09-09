using System.Collections;
using UnityEngine;

/// Менеджмент 2 босса
public class BossManagement2 : GlobalFunctions
{
    public float hp = 100f;
    public float speed = 0.5f;
    public float speedRotate = 0.5f;
    public int stage = 1;
    public bool isAlive = true;
    public bool isActivated = false;
    public bool isMove = false;
    public bool isWorkingLaser = true;
    private float timeStamina = 0f;
    private float timeRetarget = 0f;
    public Vector2 region = Vector2.zero;
    public Vector3 target = Vector3.zero;
    
    public GameObject player;
    public Transform saw;
    public Transform laser1;
    public Transform laser2;
    public Laser laserL1;
    public Laser laserL2;
    public Transform laserOffset1;
    public Transform laserOffset2;
    public Explosion explosion;
    public GameObject explosionAsset;
    public CircleCollider2D trigStart;
    public BoxCollider2D laserDetected1;
    public BoxCollider2D laserDetected2;
    public GameObject saw1;
    public GameObject saw2;
    public Transform health;
    public Transform stamina;
    public SpriteRenderer srStamina;
    private Transform pl;
    private Transform tr;

    public Transform state;
    public Laser state1;
    public Laser state2;
    public Laser state3;
    public Laser state4;
    private Coroutine coroutineStamina;

    public SpriteRenderer bossBase;
    public SpriteRenderer laserD1;
    public SpriteRenderer laserD2;
    public Gate gateStart;
    public Gate gateEnd;
    public GameObject blockWin;
    public GameObject physicsIn;
    public GameObject stateLasers;
    public GameObject expStart;
    
    public AudioSet setStart;
    public AudioClip setEnd;
    public AudioBase audioBase;

    public void Awake()
    {
        bool isDeath = PlayerPrefs.GetString("boss2") == "death";
        blockWin.SetActive(false);
        if (isDeath)
        {
            isAlive = false;
            gateStart.isReverse = true;
            gateEnd.isReverse = true;
            physicsIn.SetActive(false);
            stateLasers.SetActive(false);
            expStart.SetActive(false);
            gameObject.SetActive(false);
        }
        else
        {
            tr = transform;
            pl = player.transform;
            timeStamina = 5.4f / speedRotate / 100f;
            timeRetarget = 5.4f / speedRotate;
            saw.localScale = Vector3.zero;
            stamina.localScale = Vector3.zero;
            srStamina.color = new Color(0f, 0.5f, 1f, 0f);
            saw1.SetActive(false);
            saw2.SetActive(false);
            LaserDisable();
            LaserBlockEnable();
        }
    }

    public void Update()
    {
        if (isAlive)
        {
            if (isActivated == true)
            {
                /// Фазы боя с боссом
                switch (stage)
                {
                    case 1:
                        if (isMove == true)
                        {
                            if (tr.position == target)
                            {
                                isMove = false;
                                RotatePlayer();
                                saw1.SetActive(true);
                                saw2.SetActive(true);
                                stamina.localScale = Vector3.zero;
                                srStamina.color = new Color(0f, 0.5f, 1f, 1f);
                                if (coroutineStamina != null) { StopCoroutine(coroutineStamina); }
                                coroutineStamina = StartCoroutine(StaminaAnim(timeStamina, 100));
                                StartCoroutine(Retarget1());
                            }
                            else
                            {
                                tr.position = Vector2.MoveTowards(tr.position, target, speed);
                            }
                        }
                        break;
                    case 2:
                        if (isMove == true)
                        {
                            if (tr.position == target)
                            {
                                isMove = false;
                                RotatePlayer();
                                saw.localScale = Vector3.zero;
                                saw1.SetActive(true);
                                saw2.SetActive(true);
                                stamina.localScale = Vector3.zero;
                                srStamina.color = new Color(0f, 0.5f, 1f, 1f);
                                if (coroutineStamina != null) { StopCoroutine(coroutineStamina); }
                                coroutineStamina = StartCoroutine(StaminaAnim(timeStamina, 100));
                                StartCoroutine(Retarget2());
                            }
                            else
                            {
                                tr.position = Vector2.MoveTowards(tr.position, target, speed);
                            }
                        }
                        break;
                    case 3:
                        if (isMove == true)
                        {
                            if (tr.position == target)
                            {
                                isMove = false;
                                RotatePlayer();
                                saw.localScale = Vector3.zero;
                                LaserEnable();
                                stamina.localScale = Vector3.zero;
                                srStamina.color = new Color(0f, 0.5f, 1f, 1f);
                                if (coroutineStamina != null) { StopCoroutine(coroutineStamina); }
                                coroutineStamina = StartCoroutine(StaminaAnim(timeStamina, 100));
                                StartCoroutine(Retarget3());
                            }
                            else
                            {
                                tr.position = Vector2.MoveTowards(tr.position, target, speed);
                            }
                        }
                        break;
                    case 4:
                        if (isMove == true)
                        {
                            if (tr.position == target)
                            {
                                isMove = false;
                                RotatePlayer();
                                saw.localScale = Vector3.zero;
                                LaserEnable();
                                stamina.localScale = Vector3.zero;
                                srStamina.color = new Color(0f, 0.5f, 1f, 1f);
                                if (coroutineStamina != null) { StopCoroutine(coroutineStamina); }
                                coroutineStamina = StartCoroutine(StaminaAnim(timeStamina, 100));
                                StartCoroutine(Retarget4());
                            }
                            else
                            {
                                tr.position = Vector2.MoveTowards(tr.position, target, speed);
                            }
                        }
                        break;
                    case 5:
                        if (isMove == true)
                        {
                            if (tr.position == target)
                            {
                                isMove = false;
                                RotatePlayer();
                                saw.localScale = Vector3.zero;
                                LaserEnable();
                                saw1.SetActive(false);
                                saw2.SetActive(false);
                                stamina.localScale = Vector3.zero;
                                srStamina.color = new Color(0f, 0.5f, 1f, 1f);
                                if (coroutineStamina != null) { StopCoroutine(coroutineStamina); }
                                coroutineStamina = StartCoroutine(StaminaAnim(timeStamina, 100));
                                StartCoroutine(Retarget5());
                            }
                            else
                            {
                                tr.position = Vector2.MoveTowards(tr.position, target, speed);
                            }
                        }
                        break;
                }
            }
            else
            {
                /// Старт боя с боссом
                if (trigStart.enabled == false)
                {
                    isActivated = true;
                    audioBase.UpSound(0.01f, 5, 0, TypePlaying.Music);
                    explosion.health = 0f;
                    explosion.StartCoroutineTimerOffsetExplosion();
                    RegionDetected();
                    LaserDisable();
                    target = Target();
                }
            }
        }
    }
    /// Вращение лазеров
    public void FixedUpdate()
    {
        if (!isMove && isActivated)
        {
            laserOffset1.localEulerAngles = new Vector3(0f, 0f, laserOffset1.localEulerAngles.z + speedRotate);
            laserOffset2.localEulerAngles = new Vector3(0f, 0f, laserOffset2.localEulerAngles.z + speedRotate);
            if (isWorkingLaser)
            {
                state.localEulerAngles = new Vector3(0f, 0f, state.localEulerAngles.z + speedRotate);
            }
        }
    }

    /// Рандомное размещение лазеров
    public void RotatePlayer()
    {
        Vector2 p = pl.position;
        float angle = Mathf.Atan2(p.y, p.x) * Mathf.Rad2Deg;
        laserOffset1.localEulerAngles = new Vector3(0f, 0f, angle);
        laserOffset2.localEulerAngles = new Vector3(0f, 0f, angle - 180f);
    }

    /// Позиция лазеров
    private Vector3[] posLasers = new Vector3[] { Vector3.zero, Vector3.zero};
    public void TriggerLaserDefect(int id)
    {
        switch (id)
        {
            case 1: state1.active = false; state1.lr1.SetPositions(posLasers); break;
            case 2: state2.active = false; state2.lr1.SetPositions(posLasers); break;
            case 3: state3.active = false; state3.lr1.SetPositions(posLasers); break;
            case 4: state4.active = false; state4.lr1.SetPositions(posLasers); break;
        }
        if (!state1.active && !state2.active && !state3.active && !state4.active)
        {
            isWorkingLaser = false;
            state1.active = false;
            state2.active = false;
            state3.active = false;
            state4.active = false;
            laserL1.active = false;
            laserL2.active = false;
            laser1.localPosition = Vector2.zero;
            laser2.localPosition = Vector2.zero;
        }
    }

    /// Получения урона
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            hp -= pl.GetComponent<Power>().power;
            health.localScale = new Vector2(hp / 50f, hp / 50f);
            stage = 5 - (int)(hp / 25f);
            if (stage == 4)
            {
                LaserBlockDisable();
            }
            if (hp <= 0f && isAlive == true)
            {
                audioBase.LowerSound(0.1f, 50, 0, TypePlaying.Music);
                audioBase.SetSound(setEnd, 0, 0.8f, TypePlaying.Music, true, 1f);
                GameObject deadInside = Instantiate(explosionAsset, pl.position, Quaternion.identity);
                deadInside.GetComponent<Rigidbody2D>().isKinematic = true;
                deadInside.transform.localScale = new Vector2(2f, 2f);
                Explosion exp = deadInside.GetComponent<Explosion>();
                exp.radius = 2f;
                exp.health = 0f;
                exp.timeOffsetExplosion = 3f;
                exp.StartCoroutineTimerOffsetExplosion();
                gateStart.OnTriggerEnter2D(player.GetComponent<Collider2D>());
                gateEnd.OnTriggerEnter2D(player.GetComponent<Collider2D>());
                PlayerPrefs.SetString("boss2", "death");
                blockWin.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    /// Триггер, стартующий босса
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            blockWin.SetActive(true);
            trigStart.enabled = false;
        }
    }

    /// Включение общих лазера
    public void LaserEnable()
    {
        if (isWorkingLaser)
        {
            laserL1.active = true;
            laserL2.active = true;
            state1.active = false;
            state2.active = false;
            state3.active = false;
            state4.active = false;
        }
        laser1.localPosition = new Vector2(0f, -1f);
        laser2.localPosition = new Vector2(0f, -1f);
        return;
    }

    /// Отключение общих лазера
    public void LaserDisable()
    {
        if (isWorkingLaser)
        {
            state1.active = true;
            state2.active = true;
            state3.active = true;
            state4.active = true;
            laserL1.active = false;
            laserL2.active = false;
        }
        laser1.localPosition = Vector2.zero;
        laser2.localPosition = Vector2.zero;
        return;
    }

    /// Включение лазеров босса
    public void LaserBlockEnable()
    {
        laserDetected1.enabled = true;
        laserDetected2.enabled = true;
    }

    /// Выключение лазеров босса
    public void LaserBlockDisable()
    {
        laserDetected1.enabled = false;
        laserDetected2.enabled = false;
    }

    /// Определение региона нахождения игрока
    public void RegionDetected()
    {
        Vector2 result = Vector2.zero;
        Vector2 pos = pl.position;
        if (pos.x > -45f & pos.x <= -30f) { result.x = 1; }
        else if (pos.x > -30f & pos.x < -5f) { result.x = 2; }
        else if (pos.x >= -5f & pos.x <= 5f) { result.x = 3; }
        else if (pos.x > 5f & pos.x <= 30f) { result.x = 4; }
        else if (pos.x >= 30f & pos.x < 45f) { result.x = 5; }

        if (pos.y > -45f & pos.y <= -30f) { result.y = 1; }
        else if (pos.y > -30f & pos.y < -5f) { result.y = 2; }
        else if (pos.y >= -5f & pos.y <= 5f) { result.y = 3; }
        else if (pos.y > 5f & pos.y <= 30f) { result.y = 4; }
        else if (pos.y >= 30f & pos.y < 45f) { result.y = 5; }
        region = result;
        return;
    }

    /// Рандомное перемещение по регионам
    private readonly Vector2[] aroundCloser = new Vector2[] 
    {
        new Vector2(2, 2), new Vector2(2, 3), new Vector2(2, 4),
        new Vector2(3, 2), new Vector2(3, 4), new Vector2(4, 2),
        new Vector2(4, 3), new Vector2(4, 4)
    };

    /// Рандомная позиция в области нахождения игрока
    public Vector2 Target()
    {
        Vector2 result = Vector2.zero;
        if (region == new Vector2(3, 3))
        {
            region = aroundCloser[Random.Range(0, 8)];
        }
        switch (region.x)
        {
            case 1:
                result.x = Random.Range(-45f, -32f);
                break;
            case 2:
                result.x = Random.Range(-29f, -5f);
                break;
            case 3:
                result.x = Random.Range(-5f, 5f);
                break;
            case 4:
                result.x = Random.Range(5f, 29f);
                break;
            case 5:
                result.x = Random.Range(32f, 45f);
                break;
        }
        switch (region.y)
        {
            case 1:
                result.y = Random.Range(-45f, -32f);
                break;
            case 2:
                result.y = Random.Range(-29f, -5f);
                break;
            case 3:
                result.y = Random.Range(-5f, 5f);
                break;
            case 4:
                result.y = Random.Range(5f, 29f);
                break;
            case 5:
                result.y = Random.Range(32f, 45f);
                break;
        }
        isMove = true;
        return result;
    }

    /// Показательный таймер до следующего движения босса
    public IEnumerator StaminaAnim(float time, int count)
    {
        yield return new WaitForSeconds(time);
        float sc = hp * (100f - count) / 5000f;
        stamina.localScale = new Vector2(sc, sc);
        if (count > 1)
        {
            count -= 1;
            coroutineStamina = StartCoroutine(StaminaAnim(time, count));
        }
    }

    /// Изменение позиции для 1 фазы
    public IEnumerator Retarget1()
    {
        yield return new WaitForSeconds(timeRetarget);
        srStamina.color = new Color(0f, 0.5f, 1f, 0f);
        RotatePlayer();
        saw1.SetActive(false);
        saw2.SetActive(false);
        RegionDetected();
        target = Target();
    }

    /// Изменение позиции для 2 фазы
    public IEnumerator Retarget2()
    {
        yield return new WaitForSeconds(timeRetarget);
        srStamina.color = new Color(0f, 0.5f, 1f, 0f);
        RotatePlayer();
        saw.localScale = new Vector2(2f, 2f);
        saw1.SetActive(false);
        saw2.SetActive(false);
        RegionDetected();
        target = Target();
    }

    /// Изменение позиции для 3 фазы
    public IEnumerator Retarget3()
    {
        yield return new WaitForSeconds(timeRetarget);
        srStamina.color = new Color(0f, 0.5f, 1f, 0f);
        RotatePlayer();
        saw.localScale = new Vector2(2f, 2f);
        LaserDisable();
        RegionDetected();
        target = Target();
    }

    /// Изменение позиции для 4 фазы
    public IEnumerator Retarget4()
    {
        yield return new WaitForSeconds(timeRetarget);
        srStamina.color = new Color(0f, 0.5f, 1f, 0f);
        RotatePlayer();
        saw.localScale = new Vector2(2f, 2f);
        LaserDisable();
        RegionDetected();
        target = Target();
    }

    /// Изменение позиции для 5 фазы
    public IEnumerator Retarget5()
    {
        yield return new WaitForSeconds(timeRetarget);
        srStamina.color = new Color(0f, 0.5f, 1f, 0f);
        RotatePlayer();
        saw.localScale = new Vector2(2f, 2f);
        saw1.SetActive(true);
        saw2.SetActive(true);
        LaserDisable();
        RegionDetected();
        target = Target();
    }
}