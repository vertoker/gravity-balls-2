using UnityEngine;

/// Триггер 2 босса для лазеров
public class BossTrigger2 : MonoBehaviour
{
    public int id = 0;
    public BossManagement2 bossManagement;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossManagement.TriggerLaserDefect(id);
        }
    }
}