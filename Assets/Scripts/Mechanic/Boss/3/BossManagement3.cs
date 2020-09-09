using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// Менеджмент 3 босса
public class BossManagement3 : MonoBehaviour
{
    public float health = 100f;
    public Vector4[] boxs = new Vector4[0];
    public int[] saw1Fields = new int[0];
    public int[] saw2Fields = new int[0];
    public int[] saw3Fields = new int[0];
    public int[] laser1Fields = new int[0];
    public int[] laser2Fields = new int[0];
    public Transform trBoss;
    public SpriteRenderer srBoss;
    public BossTracing3 bt;
    public Transform saw1;
    public Transform saw2;
    public Transform saw3;
    public Transform laser;
    public Transform laser1;
    public Transform laser2;
    public Transform trap1;
    public Transform trap2;
    public Transform trap3;
    public Transform trap4;
    public LineRenderer lr1;
    public LineRenderer lr2;
    public TrailRenderer trail;
    public GameObject exp;
    public GameObject terminal1;
    public GameObject terminal2;
    public GameObject LaserTarget;
    public GameObject LaserMover;
    public GameObject TrapsMover;
    public GameObject SawMover;
    public GameObject SawsAroundMover;
    public Explosion explosion;
    public SpriteRenderer sr;
    public CircleCollider2D cc;
    public Animator animatorEnd;
    public bool isMove = false;
    public bool isMoveSaw1 = false;
    public bool isMoveSaw2 = false;
    public bool isMoveSaw3 = false;
    public bool isMoveLaser1 = false;
    public bool isMoveLaser2 = false;
    public bool isMoveTraps = false;
    public int loadScene = 35;
    public int fieldPlayer = 0;
    private bool isActive = true;

    private float maxHealth;
    private Vector2 target = Vector2.zero;
    private Vector2 saw1target = Vector2.zero;
    private Vector2 saw2target = Vector2.zero;
    private Vector2 saw3target = Vector2.zero;
    private Vector2 laser1target = Vector2.zero;
    private Vector2 laser2target = Vector2.zero;
    private Vector2 traptarget1 = Vector2.zero;
    private Vector2 traptarget2 = Vector2.zero;
    private Vector2 traptarget3 = Vector2.zero;
    private Vector2 traptarget4 = Vector2.zero;
    private Vector2 border = new Vector2(47f, 44.5f);
    private Management m;
    public GameObject p { get; private set; }
    private HealthBar hb;
    private Transform tr;
    private Power ppl;
    private int lengthBoxs = 0;
    private bool isLife = true;

    public void Awake()
    {
        isActive = !(PlayerPrefs.GetString("boss1") == "life" && PlayerPrefs.GetString("boss2") == "life");
        terminal1.SetActive(!isActive);
        terminal2.SetActive(isActive);
        trail.enabled = PlayerPrefs.GetString("graphicsquality") != "low";
        m = GameObject.FindWithTag("MainCamera").GetComponent<Management>();
        lengthBoxs = boxs.Length;
        maxHealth = health;
        hb = m.healthBar;
        p = m.player;
        tr = p.transform;
        ppl = m.ppl;
        float c = health / maxHealth;
        srBoss.color = new Color(0f, 0f, c);
    }

    public void Start()
    {
        if (isActive == false) { return; }
        StartCoroutine(Mover());

        /// Определение регионов
        fieldPlayer = bt.BoxPos(tr.position);
        if (fieldPlayer >= 0)
        {
            Vector4 r = boxs[saw1Fields[fieldPlayer]];
            saw1target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
            r = boxs[saw2Fields[fieldPlayer]];
            saw2target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
            r = boxs[saw3Fields[fieldPlayer]];
            saw3target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
            r = boxs[laser1Fields[fieldPlayer]];
            laser1target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
            r = boxs[laser2Fields[fieldPlayer]];
            laser2target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }
        else
        {
            Vector4 r = boxs[Random.Range(0, lengthBoxs)];
            saw1target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
            r = boxs[Random.Range(0, lengthBoxs)];
            saw2target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
            r = boxs[Random.Range(0, lengthBoxs)];
            saw3target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
            r = boxs[Random.Range(0, lengthBoxs)];
            laser1target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
            r = boxs[Random.Range(0, lengthBoxs)];
            laser2target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }

        TrapMover();
        StartCoroutine(Laser1AIM());
        StartCoroutine(Laser2AIM());
        isMoveSaw1 = true;
        isMoveSaw2 = true;
        isMoveSaw3 = true;
        isMoveLaser1 = true;
        isMoveLaser2 = true;
        return;
    }

