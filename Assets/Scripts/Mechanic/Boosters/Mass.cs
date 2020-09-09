using UnityEngine;

/// Бустер модификации массы
public class Mass : MonoBehaviour
{
    public bool isSet = true;
    [Range(0.01f, 100f)]
    public float speed = 0.5f;
    [Range(0.01f, 100f)]
    public float setMass = 2f;
    [Range(-100f, 100f)]
    public float plusMass = 0f;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.Mass(isSet, setMass, plusMass, speed);
            Destroy(gameObject);
        }
    }
}
