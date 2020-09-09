using UnityEngine;

/// Анимация для больших давящих стен (перманентное оружие убийство игрока)
public class TrampAnim : MonoBehaviour
{
    public float speed = 0.1f;
    public float speedOffset = 0.01f;
    public float damage = 1f;
    private float sc;
    private float maxDis;
    public Vector3 start;
    public Vector3 end;
    public TrampAnim ender;
    public bool active = true;
    public bool trigPlayer = false;
    private AudioSet audioSet;
    private bool audioAct;
    private Transform tr;
    private HealthBar hb;
    public int count = 0;

    public void Start()
    {
        if (active)
        {
            tr = transform;
            maxDis = Vector2.Distance(start, end);
            sc = Vector2.Distance(tr.localPosition, start) / maxDis;
            hb = Camera.main.GetComponent<Management>().healthBar;

            audioAct = GetComponent<AudioSet>();
            if (audioAct) { audioSet = GetComponent<AudioSet>(); }
        }
    }

    public void Update()
    {
        if (active)
        {
            float s = Time.fixedDeltaTime / 0.03f * (Time.deltaTime / 0.03f);
            if (count == 0)
            {
                tr.localPosition = Vector2.MoveTowards(tr.localPosition, end, (speed * sc + speedOffset) * s);
                if (tr.localPosition == end)
                {
                    count = 1;
                    if (trigPlayer && ender.trigPlayer)
                    {
                        hb.Damage(100f, tag, Vector2.zero);
                    }
                    if (audioAct) { audioSet.SetMusic(); }
                }
            }
            else
            {
                tr.localPosition = Vector2.MoveTowards(tr.localPosition, start, (speed * sc + speedOffset) * s);
                if (tr.localPosition == start)
                {
                    count = 0;
                }
            }
            sc = Vector2.Distance(tr.localPosition, start) / maxDis;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Transform trans = collision.transform;
        string tag = trans.tag;
        if (tag == "Player")
        {
            trigPlayer = true;
        }
        else if (active == false)
        {
            if (trans.GetComponent<PhysicsEmulation>())
            {
                trans.GetComponent<PhysicsEmulation>().TrampAnimPhysicsEmulation(GetComponent<TrampAnim>());
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        string tag = collision.transform.tag;
        if (tag == "Player")
        {
            trigPlayer = false;
        }
    }
}