    /// Смена цели движения 1 пилы
    public void SawMover1()
    {
        fieldPlayer = bt.BoxPos(tr.position);
        if (fieldPlayer >= 0)
        {
            Vector4 r = boxs[saw1Fields[fieldPlayer]];
            saw1target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }
        else
        {
            Vector4 r = boxs[Random.Range(0, lengthBoxs)];
            saw1target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }
        isMoveSaw1 = true;
    }

    /// Смена цели движения 2 пилы
    public void SawMover2()
    {
        fieldPlayer = bt.BoxPos(tr.position);
        if (fieldPlayer >= 0)
        {
            Vector4 r = boxs[saw2Fields[fieldPlayer]];
            saw2target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }
        else
        {
            Vector4 r = boxs[Random.Range(0, lengthBoxs)];
            saw2target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }
        isMoveSaw2 = true;
    }

    /// Смена цели движения 3 пилы
    public void SawMover3()
    {
        fieldPlayer = bt.BoxPos(tr.position);
        if (fieldPlayer >= 0)
        {
            Vector4 r = boxs[saw3Fields[fieldPlayer]];
            saw3target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }
        else
        {
            Vector4 r = boxs[Random.Range(0, lengthBoxs)];
            saw3target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }
        isMoveSaw3 = true;
    }

    /// Смена цели движения 1 лазера
    public void LaserMover1()
    {
        fieldPlayer = bt.BoxPos(tr.position);
        if (fieldPlayer >= 0)
        {
            Vector4 r = boxs[laser1Fields[fieldPlayer]];
            laser1target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }
        else
        {
            Vector4 r = boxs[Random.Range(0, lengthBoxs)];
            laser1target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }
        StartCoroutine(Laser1AIM());
        isMoveLaser1 = true;
    }

    /// Смена цели движения 2 лазера
    public void LaserMover2()
    {
        fieldPlayer = bt.BoxPos(tr.position);
        if (fieldPlayer >= 0)
        {
            Vector4 r = boxs[laser2Fields[fieldPlayer]];
            laser2target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }
        else
        {
            Vector4 r = boxs[Random.Range(0, lengthBoxs)];
            laser2target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
        }
        StartCoroutine(Laser2AIM());
        isMoveLaser2 = true;
    }

    /// Смена цели движения ловушек
    public void TrapMover()
    {
        traptarget1 = new Vector2(Random.Range(-border.x, border.x), Random.Range(-border.y, border.y));
        traptarget2 = new Vector2(-traptarget1.x, -traptarget1.y);
        traptarget3 = new Vector2(-traptarget1.x, traptarget1.y);
        traptarget4 = new Vector2(traptarget1.x, -traptarget1.y);
        isMoveTraps = true;
    }

