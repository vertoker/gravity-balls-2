using UnityEngine;

/// Общий скрипт для эмуляции физической разрушаемости
public class PhysicsEmulation : MonoBehaviour
{
    public float health = 1f;
    public bool isEffect = true;
    public GameObject objSpawn;

    private Transform tr;
    private SpriteRenderer sr;
    private bool rb2Bool = false;
    private Rigidbody2D rb2;
    private bool c2Bool = false;
    private Collider2D c2;
    private bool isMakeDestroying = true;
    private bool isDestroying = true;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
        if (GetComponent<Rigidbody2D>())
        {
            rb2Bool = true;
            rb2 = GetComponent<Rigidbody2D>();
        }
        if (GetComponent<Collider2D>())
        {
            c2Bool = true;
            c2 = GetComponent<Collider2D>();
            if (GetComponent<BoxCollider2D>())
            {
                Vector2 sz = GetComponent<BoxCollider2D>().size;
                bool isHigh = PlayerPrefs.GetString("graphicsquality") == "high";
                isDestroying = PlayerPrefs.GetString("graphicsquality") == "low";
                isMakeDestroying = !isDestroying && (isHigh || sz == new Vector2(1f, 1f));
            }
        }
    }

    /// Эмуляция от столкновения с простым объектом (игрок или 3 босс)
    public void PowerPhysicsEmulation(Power power)
    {
        if (health <= 0f) { return; }
        float damage = power.power;
        health -= damage;
        if (health <= 0f)
        {
            if (isMakeDestroying == true)
            {
                if (rb2Bool)
                {
                    rb2.gravityScale = 0f;
                    rb2.velocity = Vector2.zero;
                }
                sr.enabled = false;
                c2.enabled = !c2Bool;
                Vector2 pos = tr.position;
                Vector2 pos2 = power.transform.position;
                float dels = Vector2.Distance(pos2, pos); tr.position = Vector2.zero;
                GameObject ds = Instantiate(objSpawn, pos, Quaternion.Euler(tr.localEulerAngles), tr);
                ds.GetComponent<DestroyChilds>().ExplosionVelocity((pos2 - pos) / dels, isEffect, false);
                return;
            }
            else
            {
                if (isDestroying)
                {
                    gameObject.SetActive(false);
                    return;
                }
                gameObject.layer = 13;
            }
        }
        return;
    }

    /// Эмуляция для пилы
    public void SawPhysicsEmulation(Damage d)
    {
        if (health <= 0f) { return; }
        float damage = d.senDamage * 10f;
        health -= damage;
        if (health <= 0f)
        {
            if (isMakeDestroying == true)
            {
                if (rb2Bool)
                {
                    rb2.gravityScale = 0f;
                    rb2.velocity = Vector2.zero;
                }
                sr.enabled = false;
                c2.enabled = !c2Bool;
                Vector2 pos = tr.position;
                Vector2 pos2 = d.transform.position;
                float dels = Vector2.Distance(pos2, pos); tr.position = Vector2.zero;
                GameObject ds = Instantiate(objSpawn, pos, Quaternion.Euler(tr.localEulerAngles), tr);
                ds.GetComponent<DestroyChilds>().ExplosionVelocity((pos2 - pos) / dels, isEffect, false);
                return;
            }
            else
            {
                if (isDestroying)
                {
                    gameObject.SetActive(false);
                    return;
                }
                gameObject.layer = 13;
            }
        }
        return;
    }

    /// Эмуляция для лазера
    public void LaserPhysicsEmulation(Damage d)
    {
        if (health <= 0f) { return; }
        float damage = d.senDamage;
        health -= damage;
        if (health <= 0f)
        {
            if (isMakeDestroying == true)
            {
                if (rb2Bool)
                {
                    rb2.gravityScale = 0f;
                    rb2.velocity = Vector2.zero;
                }
                sr.enabled = false;
                c2.enabled = !c2Bool;
                Vector2 pos = tr.position;
                Vector2 pos2 = d.transform.position;
                float dels = Vector2.Distance(pos2, pos); tr.position = Vector2.zero;
                GameObject ds = Instantiate(objSpawn, pos, Quaternion.Euler(tr.localEulerAngles), tr);
                ds.GetComponent<DestroyChilds>().ExplosionVelocity((pos2 - pos) / dels, isEffect, false);
                return;
            }
            else
            {
                if (isDestroying)
                {
                    gameObject.SetActive(false);
                    return;
                }
                gameObject.layer = 13;
            }
        }
        return;
    }

    /// Эмуляция для взрыва
    public void ExplosionPhysicsEmulation(Explosion explosion)
    {
        if (health <= 0f) { return; }
        Vector2 pos = tr.position;
        Vector2 pos2 = explosion.transform.position;
        float dels = Vector2.Distance(pos, pos2);
        float del2 = explosion.radius;
        float sen = explosion.power * (dels / del2);
        float damage = explosion.power - sen;
        Vector2 velocity = (pos - pos2) / dels * damage;
        if (explosion.GetComponent<CircleCollider2D>())
        {
            Destroy(explosion.GetComponent<CircleCollider2D>());
        }

        health -= damage;
        if (health <= 0f)
        {
            if (isMakeDestroying == true)
            {
                if (rb2Bool)
                {
                    rb2.gravityScale = 0f;
                    rb2.velocity = Vector2.zero;
                }
                sr.enabled = false;
                c2.enabled = !c2Bool;
                dels = Vector2.Distance(pos2, pos); tr.position = Vector2.zero;
                GameObject ds = Instantiate(objSpawn, pos, Quaternion.Euler(tr.localEulerAngles), tr);
                Vector2 count = (pos2 - pos) / dels + velocity;
                ds.GetComponent<DestroyChilds>().ExplosionVelocity(count, isEffect, true);
                return;
            }
            else
            {
                if (isDestroying)
                {
                    gameObject.SetActive(false);
                    return;
                }
                gameObject.layer = 13;
            }
        }
        return;
    }

    /// Эмуляция для давящих стен
    public void TrampAnimPhysicsEmulation(TrampAnim trampanim)
    {
        if (health <= 0f) { return; }
        float damage = trampanim.damage;
        health -= damage;
        if (health <= 0f)
        {
            if (isMakeDestroying == true)
            {
                if (rb2Bool)
                {
                    rb2.gravityScale = 0f;
                    rb2.velocity = Vector2.zero;
                }
                sr.enabled = false;
                c2.enabled = !c2Bool;
                Vector2 pos = tr.position;
                Vector2 pos2 = trampanim.transform.position;
                float dels = Vector2.Distance(pos2, pos); tr.position = Vector2.zero;
                GameObject ds = Instantiate(objSpawn, pos, Quaternion.Euler(tr.localEulerAngles), tr);
                ds.GetComponent<DestroyChilds>().ExplosionVelocity((pos2 - pos) / dels, isEffect, false);
                return;
            }
            else
            {
                if (isDestroying)
                {
                    gameObject.SetActive(false);
                    return;
                }
                gameObject.layer = 13;
            }
        }
        return;
    }

    /// Обновление здоровья (при изменении напрямую его извне)
    public void HealthUpdate(Transform tr2, Vector2 velocity, bool isBomb)
    {
        if (health <= 0f)
        {
            if (isMakeDestroying == true)
            {
                if (rb2Bool)
                {
                    rb2.gravityScale = 0f;
                    rb2.velocity = Vector2.zero;
                }
                sr.enabled = false;
                c2.enabled = !c2Bool;
                float dels = Vector2.Distance(tr2.position, tr.position);
                Vector2 pos = tr.position; tr.position = Vector2.zero;
                GameObject ds = Instantiate(objSpawn, pos, Quaternion.Euler(tr.localEulerAngles), tr);
                Vector2 count = ((Vector2)tr2.position - pos) / dels + velocity;
                ds.GetComponent<DestroyChilds>().ExplosionVelocity(count, isEffect, isBomb);
                return;
            }
            else
            {
                if (isDestroying)
                {
                    gameObject.SetActive(false);
                    return;
                }
                gameObject.layer = 13;
            }
        }
        return;
    }
}