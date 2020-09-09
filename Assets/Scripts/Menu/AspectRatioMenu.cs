using UnityEngine;
using UnityEngine.UI;

/// Правильное расположение UI при разных экранах (меню)
public class AspectRatioMenu : MonoBehaviour
{
    public Camera cam;
    [Header("Start")]
    public RectTransform settingsBut;
    [Header("Settings")]
    public RectTransform text;
    public RectTransform languageText;
    public RectTransform sound;
    public RectTransform music;
    public RectTransform senrot;
    public Scrollbar soundSB;
    public Scrollbar musicSB;
    public Scrollbar senrotSB;
    public RectTransform languageBut;
    public RectTransform authorsBut;
    public RectTransform graphicsBut;
    public RectTransform consoleBut;
    [Header("Authors")]
    public RectTransform backButAuthors;
    [Header("Console")]
    public RectTransform backButConsole;

    public void Awake()
    {
        float ratio = cam.aspect; float ratioBase = 16f / 9f;
        Vector2 vx = new Vector2(ratioBase - ratio, ratio - ratioBase);

        text.localPosition += new Vector3(vx.x * 540f, 0, 0);
        settingsBut.localPosition += new Vector3(vx.y * 540f, 0, 0);

        sound.localPosition += new Vector3(vx.x * 540f, 0, 0);
        music.localPosition += new Vector3(vx.x * 540f, 0, 0);
        senrot.localPosition += new Vector3(vx.x * 540f, 0, 0);
        soundSB.GetComponent<RectTransform>().sizeDelta += new Vector2(vx.y * 1080f, 0);
        musicSB.GetComponent<RectTransform>().sizeDelta += new Vector2(vx.y * 1080f, 0);
        senrotSB.GetComponent<RectTransform>().sizeDelta += new Vector2(vx.y * 1080f, 0);

        graphicsBut.localPosition += new Vector3(vx.x * 292.5f, 0, 0);//65
        graphicsBut.sizeDelta += new Vector2(vx.y * 585f, 0);

        authorsBut.localPosition += new Vector3(vx.x * 337.5f, 0, 0);//75
        authorsBut.sizeDelta += new Vector2(vx.y * 675f, 0);

        consoleBut.localPosition += new Vector3(vx.y * 270f, 0, 0);//60
        consoleBut.sizeDelta += new Vector2(vx.y * 540f, 0);

        languageBut.localPosition += new Vector3(vx.y * 270f, 0, 0);//60
        languageBut.sizeDelta += new Vector2(vx.y * 540f, 0);

        Vector2 v = new Vector2(languageBut.localPosition.x, languageText.localPosition.y);
        languageText.localPosition = v;

        backButAuthors.localPosition += new Vector3(vx.x * 270f, 0, 0);//60
        backButAuthors.sizeDelta += new Vector2(vx.y * 540f, 0);
        backButConsole.localPosition += new Vector3(vx.x * 270f, 0, 0);//60
        backButConsole.sizeDelta += new Vector2(vx.y * 540f, 0);
    }
}
