using UnityEngine;

/// Ещё один гениальный физический объект
public class ProgressKillBar : MonoBehaviour
{
    public GameObject bar;
    public bool x = true;
    public bool y = false;
    private Transform tr;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Management m;

    private void Awake()
    {
        tr = bar.transform;
        sr = bar.GetComponent<SpriteRenderer>();
        bc = bar.GetComponent<BoxCollider2D>();
        m = GameObject.FindWithTag("MainCamera").GetComponent<Management>();
    }

    private void Update()
    {
        Vector2 v = m.pkb;
        if (x)
        {
            sr.size = new Vector2(v.x, sr.size.y);
            bc.size = new Vector2(v.x, bc.size.y);
            tr.localPosition = new Vector3(v.y, 0f, 0f);
        }
        if (y)
        {
            sr.size = new Vector2(sr.size.x, v.x);
            bc.size = new Vector2(bc.size.x, v.x);
            tr.localPosition = new Vector3(tr.localPosition.x, v.y, 0f);
        }
    }
}
