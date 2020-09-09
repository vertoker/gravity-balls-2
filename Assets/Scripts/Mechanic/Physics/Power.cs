using UnityEngine;

/// Сохранение силы объекта при ударе об объект (игрока) (почти что баг)
public class Power : MonoBehaviour
{
    public float power;
    public Vector2 velocity;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;
        power = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
    }
}