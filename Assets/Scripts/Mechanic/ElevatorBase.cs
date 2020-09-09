using UnityEngine;
using System.Collections;

/// Основной скрипт управления лифтами
public class ElevatorBase : MonoBehaviour
{
    public GameObject[] savers = new GameObject[0];
    public float inputStartBlock = 1f;
    private GameUI gameUI;

    public void Awake()
    {
        int l = savers.Length;
        if (l != 0)
        {
            for (int i = 0; i < l; i++)
            {
                if (savers[i] != null)
                {
                    if (savers[i].GetComponent<Saving>())
                    {
                        Saving saving = savers[i].GetComponent<Saving>();
                        saving.isFirst = false;
                        saving.idElevatorBase = i;
                    }
                    else if (savers[i].GetComponent<Elevator>())
                    {
                        savers[i].GetComponent<Elevator>().isFirst = false;
                    }
                }
            }

            int es = PlayerPrefs.GetInt("elevatorsave");
            if (savers[es] != null)
            {
                if (savers[es].GetComponent<Saving>())
                {
                    savers[es].GetComponent<Saving>().isFirst = true;
                }
                else if (savers[es].GetComponent<Elevator>())
                {
                    savers[es].GetComponent<Elevator>().isFirst = true;
                }
            }
            else
            {
                gameUI = GameObject.FindWithTag("Canvas").GetComponent<GameUI>();
                StartCoroutine(BlockEnabled());
                GameObject.Find("TipsInput").GetComponent<TipsGamePlayInput>().active = true;
            }
        }
        else
        {
            gameUI = GameObject.FindWithTag("Canvas").GetComponent<GameUI>();
            gameUI.ChangeisBlocked();
        }
    }
    
    public IEnumerator BlockEnabled()
    {
        yield return new WaitForSeconds(inputStartBlock);
        GameObject block = gameUI.block.gameObject;
        block.SetActive(false);
    }
}
