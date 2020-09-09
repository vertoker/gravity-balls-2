using UnityEngine;

/// Триггер с активацией PostProcessing (не лучшая моя идея)
public class GraphicsSpecial : MonoBehaviour
{
    public bool isSpecial = true;
    private Management m;

    private void Awake()
    {
        m = GameObject.FindWithTag("MainCamera").GetComponent<Management>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (m.player == collision.gameObject)
        {
            if (isSpecial) { m.SpecialGraphics(); }
            else { m.StartGraphics(); }
        }
    }
}