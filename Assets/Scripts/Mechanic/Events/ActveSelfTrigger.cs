using UnityEngine;

/// Триггер активностей объектов
public class ActveSelfTrigger : MonoBehaviour
{
    public bool oneTime = true;
    public bool activeReverse = false;
    public GameObject[] outputTrue;
    public GameObject[] outputFalse;
    private bool active = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active == true)
        {
            if (oneTime == true)
            {
                active = false;
            }

            if (outputTrue.Length != 0)
            {
                for (int i = 0; i < outputTrue.Length; i++)
                {
                    outputTrue[i].SetActive(!activeReverse);
                }
            }

            if (outputFalse.Length != 0)
            {
                for (int i = 0; i < outputFalse.Length; i++)
                {
                    outputFalse[i].SetActive(activeReverse);
                }
            }
        }
    }
}
