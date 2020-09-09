using UnityEngine;

/// Триггер для больших давящих стен
public class TrampTrigger : MonoBehaviour
{
    public TrampAnim trampAnim;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trampAnim.active = true;
        }
    }
}