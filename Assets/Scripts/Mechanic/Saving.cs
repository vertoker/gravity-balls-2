using System.Collections;
using UnityEngine;

/// Триггер для сохранения игры
public class Saving : MonoBehaviour
{
    public Saving[] savings;
    public Vector2 startPos;
    public float startRot;
    public bool isActive = true;
    public bool isFirst = true;
    public int idElevatorBase = 0;
    public TipsGamePlayInput tgpi;
    private GameObject player;
    private GameObject cam;
    private Transform trp;
    private GameUI gameui;
    private Management m;

    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        m = cam.GetComponent<Management>();
        gameui = GameObject.FindWithTag("Canvas").GetComponent<GameUI>();
        player = m.player;
        trp = player.GetComponent<Transform>();

        if (isFirst)
        {
            trp.position = startPos;
            m.Set(startRot);
            OfferSaves();
        }
        isActive = !isFirst;
        tgpi.SetActive(!isFirst);
        StartCoroutine(BlockFalse());
    }

    public IEnumerator BlockFalse()
    {
        yield return new WaitForSeconds(1f);
        gameui.block.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive == true)
        {
            isActive = false;
            PlayerPrefs.SetInt("elevatorsave", idElevatorBase);
            OfferSaves();
        }
    }

    public void OfferSaves()
    {
        if (savings.Length != 0)
        {
            for (int i = 0; i < savings.Length; i++)
            {
                savings[i].isActive = false;
                savings[i].tgpi.SetActive(false);
            }
        }
    }
}
