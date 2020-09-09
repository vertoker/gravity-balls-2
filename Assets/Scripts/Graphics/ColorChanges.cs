using System.Collections;
using UnityEngine;

/// Изменение цвета (для массива объектов)
public class ColorChanges : MonoBehaviour
{
    public float time = 0.01f;
    public SpriteRenderer[] srs = new SpriteRenderer[0];
    private int length, lengthM1;

    private void Awake()
    {
        length = srs.Length;
        lengthM1 = length - 1;
        for (int i = 0; i < length; i++)
        {
            srs[i].color = LightSpecial();
        }
    }

    private void Start()
    {
        StartCoroutine(Color());
    }

    private IEnumerator Color()
    {
        yield return new WaitForSeconds(time);
        int id = Random.Range(0, length);
        srs[id].color = LightSpecial();
        srs[lengthM1 - id].color = LightSpecial();
        StartCoroutine(Color());
    }

    public Color LightSpecial()
    {
        Color ret = new Color();
        int rid = Random.Range(1, 4);
        switch (rid)
        {
            case 1:
                float r1 = 1f;
                float g1 = Random.Range(0.4f, 1f);
                float b1 = Random.Range(0.4f, 1f);
                ret = new Color(r1, g1, b1);
                break;
            case 2:
                float r2 = Random.Range(0.4f, 1f);
                float g2 = 1f;
                float b2 = Random.Range(0.4f, 1f);
                ret = new Color(r2, g2, b2);
                break;
            case 3:
                float r3 = Random.Range(0.4f, 1f);
                float g3 = Random.Range(0.4f, 1f);
                float b3 = 1f;
                ret = new Color(r3, g3, b3);
                break;
        }
        return ret;
    }
}
