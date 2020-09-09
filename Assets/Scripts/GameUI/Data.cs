using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// Хранит основные классы и данные
public class Data : GlobalFunctions
{
    public Dialoge[] dialoges;/// Монологи
    public DeadPhrases[] deadPhrases;/// Фразы после смерти
    public GamePlay[] gameplay;/// Геймплейное окно уведомлений
    [Space]
    public Tips tips;
    public AudioBase audioBase;
    public TipsGamePlay gamePlayTips;
    public Image slowmobonus;
    public Text fpsText;

    /// Менеджмент времени
    public float scaleTips = 1f;
    public float scaleGameUI = 1f;
    public float scaleSlowMo = 1f;
    private float speed;
    private float target;
    private float timeDuration;
    private int updFPS = 0;

    public void Awake()
    {
        scaleTips = scaleGameUI = scaleSlowMo = 1f;
        slowmobonus.color = new Color(0f, 0f, 0f, 0f);
    }

    public void Start()
    {
        StartCoroutine(SecFPSUpdate());
    }

    /// Активация монолога
    public void SetDialoge(int id)
    {
        if (dialoges.Length != 0)
        {
            tips.SetActiveTrue(dialoges[id].dialogeStrings, dialoges[id].name);
        }
    }

    /// Вроде принудительное отключение монолога
    public void FalseP2R()
    {
        tips.SetFalse();
    }

    /// Выдача посмертной фразы
    public string GetDeadPhrase(string typeDead)
    {
        int idType = -1;
        for (int i = 0; i < deadPhrases.Length; i++)
        {
            if (deadPhrases[i].typeDead == typeDead)
            {
                idType = i; break;
            }
        }

        if (idType == -1)
        {
            return typeDead;
        }

        int rand = Random.Range(0, deadPhrases[idType].deadPhrases.Length);
        return deadPhrases[idType].deadPhrases[rand].GetString();
    }

    /// Выдача фразы-пояснение
    public string GetDeadPhrase2()
    {
        string ret = "";
        switch (PlayerPrefs.GetString("language"))
        {
            case "english": ret = "Tap to continue"; break;
            case "spanish": ret = "Pulse para continuar"; break;
            case "italian": ret = "Tocca per continuare"; break;
            case "german": ret = "Tippen Sie, um fortzufahren"; break;
            case "russian": ret = "Нажмите для продолжения"; break;
            case "french": ret = "Appuyez sur pour continuer"; break;
            case "portuguese": ret = "Clique para continuar"; break;
            case "korean": ret = "계속하려면 탭하세요"; break;
            case "chinese": ret = "点按即可继续"; break;
            case "japan": ret = "タップして続行します"; break;
        }
        return ret;
    }

    /// Метод паузы
    public void PauseGameUI(float time)
    {
        scaleGameUI = time;
        Update();
        audioBase.UpdateSound();
    }

    /// Активация геймплейного уведомления
    public void SetGamePlayTips(int id)
    {
        if (id == -1) { gamePlayTips.SetActiveTrueSaved(); }
        else { gamePlayTips.SetActiveTrue(gameplay[id]); }
    }

    /// Активация слоу-мо
    public void SlowMo(float timeDuration2, float setSlowMo, float speed2)
    {
        speed = speed2;
        target = setSlowMo;
        timeDuration = timeDuration2;
        Update();
        audioBase.UpdateSound();
    }

    /// Другая активация слоу-мо
    public void SlowMo(float timeDuration2)
    {
        scaleSlowMo = 0.1f;
        float sb = (1f - scaleSlowMo) * 0.3921569f;
        slowmobonus.color = new Color(0f, 0f, 0f, sb);
        Update();
        audioBase.UpdateSound();
    }

    /// Таймер слоу-мо
    public IEnumerator EndAnim(float timeDuration)
    {
        yield return new WaitForSeconds(timeDuration);
        End();
    }

    /// Окончить слоу-мо
    public void End()
    {
        scaleSlowMo = 1f;
        float sb = (1f - scaleSlowMo) * 0.3921569f;
        slowmobonus.color = new Color(0f, 0f, 0f, sb);
        Update();
        audioBase.UpdateSound();
    }

    /// Другое окончание слоу-мо
    public void End2(float timeDuration2)
    {
        if (timeDuration2 == 0) { End(); return; }
        StartCoroutine(EndAnim(timeDuration2));
    }

    private void Update()
    {
        Time.timeScale = scaleTips * scaleSlowMo * scaleGameUI;/// Скорость течения времени
        Time.fixedDeltaTime = 0.03f * scaleSlowMo * scaleTips;/// Физика
        updFPS++;
        return;
    }

    /// Запись значения fps для паузы
    private IEnumerator SecFPSUpdate()
    {
        yield return new WaitForSeconds(1f);
        fpsText.text = "FPS: " + updFPS; updFPS = 0;
        StartCoroutine(SecFPSUpdate());
    }
}

/// Короткая строка в монологе (не закончено)
[System.Serializable]
public class Dialoge
{
    public StringLanguageMinimize name;
    public DialogeString[] dialogeStrings;
}

/// Полноценная строка в монологе
[System.Serializable]
public class DialogeString
{
    public StringLanguage dialogeString;
    public bool isSkip = false;
    public float skipOffset = 0f;
    public bool isStep = false;
}

/// Фразы при смерти игрока
[System.Serializable]
public class DeadPhrases
{
    public string typeDead = "Saw";
    public StringLanguage[] deadPhrases;
}

/// Строка уведомлений
[System.Serializable]
public class GamePlay
{
    public StringLanguage stringLanguage;
    public float timeWatch = 3f;
}

/// Локализуемая строка
[System.Serializable]
public class StringLanguage
{
    [TextArea]
    public string english = "";
    [TextArea]
    public string spanish = "";
    [TextArea]
    public string italian = "";
    [TextArea]
    public string german = "";
    [TextArea]
    public string russian = "";
    [TextArea]
    public string french = "";
    [TextArea]
    public string portuguese = "";
    [TextArea]
    public string korean = "";
    [TextArea]
    public string chinese = "";
    [TextArea]
    public string japan = "";
    /// Выдача локализованной строки
    public string GetString()
    {
        string ret = english;
        switch (PlayerPrefs.GetString("language"))
        {
            //case "english": ret = english; break;
            case "spanish": ret = spanish; break;
            case "italian": ret = italian; break;
            case "german": ret = german; break;
            case "russian": ret = russian; break;
            case "french": ret = french; break;
            case "portuguese": ret = portuguese; break;
            case "korean": ret = korean; break;
            case "chinese": ret = chinese; break;
            case "japan": ret = japan; break;
        }
        return ret;
    }
}

/// Короткая локализуемая строка
[System.Serializable]
public class StringLanguageMinimize
{
    public string english = "";
    public string spanish = "";
    public string italian = "";
    public string german = "";
    public string russian = "";
    public string french = "";
    public string portuguese = "";
    public string korean = "";
    public string chinese = "";
    public string japan = "";

    /// Выдача локализованной строки
    public string GetString()
    {
        string ret = english;
        switch (PlayerPrefs.GetString("language"))
        {
            //case "english": ret = english; break;
            case "spanish": ret = spanish; break;
            case "italian": ret = italian; break;
            case "german": ret = german; break;
            case "russian": ret = russian; break;
            case "french": ret = french; break;
            case "portuguese": ret = portuguese; break;
            case "korean": ret = korean; break;
            case "chinese": ret = chinese; break;
            case "japan": ret = japan; break;
        }
        return ret;
    }
}