using UnityEngine;

/// Скрипт из первой игры для 0 уровня
public class Star : MonoBehaviour
{
    public GameObject effect;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
