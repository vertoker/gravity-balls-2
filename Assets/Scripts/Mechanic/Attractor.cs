using UnityEngine;

/// Триггер для притягивание объектов к определённой точке
public class Attractor : GlobalFunctions
{
    public float power = 1f;
    public Rigidbody2D[] inputs = new Rigidbody2D[0];
    public GameObject inGravity;
    private Transform tr;

    public void Awake()
    {
        tr = inGravity.transform;
    }

    public void FixedUpdate()
    {
        if (inputs.Length != 0)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] != null)
                {
                    float del = Vector2.Distance(tr.position, inputs[i].position);
                    Vector2 vel = (Vector2)tr.position - inputs[i].position;
                    Vector2 pwr = vel / del * power;
                    inputs[i].AddForce(pwr, ForceMode2D.Impulse);
                }
                else
                {
                    inputs = Remove(inputs, inputs[i]);
                    i -= 1;
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D rb2 = collision.GetComponent<Rigidbody2D>();
            if (collision.GetComponent<Destroy>())
            {
                collision.GetComponent<Destroy>().ActiveTimerDeleteChange(300f);
            }
            inputs = Add(inputs, rb2);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D rb2 = collision.GetComponent<Rigidbody2D>();
            if (collision.GetComponent<Destroy>())
            {
                collision.GetComponent<Destroy>().ActiveTimerDeleteChange(60f);
            }
            inputs = Remove(inputs, rb2);
        }
    }
}
