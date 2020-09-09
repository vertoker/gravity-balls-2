using UnityEngine;

/// Триггер для активности лазеров
public class LaserTrigger : MonoBehaviour
{
    public bool makeIsActive = true;
    public bool oneTime = true;
    public Laser[] outputs;
    private bool active = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && outputs.Length != 0 && active == true)
        {
            if (oneTime == true)
            {
                active = false;
            }

            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i].active = makeIsActive;
                outputs[i].lr1.enabled = makeIsActive;
            }
        }
    }
}
