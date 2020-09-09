using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// Окно уведомлений
public class TipsGamePlay : MonoBehaviour
{
    public Animator anim;
    public Animator saved;
    public GameObject savedObj;
    public Text txt;
    public RectTransform rt;

    private void Start()//-1600 -510
    {
        anim.SetBool("isActive", false);
        txt.text = "";
        //PlayerPrefs.GetString("graphicsquality")
    }

    /// Старт окна
    public void SetActiveTrue(GamePlay input)
    {
        txt.text = input.stringLanguage.GetString();
        if (PlayerPrefs.GetString("graphicsquality") != "low")
        {
            anim.SetBool("isActive", true);
        }
        else
        {
            rt.localPosition = rt.localPosition + new Vector3(1090f, 0f, 0f);
        }
        StartCoroutine(IsSkip(input.timeWatch));
    }

    /// Таймер окончания окна
    private IEnumerator IsSkip(float time)
    {
        yield return new WaitForSeconds(time);
        SetActiveFalse();
    }

    /// Закрытие окна
    public void SetActiveFalse()
    {
        if (PlayerPrefs.GetString("graphicsquality") != "low")
        {
            anim.SetBool("isActive", false);
        }
        else
        {
            rt.localPosition = rt.localPosition - new Vector3(1090f, 0f, 0f);
        }
    }
    /////////////////////////////////////////////////////
    /// Уведомление о сохранении
    public void SetActiveTrueSaved()
    {
        if (PlayerPrefs.GetString("graphicsquality") != "low")
        {
            saved.SetBool("isActive", true);
        }
        else
        {
            saved.enabled = false;
            savedObj.SetActive(true);
            rt.localPosition += new Vector3(190f, 0f, 0f);
        }
        StartCoroutine(IsSkipSaved());
    }

    /// Таймер уведомления
    private IEnumerator IsSkipSaved()
    {
        yield return new WaitForSeconds(1.5f);
        SetActiveFalseSaved();
    }

    /// Закрыть уведомление о сохранении
    public void SetActiveFalseSaved()
    {
        if (PlayerPrefs.GetString("graphicsquality") != "low")
        {
            saved.SetBool("isActive", false);
        }
        else
        {
            rt.localPosition = rt.localPosition - new Vector3(190f, 0f, 0f);
            savedObj.SetActive(false);
            saved.enabled = true;
        }
    }
}