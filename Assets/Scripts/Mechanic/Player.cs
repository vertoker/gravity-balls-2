using System.Collections;
using UnityEngine;

/// Скрипт для управления графических настроек игрока
public class Player : GlobalFunctions
{
    public Transform tr;
    public HealthBar healthBar;
    public Management m;
    public GameObject objSpawn;
    public Rigidbody2D rb;
    public Power power;
    public GameObject effect;
    public TrailRenderer trail;

    private GameObject destroySpawning;
    private float massEnd = 2f;
    private float speed = 0.05f;
    private bool active = false;
    public bool dead = false;
    private bool animationActive = false;
    private bool offsetActive = false;
    private GameObject[] childs;

    public void Awake()
    {
        string graphicsquality = PlayerPrefs.GetString("graphicsquality");
        animationActive = graphicsquality == "high";
        trail.enabled = graphicsquality != "low";
    }

    public void Heal(bool isSet, float setHealth, float plusHealth)
    {
        if (isSet == true)
        {
            healthBar.Heal(isSet, setHealth);
        }
        else
        {
            healthBar.Heal(isSet, plusHealth);
        }
    }

    public void Mass(bool isSet, float setMass, float plusMass, float speed2)
    {
        if (animationActive)
        {
            speed = speed2;
            if (isSet == true)
            {
                massEnd = setMass;
            }
            else
            {
                massEnd = Stable2(massEnd + plusMass, 0.01f, 100f);
            }
            active = true;
        }
        else
        {
            if (isSet == true)
            {
                rb.mass = setMass;
            }
            else
            {
                rb.mass = Stable2(rb.mass + plusMass, 0.01f, 100f);
            }
        }
    }

    public void Mass(float inp)
    {
        rb.mass = rb.mass + inp;
    }

    public void Gravity(bool isSet, float setGravity, float plusGravity, float speed)
    {
        m.SetGravity(isSet, setGravity, plusGravity, speed);
    }

    public void Immortality(float setSecImmortality)
    {
        healthBar.Immortality(setSecImmortality);
    }
    
    public void SlowMo(float timeDuration, float setSlowMo, float speed)
    {
        m.data.SlowMo(timeDuration);
        End2(timeDuration);
    }

    public void SlowMo(float timeDuration)
    {
        m.data.SlowMo(timeDuration);
    }

    public void End2(float timeduration)
    {
        m.data.End2(timeduration);
    }

    public GameObject[] Dead()
    {
        if (dead == false)
        {
            dead = true;
            tr.tag = "Untagged";
            if (trail.enabled)
            {
                Vector2 velocity = rb.velocity;
                rb.gravityScale = 0f;
                Vector2 pos = tr.position;
                tr.position = Vector2.zero;
                Destroy(GetComponent<CircleCollider2D>());
                Destroy(GetComponent<SpriteRenderer>());
                Destroy(GetComponent<Damage>());
                Destroy(GetComponent<Power>());

                destroySpawning = Instantiate(objSpawn, pos, new Quaternion(), tr);
                DeadPlayerManagement dpm = destroySpawning.GetComponent<DeadPlayerManagement>();
                dpm.SetAwake(animationActive, velocity);
                childs = dpm.childs;////////////////////////////////////////////
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1f);
            }
        }
        return childs;
    }

    private void FixedUpdate()
    {
        if (active == true)
        {
            if (rb.mass != massEnd)
            {
                rb.mass = MoveToward(rb.mass, massEnd, speed, new Vector2(0.01f, 100f));
            }
            active = rb.mass != massEnd;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animationActive || power.power < 20f) { return; }
        Vector2 pwr = power.velocity;
        float h = healthBar.healthEnd;
        float angle = Mathf.Atan2(pwr.y, pwr.x) * Mathf.Rad2Deg + 180f;
        Vector2 posLocal = Quaternion.Euler(0, 0, angle) * Vector2.up * 0.4f;

        Vector3 pos = new Vector3(posLocal.x, posLocal.y, -0.5f) + tr.position;
        Vector3 rot = new Vector3(angle - tr.rotation.z, -90f, 90f);
        GameObject ffct = Instantiate(effect, pos, Quaternion.Euler(rot), tr);
        ffct.GetComponent<ParticleSystem>().startColor = new Color(h, 0f, 0f, 0.75f);
        StartCoroutine(DeleteWallEffect(ffct));
    }

    private IEnumerator DeleteWallEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1f);
        Destroy(effect);
    }

    public Vector2 RotateVector(Vector2 a, float offsetAngle)//метод вращения объекта
    {
        float power = Mathf.Sqrt(a.x * a.x + a.y * a.y);//коэффициент силы
        float angle = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg - 90f + offsetAngle;//угол из координат с offset'ом
        return Quaternion.Euler(0, 0, angle) * Vector2.up * power;
        //построение вектора из изменённого угла с коэффициентом силы
    }
}
