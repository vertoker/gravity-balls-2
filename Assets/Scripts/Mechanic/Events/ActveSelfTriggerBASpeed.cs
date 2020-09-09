using UnityEngine;

/// Триггер для скрипта базовой анимации
public class ActveSelfTriggerBASpeed : MonoBehaviour
{
    public float speedMod = 0f;
    public bool isStart = true;
    public bool oneTime = false;
    public bool reverse = false;
    public BasicAnimation[] output;
    private bool active = true;

    public void Start()
    {
        if (isStart)
        {
            for (int i = 0; i < output.Length; i++)
            {
                output[i].SetPos(!reverse, speedMod);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active == true)
        {
            if (oneTime == true)
            {
                active = false;
            }

            for (int i = 0; i < output.Length; i++)
            {
                output[i].SetPos(reverse, speedMod);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active == true)
        {
            if (oneTime == true)
            {
                active = false;
            }

            for (int i = 0; i < output.Length; i++)
            {
                output[i].SetPos(!reverse, speedMod);
            }
        }
    }
}
