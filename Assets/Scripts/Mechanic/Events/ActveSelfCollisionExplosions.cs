using UnityEngine;

/// Коллайдер взрывов
public class ActveSelfCollisionExplosions : MonoBehaviour
{
    public float healthTarget = 0f;
    public bool oneTime = true;
    public bool activeReverse = false;
    public Explosion[] output;
    private bool active = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && active == true)
        {
            if (oneTime == true)
            {
                active = false;
            }

            for (int i = 0; i < output.Length; i++)
            {
                output[i].health = healthTarget;
                output[i].StartCoroutineTimerOffsetExplosion();
            }
        }
    }
}
