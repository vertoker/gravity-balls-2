using System.Collections;
using UnityEngine;

/// Оптимизация физических объектов путём их удаления
public class Destroy : MonoBehaviour
{
    public float del = 3f;
    public Vector2 org = new Vector2(0.2f, 0.2f);
    private bool active = false;
    private float activeTimerDelete;
    private float mainTimerDelete;
    private bool activeTimerDeleteChanged = false;
    private VelocityInput velocityInput;
    private Rigidbody2D rb2;
    private float deler = 1f;

    private void Start()
    {
        switch (PlayerPrefs.GetString("graphicsquality"))
        {
            case "low":
                deler = 5f;
                break;
            case "medium":
                deler = 3f;
                break;
            case "high":
                deler = 1f;
                break;
        }
        velocityInput = GetComponent<VelocityInput>();
        rb2 = GetComponent<Rigidbody2D>();
        ActiveTimerDeleteChange(30f);
        StartCoroutine(StartActive());
        StartCoroutine(Delete2());
    }

    private IEnumerator StartActive()
    {
        yield return new WaitForSeconds(1f);
        active = true;
        StartCoroutine(ActiveUpdate());
    }
    
    private IEnumerator ActiveUpdate()
    {
        yield return new WaitForSeconds(0.5f);
        UpdateDestroy();
        StartCoroutine(ActiveUpdate());
    }

    private IEnumerator Delete2()
    {
        yield return new WaitForSeconds(0.1f);
        if (activeTimerDeleteChanged == false)
        {
            if (mainTimerDelete > 0f)
            {
                mainTimerDelete -= 0.1f;
            }
            else
            {
                if (activeTimerDelete != 300f)
                {
                    if (velocityInput.inVelocityField == true)
                    {
                        for (int i = 0; i < velocityInput.fields.Length; i++)
                        {
                            velocityInput.fields[i].GetComponent<VelocityField>().OnTriggerExit2D(GetComponent<Collider2D>());
                        }
                    }
                    DeathDelete();
                }
                else
                {
                    mainTimerDelete = activeTimerDelete;
                    activeTimerDeleteChanged = false;
                }
            }
        }
        else
        {
            mainTimerDelete = activeTimerDelete;
            activeTimerDeleteChanged = false;
        }
        StartCoroutine(Delete2());
    }

    public void ActiveTimerDeleteChange(float atd)
    {
        activeTimerDelete = atd / deler;
        activeTimerDeleteChanged = true;
        return;
    }

    private void UpdateDestroy()//Fixed
    {
        if (active)
        {
            Vector2 v = rb2.velocity;
            try
            {
                if (v.x > -org.x && v.x < org.x && v.y > -org.y && v.y < org.y && velocityInput.inVelocityField == false)
                {
                    active = false;
                    StartCoroutine(Delete());
                }
            } catch { }
        }
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(del / deler);
        Vector2 v = rb2.velocity;
        if (v.x > -org.x && v.x < org.x && v.y > -org.y && v.y < org.y && velocityInput.inVelocityField == false)
        {
            DeathDelete();
        }
        else
        {
            active = true;
        }
    }

    private void DeathDelete()
    {
        if (GetComponent<Collider2D>())
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
        Destroy(gameObject);
    }
}