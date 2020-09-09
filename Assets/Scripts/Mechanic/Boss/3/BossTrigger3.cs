using UnityEngine;

/// Воспринимание урона
public class BossTrigger3 : MonoBehaviour
{
    public BossManagement3 bs3;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bs3.EndedCoroutine();
        }
    }
}