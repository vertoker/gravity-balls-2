using UnityEngine;

/// Активация при активации trigger
public class TriggerOn : MonoBehaviour
{
    public Sprite spriteOff;
    public Sprite spriteOn;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sr.sprite = spriteOn;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sr.sprite = spriteOff;
        }
    }
}