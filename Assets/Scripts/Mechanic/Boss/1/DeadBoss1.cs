using UnityEngine;

/// Смерть босса
public class DeadBoss1 : MonoBehaviour
{
    public GameObject objct;
    public GameObject objct2;
    public Explosion expEnd;
    public GameObject explosion;
    public GameObject expSpawn;
    public GameObject[] dops = new GameObject[0];

    public void Start()
    {
        for (int i = 0; i < dops.Length; i++)
        {
            dops[i].SetActive(false);
        }
    }

    public void StartBoss()
    {
        for (int i = 0; i < dops.Length; i++)
        {
            dops[i].SetActive(true);
        }
    }

    /// Смерть босса
    public void Dead(Vector2 pos, bool boom)
    {
        objct.GetComponent<CircleCollider2D>().enabled = false;
        objct2.GetComponent<SpriteRenderer>().enabled = false;
        for (int i = 0; i < dops.Length; i++)
        {
            dops[i].SetActive(false);
        }
        if (!boom) { return; }
        explosion.transform.position = pos;
        explosion.SetActive(true);
        expEnd.StartCoroutineTimerOffsetExplosion();
    }

    /// Взрыв босса
    public void Boom()
    {
        GameObject obj = Instantiate(expSpawn);
        obj.transform.position = new Vector2(-40f, 0f);
        Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();
        Explosion exp = obj.GetComponent<Explosion>();
        obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        rb2d.gravityScale = 0f;
        rb2d.isKinematic = true;
        exp.power = 50f;
        exp.radius = 5f;
        exp.health = 0f;
    }
}