    /// АИМ лазеров
    public IEnumerator Laser1AIM()
    {
        yield return new WaitForSeconds(0.5f);
        Vector2 diff = tr.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90f;
        laser1.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
    public IEnumerator Laser2AIM()
    {
        yield return new WaitForSeconds(0.5f);
        Vector2 diff = tr.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90f;
        laser2.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    /// Активация движения
    public IEnumerator Mover()
    {
        yield return new WaitForSeconds(7.5f);
        if (isLife)
        {
            Vector2 diff = tr.position;
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90f;
            laser.rotation = Quaternion.Euler(0f, 0f, rot_z);
            target = bt.GetPosRaycast();
            isMove = true;
        }
    }

    /// Основной метод передвижения
    public void Update()
    {
        if (isActive == false) { return; }
        float s = Time.fixedDeltaTime / (0.03f / Time.timeScale);
        if (isMove)/// Движение босса
        {
            trBoss.position = Vector2.MoveTowards(trBoss.position, target, s * 0.5f);
            if (trBoss.position == (Vector3)target)
            {
                isMove = false;
                if (isLife)
                {
                    StartCoroutine(Mover());
                }
            }
        }
        if (isMoveSaw1)/// Движение 1 пилы
        {
            saw1.position = Vector2.MoveTowards(saw1.position, saw1target, s * 0.1f);
            if (saw1.position == (Vector3)saw1target)
            {
                isMoveSaw1 = false;
                if (isLife)
                {
                    SawMover1();
                }
            }
        }
        if (isMoveSaw2)/// Движение 2 пилы
        {
            saw2.position = Vector2.MoveTowards(saw2.position, saw2target, s * 0.1f);
            if (saw2.position == (Vector3)saw2target)
            {
                isMoveSaw2 = false;
                if (isLife)
                {
                    SawMover2();
                }
            }
        }
        if (isMoveSaw3)/// Движение 3 пилы
        {
            saw3.position = Vector2.MoveTowards(saw3.position, saw3target, s * 0.1f);
            if (saw3.position == (Vector3)saw3target)
            {
                isMoveSaw3 = false;
                if (isLife)
                {
                    SawMover3();
                }
            }
        }
        if (isMoveLaser1)/// Движение 1 лазера
        {
            laser1.position = Vector2.Lerp(laser1.position, laser1target, s * 0.1f);
            if (laser1.position == (Vector3)laser1target)
            {
                isMoveLaser1 = false;
                if (isLife)
                {
                    LaserMover1();
                }
            }
        }
        if (isMoveLaser2)/// Движение 2 лазера
        {
            laser2.position = Vector2.Lerp(laser2.position, laser2target, s * 0.1f);
            if (laser2.position == (Vector3)laser2target)
            {
                isMoveLaser2 = false;
                if (isLife)
                {
                    LaserMover2();
                }
            }
        }
        if (isMoveTraps)/// Движение ловушек
        {
            trap1.position = Vector2.MoveTowards(trap1.position, traptarget1, s * 0.1f);
            trap2.position = Vector2.MoveTowards(trap2.position, traptarget2, s * 0.1f);
            trap3.position = Vector2.MoveTowards(trap3.position, traptarget3, s * 0.1f);
            trap4.position = Vector2.MoveTowards(trap4.position, traptarget4, s * 0.1f);
            lr1.SetPosition(0, trap1.position);
            lr1.SetPosition(1, trap2.position);
            lr2.SetPosition(0, trap3.position);
            lr2.SetPosition(1, trap4.position);
            if (trap1.position == (Vector3)traptarget1)
            {
                isMoveTraps = false;
                if (isLife)
                {
                    TrapMover();
                }
            }
        }
    }

    /// Взаимодействие между боссом и игроком
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == p)
        {
            if (isActive == false) { isActive = true; Start(); }
            if (isMove == true)/// При движении убивать перманентно игрока
            {
                hb.StraightDamage(10f, "Boss3");
            }
            else/// Иначе наносить урон боссу
            {
                health -= ppl.power;
                float c = health / maxHealth;
                srBoss.color = new Color(0f, 0f, c);
                trail.startColor = srBoss.color;
                if (health <= 0f)
                {
                    isLife = false;
                    isMove = false;
                    saw1target = trBoss.position;
                    saw2target = trBoss.position;
                    saw3target = trBoss.position;
                    isMoveSaw1 = true;
                    isMoveSaw2 = true;
                    isMoveSaw3 = true;
                    sr.enabled = false;
                    cc.enabled = false;
                    exp.SetActive(true);
                    explosion.health = 0f;
                    explosion.StartCoroutineTimerOffsetExplosion();
                    Vector2 diff = trBoss.position;
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90f;
                    laser.rotation = Quaternion.Euler(0f, 0f, rot_z);
                    int fieldBoss = bt.BoxPos(trBoss.position);
                    Vector4 r = boxs[laser1Fields[fieldBoss]];
                    laser1target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
                    r = boxs[laser2Fields[fieldBoss]];
                    laser2target = new Vector2(Random.Range(r.z, r.x), Random.Range(r.w, r.y));
                    StartCoroutine(Ended());
                }
            }
        }
    }
    /// Старт смерти босса
    public void EndedCoroutine()
    {
        if (!isActive)
        {
            isActive = true;
            StartCoroutine(Ended());
        }
    }

    /// Таймер до начала затухания экрана
    public IEnumerator Ended()
    {
        yield return new WaitForSeconds(6.5f);
        if (hb.healthBarImage.fillAmount != 0f)
        {
            animatorEnd.SetBool("isActive", true);
            StartCoroutine(EndedFunction());
        }
    }

    ///Переход в концовку игры 
    public IEnumerator EndedFunction()
    {
        yield return new WaitForSeconds(1.5f);
        if (hb.healthBarImage.fillAmount != 0f)
        {
            PlayerPrefs.SetInt("progress", 35);
            SceneManager.LoadSceneAsync(loadScene);
        }
    }

    /// Подключение оружий босса
    public void ControlDamagers(bool lt, bool lm, bool tm, bool sm, bool sam)
    {
        LaserTarget.SetActive(lt);
        LaserMover.SetActive(lm);
        TrapsMover.SetActive(tm);
        SawMover.SetActive(sm);
        SawsAroundMover.SetActive(sam);
    }
}