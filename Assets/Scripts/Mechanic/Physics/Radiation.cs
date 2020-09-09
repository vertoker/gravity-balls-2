using System.Collections;
using UnityEngine;

/// Скрипт для нанесения постоянного урона при нахождении в области триггера
public class Radiation : MonoBehaviour
{
    public bool isActiveRadiation = false;
    private Management m;
    private HealthBar hb;

    private void Awake()
    {
        gameObject.SetActive(PlayerPrefs.GetString("ai") == "off");
        m = GameObject.FindWithTag("MainCamera").GetComponent<Management>();
        hb = m.healthBar;
    }

    private void Start()
    {
        StartCoroutine(RadiationDamage());
    }

    public IEnumerator RadiationDamage()
    {
        yield return new WaitForSeconds(0.0002f);
        if (isActiveRadiation)
        {
            hb.StraightDamage(0.0002f, "Radiation");
        }
        StartCoroutine(RadiationDamage());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActiveRadiation = true;
            hb.animator.SetBool("isVisible", true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActiveRadiation = false;
            hb.animator.SetBool("isVisible", false);
            if (hb.healthBarImage.fillAmount == 0f)
            {
                m.StartGraphics();
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            hb.animator.SetBool("isVisible", false);
            PlayerPrefs.SetString("ai", "on");
            gameObject.SetActive(false);
        }
    }
}
