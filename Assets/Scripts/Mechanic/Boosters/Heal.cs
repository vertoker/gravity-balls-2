using UnityEngine;

/// Бустер модификации лечения
public class Heal : MonoBehaviour
{
    public bool isSet = true;
    [Range(0f, 1f)]
    public float setHealth = 1f;
    [Range(-1f, 1f)]
    public float plusHealth = 0f;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.Heal(isSet, setHealth, plusHealth);
            Destroy(gameObject);
        }
    }
}
