using UnityEngine;

/// Триггер заморозки объектов
public class ActveSelfTriggerIsKinematic : MonoBehaviour
{
    public bool oneTime = true;
    public bool activeAct = true;
    public Rigidbody2D[] output;
    private bool active = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active)
        {
            if (oneTime == true)
            {
                active = false;
            }

            if (activeAct)
            {
                for (int i = 0; i < output.Length; i++)
                {
                    output[i].constraints = RigidbodyConstraints2D.FreezePosition;
                }
            }
            else
            {
                for (int i = 0; i < output.Length; i++)
                {
                    output[i].constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }
}
