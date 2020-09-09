using UnityEngine;

/// Бустер модификации бессмертия
public class Immortality : MonoBehaviour
{
    [Range(0f, 100f)]
    public float setSecImmortality = 3f;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.Immortality(setSecImmortality);
            Destroy(gameObject);
        }
    }
}
