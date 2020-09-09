using UnityEngine;

/// Триггер для мгновенного убийства игрока
public class DeathlessScript : MonoBehaviour
{
    private HealthBar hb;

    private void Awake()
    {
        hb = Camera.main.GetComponent<Management>().healthBar;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hb.Damage(10f, tag, Vector2.zero);
        }
    }
}