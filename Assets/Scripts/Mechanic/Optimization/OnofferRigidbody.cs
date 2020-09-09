using UnityEngine;

/// Триггер для оптимизации объектов с физикой
public class OnofferRigidbody : MonoBehaviour
{
    public BoxCollider2D[] on = new BoxCollider2D[0];
    public BoxCollider2D[] off = new BoxCollider2D[0];
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        int offL = off.Length;
        int onL = on.Length;
        if (offL != 0)
        {
            for (int i = 0; i < offL; i++)
            {
                off[i].enabled = false;
            }
        }
        if (onL != 0)
        {
            for (int i = 0; i < onL; i++)
            {
                on[i].enabled = true;
            }
        }
    }
}