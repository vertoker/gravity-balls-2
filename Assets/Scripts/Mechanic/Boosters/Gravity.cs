using UnityEngine;

/// Бустер модификации гравитации
public class Gravity : MonoBehaviour
{
    public bool isSet = true;
    [Range(-100f, 100f)]
    public float speed = 0.5f;
    [Range(-100f, 100f)]
    public float setGravity = 5f;
    [Range(-200f, 200f)]
    public float plusGravity = 0f;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.Gravity(isSet, setGravity, plusGravity, speed);
            Destroy(gameObject);
        }
    }
}
