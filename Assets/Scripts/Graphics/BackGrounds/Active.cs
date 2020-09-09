using System.Collections;
using UnityEngine;

/// Переодическая активность UI (для массива объектов)
public class Active : MonoBehaviour
{
    public float timeOffset = 0.1f;
    public Sprite spriteOn;
    public Sprite spriteOff;
    public Active[] outputs = new Active[0];
    private bool activeNow = false;
    private bool initiatorActive = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        initiatorActive = PlayerPrefs.GetString("graphicsquality") == "high";
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteOff;
        activeNow = false;
    }

    public void On()
    {
        if (activeNow == false && initiatorActive == true)
        {
            activeNow = true;
            spriteRenderer.sprite = spriteOn;
            StartCoroutine(NextActivator());
        }
    }

    public void Off()
    {
        if (activeNow == true && initiatorActive == true)
        {
            activeNow = false;
            spriteRenderer.sprite = spriteOff;
            StartCoroutine(NextActivator());
        }
    }

    private IEnumerator NextActivator()
    {
        yield return new WaitForSeconds(timeOffset);
        if (outputs.Length != 0)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                if (activeNow == true)
                {
                    outputs[i].On();
                }
                else
                {
                    outputs[i].Off();
                }
            }
        }
    }
}