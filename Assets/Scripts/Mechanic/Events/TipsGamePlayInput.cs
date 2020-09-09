using UnityEngine;

/// Триггер для геймплейных уведомлений
public class TipsGamePlayInput : MonoBehaviour
{
    public int idGamePlayTips = 0;
    public TipsGamePlayInput[] copys;
    public bool active = true;
    private Data d;

    public void SetActive(bool l)
    {
        active = l;
    }

    private void Awake()
    {
        d = GameObject.FindWithTag("MainCamera").GetComponent<Data>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active == true)
        {
            active = false;
            d.SetGamePlayTips(idGamePlayTips);
            if (copys.Length != 0)
            {
                for (int i = 0; i < copys.Length; i++)
                {
                    copys[i].active = false;
                }
            }
        }
    }
}
