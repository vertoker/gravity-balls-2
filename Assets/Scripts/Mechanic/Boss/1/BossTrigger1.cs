using UnityEngine;

/// Получение урона
public class BossTrigger1 : MonoBehaviour
{
    public BossManagement1 bm;
    private Power power;

    public void Awake()
    {
        power = GameObject.FindWithTag("Player").GetComponent<Power>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            bm.Damage(power.power);
        }
    }
}
