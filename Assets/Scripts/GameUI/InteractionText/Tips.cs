using System.Collections;
using UnityEngine.UI;
using UnityEngine;

/// Монологовое окно
public class Tips : GlobalFunctions
{
    public Data data;
    public Press2Read p2r;
    public GameUI gameUI;
    public GameObject obj;
    public AudioClip setClip;
    public Text nameText;
    public Text txt;
    private int textID = 0;
    private int textsID = 0;
    private AudioBase audioBase;
    private DialogeString textActive;
    private DialogeString[] textsActive;
    private bool isMass = false;
    [TextArea]
    public string end = "";
    [TextArea]
    public string endPast = "";

    public void Start()
    {
        audioBase = GameObject.FindWithTag("MainCamera").GetComponent<AudioBase>();
        data.scaleTips = 1f;
        obj.SetActive(false);
        txt.text = "";
    }

    /// Активация монолога
    public void SetActiveTrue(DialogeString text, StringLanguageMinimize name)
    {
        data.scaleTips = 0.1f;
        audioBase.layerSounds[0].volume /= 10f;
        obj.SetActive(true);
        nameText.text = name.GetString();
        gameUI.pauseButton.SetActive(false);
        textActive = text;
        isMass = false;
        StartCoroutine(TimerFalse());
    }

    /// Активация монолога (для массива данных)
    public void SetActiveTrue(DialogeString[] texts, StringLanguageMinimize name)
    {
        data.scaleTips = 0.1f;
        audioBase.layerSounds[0].volume /= 10f;
        obj.SetActive(true);
        nameText.text = name.GetString();
        gameUI.pauseButton.SetActive(false);
        textsActive = texts;
        isMass = true;
        StartCoroutine(TimersFalse());
    }

    /// Анимация с печатанием текста
    public IEnumerator TimerFalse(float time = 0.02f)
    {
        yield return new WaitForSecondsRealtime(time);
        string ds = textActive.dialogeString.GetString();
        if (textID < ds.Length && ds != end)
        {
            audioBase.SetSound(setClip, 1, 0.5f, TypePlaying.Sound, false);
            end = end + ds.Substring(textID, 1);
            txt.text = endPast + end;
            textID = textID + 1;

            if (textID + 1 != ds.Length && ds != end)
            {
                if (ds.Substring(textID + 1, 1) == ",")
                {
                    StartCoroutine(TimerFalse(0.1f));
                }
                else if (ds.Substring(textID + 1, 1) == ".")
                {
                    StartCoroutine(TimerFalse(0.15f));
                }
                else if (ds.Substring(textID + 1, 1) == "?")
                {
                    StartCoroutine(TimerFalse(0.15f));
                }
                else if (ds.Substring(textID + 1, 1) == ".")
                {
                    StartCoroutine(TimerFalse(0.15f));
                }
                else
                {
                    StartCoroutine(TimerFalse());
                }
            }
            else
            {
                StartCoroutine(TimerFalse());
            }
        }
        else
        {
            endPast = txt.text;
            if (textActive.isSkip)
            {
                if (textActive.skipOffset == 0f)
                {
                    SetActiveFalse();
                }
                else
                {
                    IsSkip(textActive.skipOffset);
                }
            }
        }
    }

    /// Анимация с печатанием текста (для массива данных)
    public IEnumerator TimersFalse(float time = 0.02f)
    {
        yield return new WaitForSecondsRealtime(time);
        string ds = textsActive[textsID].dialogeString.GetString();
        if (textID < ds.Length && ds != end)
        {
            audioBase.SetSound(setClip, 1, 0.5f, TypePlaying.Sound, false);
            end = end + ds.Substring(textID, 1);
            txt.text = endPast + end;
            textID = textID + 1;

            string ds1 = textsActive[textsID].dialogeString.GetString();
            if (textID + 1 != ds1.Length && ds1 != end)
            {
                if (ds1.Substring(textID + 1, 1) == ",")
                {
                    StartCoroutine(TimersFalse(0.1f));
                }
                else if (ds1.Substring(textID + 1, 1) == ".")
                {
                    StartCoroutine(TimersFalse(0.15f));
                }
                else if (ds1.Substring(textID + 1, 1) == "?")
                {
                    StartCoroutine(TimersFalse(0.15f));
                }
                else if (ds1.Substring(textID + 1, 1) == "!")
                {
                    StartCoroutine(TimersFalse(0.15f));
                }
                else
                {
                    StartCoroutine(TimersFalse());
                }
            }
            else
            {
                StartCoroutine(TimersFalse());
            }
        }
        else
        {
            endPast = txt.text;
            if (textsActive[textsID].isSkip)
            {
                if (textsActive[textsID].skipOffset == 0f)
                {
                    SetActiveFalse();
                }
                else
                {
                    IsSkip(textsActive[textsID].skipOffset);
                }
            }
        }
    }

    /// Отключение анимации текста
    public IEnumerator IsSkip(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        SetActiveFalse();
    }

    /// Отключение от монологового окна
    public void SetFalse()
    {
        obj.SetActive(false);
        gameUI.pauseButton.SetActive(true);
        end = "";
        endPast = "";
        txt.text = "";
        textID = textsID = 0;
        data.scaleTips = 1f;
        audioBase.layerSounds[0].volume *= 10f;
    }

    /// Переход на следующий текст или отключение монологового окна
    public void SetActiveFalse()
    {
        /// Если активность массива
        if (isMass == false)
        {
            /// Если анимация не окончена
            if (textActive.dialogeString.GetString() != end)
            {
                end = textActive.dialogeString.GetString();
                if (textActive.isSkip)
                {
                    SetActiveFalse();
                }
            }
            else
            {
                obj.SetActive(false);
                gameUI.pauseButton.SetActive(true);
                end = "";
                data.scaleTips = 1f;
                audioBase.layerSounds[0].volume *= 10f;
            }
        }
        else
        {
            /// Если анимация не окончена
            if (textsActive[textsID].dialogeString.GetString() != end)
            {
                /// Если необходимо не стирать прошлый текст
                if (textsActive[textsID].isStep == true)
                {
                    txt.text = end = textsActive[textsID].dialogeString.GetString();
                    if (textsActive[textsID].isSkip)
                    {
                        SetActiveFalse();
                    }
                }
                else
                {
                    end = textsActive[textsID].dialogeString.GetString();
                    txt.text = endPast + end;
                }
            }
            else
            {
                /// Если в массиве остался невоспроизведённый текст
                if (textsID != textsActive.Length - 1)
                {
                    textsID += 1;
                    textID = 0;
                    end = "";
                    /// Если необходимо не стирать прошлый текст
                    if (textsActive[textsID].isStep == true)
                    {
                        endPast = "";
                    }
                    StartCoroutine(TimersFalse());
                }
                else
                {
                    obj.SetActive(false);
                    gameUI.pauseButton.SetActive(true);
                    p2r.UnTap();
                    end = "";
                    endPast = "";
                    txt.text = "";
                    textID = textsID = 0;
                    data.scaleTips = 1f;
                    audioBase.layerSounds[0].volume *= 10f;
                }
            }
        }
    }
}