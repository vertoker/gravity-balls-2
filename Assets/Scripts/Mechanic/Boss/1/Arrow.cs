using UnityEngine;
/// Указатель на босса
public class Arrow : MonoBehaviour
{
    public Transform tr;
    public Transform pl;
    public Transform boss;
    public SpriteRenderer sr;
    public TrailRenderer trRen;
    public HealthBar hb;
    public GameObject obj;
    public Vector2 border;
    public bool isActive = false;
    private bool isAnimMove = true;
    private bool isHighLevel = true;
    private float speed = 0.75f;
    private float speedAngle = 3f;
    private float angle = 0f;

    public void Start()
    {
        obj.SetActive(PlayerPrefs.GetString("boss1") == "life");
        isHighLevel = PlayerPrefs.GetString("graphicsquality") == "high";
        isAnimMove = PlayerPrefs.GetString("graphicsquality") != "low";
        trRen.enabled = isHighLevel;
    }

    /// Поворот стрелы
    public void Update()
    {
        if (isAnimMove)
        {
            Vector2 a = boss.position - tr.position;
            angle = Mathf.MoveTowardsAngle(angle, Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg - 45f, speedAngle);
            tr.localEulerAngles = new Vector3(0f, 0f, angle);
            Color c = new Color(0f, 0f, 0f, hb.healthBarImage.fillAmount);
            sr.color = c;
            if (isHighLevel)
            {
                trRen.startColor = c;
            }
        }
        else
        {
            Vector2 a = boss.position - tr.position;
            tr.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg - 45f);
        }
    }

    /// Перемещение стрелы
    public void FixedUpdate()
    {
        if (isActive)
        {
            if (isAnimMove)
            {
                Vector2 pos = Vector2.MoveTowards(tr.position, pl.position, speed);
                if (pos.x < -border.x) { pos.x = -border.x; }
                else if (pos.x > border.x) { pos.x = border.x; }
                if (pos.y < -border.y) { pos.y = -border.y; }
                else if (pos.y > border.y) { pos.y = border.y; }
                tr.position = pos;
            }
            else
            {
                tr.position = pl.position;
            }
        }
    }
}