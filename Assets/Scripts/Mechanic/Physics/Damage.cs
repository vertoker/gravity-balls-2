using UnityEngine;

/// Общий скрипт для нанесения урона
public class Damage : MonoBehaviour
{
    public enum TypeDamage { Player = 1, Saw = 2, Laser = 3, Explosion = 4 };
    public TypeDamage typeDamage = TypeDamage.Player;
    public float senDamage = 1f;
    private Player pl;
    private HealthBar hb;
    private Explosion el;
    private Transform tr;
    private Power pw;
    private Laser ls;
    private TypeLaser tl;
    private Damage dmg;
    private int tli = 0;
    private Saw sw;

    private void Awake()
    {
        if (typeDamage != TypeDamage.Player)
        {
            hb = Camera.main.GetComponent<Management>().healthBar;
            el = GetComponent<Explosion>();
            dmg = GetComponent<Damage>();
            if (typeDamage == TypeDamage.Explosion)
            {
                tr = GetComponent<Transform>();
            }
            else if (typeDamage == TypeDamage.Saw)
            {
                sw = GetComponent<Transform>().parent.GetComponent<Saw>();
            }
            else if (typeDamage == TypeDamage.Laser)
            {
                pl = GameObject.FindWithTag("Player").GetComponent<Player>();
                ls = GetComponent<Laser>();
                tl = GetComponent<TypeLaser>();
                tli = tl.Type2int();
            }
        }
        else
        {
            pw = GetComponent<Power>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (typeDamage == TypeDamage.Player)
        {
            Player(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (typeDamage == TypeDamage.Saw)
        {
            Saw(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (typeDamage == TypeDamage.Explosion)
        {
            Explosion(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (typeDamage == TypeDamage.Laser)
        {
            Laser(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (typeDamage == TypeDamage.Laser)
        {
            switch (tli)
            {
                case 2:
                    hb.VisFalse();
                    break;
                case 3:
                    pl.End2(tl.input);
                    break;
                case 4:
                    hb.End(tl.input);
                    break;
                case 5:

                    break;
                case 6:

                    break;
            }
        }
    }

    private void Player(GameObject input)/// Триггер для игрока
    {
        if (input.CompareTag("Wall"))
        {
            if (input.GetComponent<PhysicsEmulation>())
            {
                input.GetComponent<PhysicsEmulation>().PowerPhysicsEmulation(pw);
            }
        }
        if (input.CompareTag("Dynamic"))
        {
            if (input.GetComponent<VelocityInput>().inVelocityField == false)
            {
                input.GetComponent<PhysicsEmulation>().PowerPhysicsEmulation(pw);
            }
        }
        else if (input.CompareTag("Bomb"))
        {
            if (input.GetComponent<Explosion>())
            {
                input.GetComponent<Explosion>().ActionExplosionEmulation(gameObject);
            }
        }
    }

    private void Saw(GameObject input)/// Триггер для пилы
    {
        if (input.CompareTag("Player"))
        {
            if (input.layer == 12)
            {
                hb.Damage(senDamage * 3f, tag, Vector2.zero);
            }
        }
        else if (input.CompareTag("Dynamic"))
        {
            if (input.GetComponent<VelocityInput>().inVelocityField == false)
            {
                input.GetComponent<PhysicsEmulation>().SawPhysicsEmulation(dmg);
            }
        }
        else if (input.CompareTag("Bomb"))
        {
            input.GetComponent<Explosion>().ActionExplosionEmulation(gameObject);
        }
    }

    private void Laser(GameObject input)/// Триггер для лазера
    {
        if (ls.active)
        {
            if (input.CompareTag("Player"))
            {
                if (input.layer == 12)
                {
                    switch (tli)
                    {
                        case 1:
                            hb.Damage(senDamage, tag, Vector2.zero);
                            break;
                        case 2:
                            hb.Heal(senDamage);
                            break;
                        case 3:
                            pl.SlowMo(tl.input);
                            break;
                        case 4:
                            hb.Immortality();
                            break;
                        case 5:
                            pl.m.SetGravity(tl.input);
                            break;
                        case 6:
                            pl.Mass(tl.input);
                            break;
                    }
                }
            }
            else if (input.CompareTag("Bomb"))
            {
                input.GetComponent<Explosion>().ActionExplosionEmulation(gameObject);
            }
            else if (input.CompareTag("Dynamic"))
            {
                input.GetComponent<PhysicsEmulation>().LaserPhysicsEmulation(dmg);
            }
        }
    }

    private void Explosion(GameObject input)/// Триггер для взрыва
    {
        if (input.GetComponent<PhysicsEmulation>())
        {
            el.writeContacs = false;
            input.GetComponent<PhysicsEmulation>().ExplosionPhysicsEmulation(el);
            el.APEvirtual();
        }
        else if (input.CompareTag("Player"))
        {
            if (input.layer == 12)
            {
                Transform transformInput = input.transform;
                float del1 = Vector2.Distance(tr.position, transformInput.position);
                float del2 = el.radius;
                float sen = el.power * (del1 / del2);
                float damage = el.power - sen;
                Vector2 velocity = (transformInput.position - tr.position) / del1 * damage;
                hb.Damage(damage, tag, velocity);
            }
        }
    }

    public TypeLaser GetTypeLaser()
    {
        return tl;
    }
}