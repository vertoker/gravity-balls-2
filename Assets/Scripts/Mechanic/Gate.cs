using UnityEngine;
using System.Collections;

/// Скрипт для ворот (редко использовал)
public class Gate : MonoBehaviour
{
    [Header("StartSet")]
    public Vector2 gateScale = new Vector2(1, 4);
    public float speed = 0.1f;
    public bool isReverse = false;
    public bool isEnd = true;
    public Vector2 animSetGateScale = new Vector2();
    public Vector2 target = new Vector2();
    [Header("SpriteEditor")]
    public Sprite mainSprite;
    [Header("Assets")]
    public GameObject door1;
    public GameObject door2;
    private IEnumerator fixUpdate;

    private void Start()
    {
        SpriteRenderer ds1 = door1.GetComponent<SpriteRenderer>();
        SpriteRenderer ds2 = door2.GetComponent<SpriteRenderer>();
        ds1.sprite = mainSprite;
        ds2.sprite = mainSprite;
        if (isReverse == false)
        {
            animSetGateScale = target = gateScale;
        }
        fixUpdate = FixUpdate();
        SetGate(animSetGateScale);
    }

    private IEnumerator FixUpdate()///////////////////////////////////////////////////////
    {
        yield return new WaitForSeconds(0.03f);
        if (animSetGateScale != target)
        {
            float s = Time.fixedDeltaTime / 0.03f;
            animSetGateScale = Vector2.MoveTowards(animSetGateScale, target, speed * s);
            SetGate(animSetGateScale);
            StartCoroutine(FixUpdate());
        }
    }

    private void SetGate(Vector2 scale)
    {
        SpriteRenderer ds1 = door1.GetComponent<SpriteRenderer>();
        SpriteRenderer ds2 = door2.GetComponent<SpriteRenderer>();
        Vector2 size = new Vector2(mainSprite.texture.width, mainSprite.texture.height);
        float k = size.x / size.y;
        ds1.size = new Vector2(gateScale.x, scale.y / 2f);
        ds2.size = new Vector2(gateScale.x, scale.y / 2f);

        BoxCollider2D d1 = door1.GetComponent<BoxCollider2D>();
        BoxCollider2D d2 = door2.GetComponent<BoxCollider2D>();
        d1.size = new Vector2(gateScale.x, scale.y / 2f);
        d2.size = new Vector2(gateScale.x, scale.y / 2f);
        
        door1.transform.localScale = new Vector3(1f, 1f, 1f);
        door2.transform.localScale = new Vector3(1f, 1f, 1f);
        door1.transform.localPosition = new Vector3(0f, (gateScale.y / 2f) - (scale.y / 4f), 0f);
        door2.transform.localPosition = new Vector3(0f, -(gateScale.y / 2f) + (scale.y / 4f), 0f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isReverse == false)
            {
                target = Vector2.zero;
            }
            else
            {
                target = gateScale;
            }
            StopCoroutine(fixUpdate);
            fixUpdate = FixUpdate();
            StartCoroutine(fixUpdate);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isEnd == true)
        {
            if (isReverse == false)
            {
                target = gateScale;
            }
            else
            {
                target = Vector2.zero;
            }
            StopCoroutine(fixUpdate);
            fixUpdate = FixUpdate();
            StartCoroutine(fixUpdate);
        }
    }
}
