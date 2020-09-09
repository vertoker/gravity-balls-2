using System.Collections;
using UnityEngine;

/// Изменение цвета
public class ColorChange : MonoBehaviour
{
    public float timer = 1f;
    public SpriteRenderer sr;

    private void Awake()
    {
        float r = Random.Range(0.1f, 1f);
        float g = Random.Range(0.1f, 1f);
        float b = Random.Range(0.1f, 1f);
        sr.color = new Color(r, g, b, 1f);
    }

    private void Start()
    {
        StartCoroutine(Color());
    }

    private IEnumerator Color()
    {
        yield return new WaitForSeconds(timer);
        float r = Random.Range(0.1f, 1f);
        float g = Random.Range(0.1f, 1f);
        float b = Random.Range(0.1f, 1f);
        sr.color = new Color(r, g, b, 1f);
        StartCoroutine(Color());
    }
}