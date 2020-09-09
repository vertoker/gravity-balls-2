using System.Collections;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// Полноценная концовка игры
public class Ended : MonoBehaviour
{
    public Camera cam;
    public GameObject egg;
    public Animator black;
    public GameObject bad;
    public Animator badAnim;
    public GameObject good;
    public Animator goodAnim;
    public GameObject neutral;
    public Animator neutralAnim;
    public GameObject secret;
    public Animator secretAnim;
    public Text textDialoge;
    public AudioSource music;
    public AudioClip badMusic;
    public AudioClip goodMusic;
    public AudioClip neutralMusic;
    public AudioClip secretMusic;
    public AudioSource textDialogeSound;
    public AudioSource scrollMouseSound;
    public StringLanguage[] badEnd;
    public StringLanguage[] goodEnd;
    public StringLanguage[] neutralEnd;
    public StringLanguage[] secretEnd;
    private StringLanguage[] activeEnd;
    private string targetText = string.Empty;
    private int countTexts = 0;
    private int count = 0;
    private bool isActive = true;
    private bool isSecretAnim = true;

    public void Awake()
    {
        isActive = PlayerPrefs.GetString("end") != "ended";
        PlayerPrefs.SetInt("progress", 35);
        if (isActive)
        {
            float ratio = cam.aspect * 9f - 16f;
            textDialoge.rectTransform.sizeDelta = new Vector2(7680f + ratio * 475f, 4320f);
            if (PlayerPrefs.GetString("boss3") == "life")
            {
                if (PlayerPrefs.GetString("ai") == "on")
                {
                    PlayerPrefs.SetString("end", "good");
                    music.clip = goodMusic;
                    activeEnd = goodEnd;
                }
                else
                {
                    PlayerPrefs.SetString("end", "secret");
                    music.clip = secretMusic;
                    activeEnd = secretEnd;
                }
            }
            else
            {
                if (PlayerPrefs.GetString("ai") == "on")
                {
                    PlayerPrefs.SetString("end", "neutral");
                    music.clip = neutralMusic;
                    activeEnd = neutralEnd;
                }
                else
                {
                    PlayerPrefs.SetString("end", "bad");
                    music.clip = badMusic;
                    activeEnd = badEnd;
                }
            }
        }
        isSecretAnim = PlayerPrefs.GetString("end") == "secret";
    }

    public void Start()
    {
        if (isActive)
        {
            textDialoge.text = string.Empty;
            targetText = activeEnd[countTexts].GetString();
            StartCoroutine(TextAdd(1f));
        }
        else
        {
            StartCoroutine(Secret());
        }
    }

    public void TextAdd()
    {
        if (textDialoge.text != targetText)
        {
            textDialogeSound.Play();
            string symbol = targetText.Substring(count, 1);
            textDialoge.text = textDialoge.text + symbol;
            count = count + 1;

            switch (symbol)
            {
                case " ":
                    StartCoroutine(TextAdd(0.05f));
                    break;
                case ",":
                    StartCoroutine(TextAdd(0.1f));
                    break;
                case ".":
                    StartCoroutine(TextAdd(0.1f));
                    break;
                case "?":
                    StartCoroutine(TextAdd(0.2f));
                    break;
                case "!":
                    StartCoroutine(TextAdd(0.2f));
                    break;
                default:
                    StartCoroutine(TextAdd(0.02f));
                    break;
            }
        }
        else
        {
            count = 0;
            countTexts = countTexts + 1;
            StartCoroutine(TextRemove(2f));
        }
    }

    public IEnumerator Secret()
    {
        yield return new WaitForSeconds(600f);
        egg.SetActive(true);
    }

    public IEnumerator TextAdd(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        if (textDialoge.text != targetText)
        {
            textDialogeSound.Play();
            string symbol = targetText.Substring(count, 1);
            textDialoge.text = textDialoge.text + symbol;
            count = count + 1;

            switch (symbol)
            {
                case " ":
                    StartCoroutine(TextAdd(0.05f));
                    break;
                case ",":
                    StartCoroutine(TextAdd(0.1f));
                    break;
                case ".":
                    StartCoroutine(TextAdd(0.1f));
                    break;
                case "?":
                    StartCoroutine(TextAdd(0.2f));
                    break;
                case "!":
                    StartCoroutine(TextAdd(0.2f));
                    break;
                default:
                    StartCoroutine(TextAdd(0.02f));
                    break;
            }
        }
        else
        {
            count = 0;
            countTexts = countTexts + 1;
            StartCoroutine(TextRemove(targetText.Length / 20f));
        }
    }

    public IEnumerator TextRemove(float time)
    {
        yield return new WaitForSeconds(time);
        if (textDialoge.text != string.Empty)
        {
            scrollMouseSound.Play();
            string symbol = textDialoge.text.Substring(textDialoge.text.Length - 1, 1);
            textDialoge.text = textDialoge.text.Substring(0, textDialoge.text.Length - 1);
            StartCoroutine(TextRemove(0.01f));
        }
        else
        {
            if (countTexts < activeEnd.Length)
            {
                textDialoge.text = string.Empty;
                targetText = activeEnd[countTexts].GetString();
                TextAdd();
                if (isSecretAnim)
                {
                    if (countTexts == activeEnd.Length - 2)
                    {
                        secret.SetActive(true);
                        music.Play();
                        StartCoroutine(ExitGame());
                    }
                }
            }
            else
            {
                switch (PlayerPrefs.GetString("end"))
                {
                    case "bad":
                        bad.SetActive(true);
                        break;
                    case "good":
                        good.SetActive(true);
                        break;
                    case "neutral":
                        neutral.SetActive(true);
                        break;
                }
                music.Play();
                black.SetBool("isActive", true);
                PlayerPrefs.SetString("end", "ended");
            }
        }
    }

    public IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(10f);
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
