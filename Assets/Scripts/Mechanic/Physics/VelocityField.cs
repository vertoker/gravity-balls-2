using UnityEngine;

/// Скрипт для силового поля (применим и к разрушаемым объектам)
public class VelocityField : GlobalFunctions
{
    public float percent = 10f;
    public float damage = 0.01f;
    public float heal = 0.01f;
    public GameObject[] contacts = new GameObject[0];
    private HealthBar hb;

    private void Awake()
    {
        hb = GameObject.FindWithTag("MainCamera").GetComponent<Management>().healthBar;
    }

    private void FixedUpdate()
    {
        if (contacts.Length != 0)
        {
            for (int i = 0; i < contacts.Length; i++)
            {
                if (contacts[i] != null)
                {
                    if (contacts[i].GetComponent<Rigidbody2D>())
                    {
                        float s = Time.fixedDeltaTime / 0.03f;
                        Vector2 vel = contacts[i].GetComponent<Rigidbody2D>().velocity;
                        contacts[i].GetComponent<Rigidbody2D>().velocity = vel / 100f * (100f - percent * s);
                    }
                }
                else
                {
                    contacts = Remove(contacts, i);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D rb2 = collision.GetComponent<Rigidbody2D>();
            if (rb2.isKinematic == false)
            {
                VelocityInput vi = collision.GetComponent<VelocityInput>();

                vi.fields = Add(vi.fields, gameObject);

                rb2.gravityScale = 0f;
                rb2.freezeRotation = true;
                vi.inVelocityField = true;
                if (collision.GetComponent<Destroy>())
                {
                    collision.GetComponent<Destroy>().ActiveTimerDeleteChange(300f);
                }
                if (collision.tag == "Player")
                {
                    hb.StartVFRad(damage);
                }

                contacts = Add(contacts, collision.gameObject);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D rb2 = collision.GetComponent<Rigidbody2D>();
            if (rb2.isKinematic == false)
            {
                VelocityInput vi = collision.GetComponent<VelocityInput>();

                vi.fields = Remove(vi.fields, gameObject);

                if (vi.fields.Length != 0)
                {
                    rb2.gravityScale = 0f;
                    rb2.freezeRotation = true;
                    vi.inVelocityField = true;
                }
                else
                {
                    rb2.gravityScale = 1f;
                    rb2.freezeRotation = false;
                    vi.inVelocityField = false;
                }
                if (collision.GetComponent<Destroy>())
                {
                    collision.GetComponent<Destroy>().ActiveTimerDeleteChange(60f);
                }
                if (collision.tag == "Player")
                {
                    hb.EndVFRad(heal);
                }

                contacts = Remove(contacts, collision.gameObject);
            }
        }
    }
}