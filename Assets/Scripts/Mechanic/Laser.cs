using UnityEngine;

/// Скрипт для управления лазеров (не имеет режима статики)
public class Laser : MonoBehaviour
{
    public Vector2 vector2;
    public bool active = true;
    public GameObject laserActive;
    public GameObject effect;
    public ParticleSystem laserEffect;
    public LineRenderer lr1;
    public Transform tr;
    public BoxCollider2D bcl;
    public Damage dmg;
    private bool isHigh = false;
    private Transform trEffect;

    private void Start()
    {
        lr1.startColor = lr1.endColor = LaserColor();
        isHigh = PlayerPrefs.GetString("graphicsquality") == "high";
        if (!isHigh) { return; }
        laserEffect.startColor = lr1.startColor;
        effect.SetActive(isHigh);
        trEffect = effect.transform;
    }

    public Color LaserColor()
    {
        Color c = new Color(0f, 0f, 0f, 1f);
        switch (dmg.GetTypeLaser().Type2int())
        {
            case 1:
                c = new Color(1f, 0f, 0f, 1f);
                break;
            case 2:
                c = new Color(0f, 1f, 0f, 1f);
                break;
            case 3:
                c = new Color(0f, 0f, 0f, 0.4901961f);
                break;
            case 4:
                c = new Color(1f, 0.8823529f, 0f, 1f);
                break;
            case 5:
                c = new Color(0.6078432f, 0.8823529f, 0f, 1f);
                break;
            case 6:
                c = new Color(1f, 0.2745098f, 0f, 1f);
                break;
        }
        return c;
    }

    private void Update()
    {
        LaserUpdate();
    }

    private void LaserUpdate()
    {
        if (active)
        {
            Vector2[] act1 = Points(tr.position, -tr.up);
            lr1.SetPosition(0, act1[0]);
            lr1.SetPosition(1, act1[1]);
            bcl.size = new Vector2(0.1f, 0.1f);
            bcl.offset = act1[2];
            
            if (isHigh)
            {
                trEffect.position = new Vector3(act1[1].x, act1[1].y, -0.5f);
            }
        }

        return;
    }

    private Vector2[] Points(Vector2 start, Vector2 end)
    {
        Vector2[] ret = new Vector2[3];
        RaycastHit2D hit = Physics2D.Raycast(tr.position, -tr.up, 200);
        ret[0] = tr.position;
        ret[1] = hit.point;
        vector2 = ret[1];
        float distance = Vector2.Distance(tr.position, hit.point);
        bcl.size = new Vector2(0.1f, 0.1f);
        if (hit.collider == bcl)
        {
            ret[2] = new Vector2(0, 0.5f);
        }
        else
        {
            ret[2] = new Vector2(0, -distance - 0.2f);
        }
        return ret;
    }
}
