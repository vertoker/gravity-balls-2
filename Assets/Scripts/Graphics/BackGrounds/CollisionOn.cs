using UnityEngine;

/// Активация при активации collision
public class CollisionOn : MonoBehaviour
{
    public Sprite spriteOff;
    public Sprite spriteOn;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            sr.sprite = spriteOn;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            sr.sprite = spriteOff;
        }
    }
}