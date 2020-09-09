using UnityEngine;

/// Бустер модификации слоу-мо
public class SlowMo : MonoBehaviour
{
    [Range(0f, 100f)]
    public float timeDuration = 4f;
    [Range(0.001f, 1f)]
    public float speed = 0.01f;
    [Range(0.01f, 1f)]
    public float setSlowMo = 0.5f;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.SlowMo(timeDuration, setSlowMo, speed);
            Destroy(gameObject);
        }
    }
}
