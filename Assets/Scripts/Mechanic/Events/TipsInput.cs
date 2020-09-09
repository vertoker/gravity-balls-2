using UnityEngine;
/// Триггер для активации монолога
public class TipsInput : MonoBehaviour
{
    public int idTips = 0;
    public bool isPress2Read = true;
    public bool oneTime = true;
    private bool active = true;
    public GameObject[] copys;
    private Data data;
    private Press2Read p2r;
    private TipsInput ti;

    private void Awake()
    {
        data = GameObject.FindWithTag("MainCamera").GetComponent<Data>();
        p2r = GameObject.FindWithTag("Press2Read").GetComponent<Press2Read>();
        ti = GetComponent<TipsInput>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (isPress2Read == false && active == true)
            {
                Disable();
                data.SetDialoge(idTips);

                if (copys.Length != 0)
                {
                    for (int i = 0; i < copys.Length; i++)
                    {
                        copys[i].GetComponent<TipsInput>().Disable();
                    }
                }
            }
            else if (isPress2Read == true)
            {
                p2r.Active(ti);
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (isPress2Read == true)
        {
            p2r.DeActive();
        }
    }

    public void Disable()
    {
        if (oneTime == true)
        {
            active = false;
        }
        return;
    }
}