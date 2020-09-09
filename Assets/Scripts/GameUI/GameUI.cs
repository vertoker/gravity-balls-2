using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// Игровой UI
public class GameUI : MonoBehaviour
{
    public float falseisActive = 1.5f;
    public StringLanguageMinimize nameScene;
    public Animator animatorBlackScreenGame;
    public Data data;
    public Management m;
    public GameObject pauseButton;
    public GameObject pauseWindow;
    public GameObject loseWindow;
    public GameObject tap2Next;
    public Text tap2NextText;
    public Text tap2NextText2;
    public Image block;
    [Space]
    public new Text name;
    public Text continie;
    public Text repeat, repeat2;
    public Text menu, menu2;
    public Text read;
    public bool isFirstLevel = false;
    private bool isBlocked = true;
    private bool isTouch = false;

    public void ChangeisBlocked()
    {
        isBlocked = false;
    }

    private void Start()
    {
        block.gameObject.SetActive(isBlocked);
        if (isFirstLevel)
        {
            Time.timeScale = 0f;
            data.scaleGameUI = 0f;
        }
        else
        {
            falseisActive = 0.5f;
            data.scaleGameUI = 1f;
        }
        StartCoroutine(FalseisActive());
        pauseButton.SetActive(true);
        pauseWindow.SetActive(false);
        loseWindow.SetActive(false);
        tap2Next.SetActive(false);
        TextStatic();
    }

    /// Анимация угасания чёрного экрана
    public IEnumerator FalseisActive()
    {
        yield return new WaitForSecondsRealtime(falseisActive);
        animatorBlackScreenGame.SetBool("isActive", false);
        data.scaleGameUI = 1f;
    }

    /// Стартовая локализация базовых кнопок в игровом UI
    public void TextStatic()
    {
        name.text = nameScene.GetString();
        switch (PlayerPrefs.GetString("language"))
        {
            case "english":
                continie.text = "Continie";
                repeat.text = "Restart";
                repeat2.text = "Restart";
                menu.text = "Menu";
                menu2.text = "Menu";
                read.text = "<Read>";
                break;
            case "spanish":
                continie.text = "Continuar";
                repeat.text = "Reiniciar";
                repeat2.text = "Reiniciar";
                menu.text = "Menú";
                menu2.text = "Menú";
                read.text = "<Leer>";
                break;
            case "italian":
                continie.text = "Continua";
                repeat.text = "Ricomincia";
                repeat2.text = "Ricomincia";
                menu.text = "Menu";
                menu2.text = "Menu";
                read.text = "<Leggere>";
                break;
            case "german":
                continie.text = "Fortsetzen";
                repeat.text = "Neustart";
                repeat2.text = "Neustart";
                menu.text = "Speisekarte";
                menu2.text = "Speisekarte";
                read.text = "<Lesen>";
                break;
            case "russian":
                continie.text = "Продолжить";
                repeat.text = "Заново";
                repeat2.text = "Заново";
                menu.text = "Меню";
                menu2.text = "Меню";
                read.text = "<Прочитать>";
                break;
            case "french":
                continie.text = "Continuer";
                repeat.text = "Redémarrer";
                repeat2.text = "Redémarrer";
                menu.text = "Menu";
                menu2.text = "Menu";
                read.text = "<Lis>";
                break;
            case "portuguese":
                continie.text = "Continuar";
                repeat.text = "Reiniciar";
                repeat2.text = "Reiniciar";
                menu.text = "Cardápio";
                menu2.text = "Cardápio";
                read.text = "<Ler>";
                break;
            case "korean":
                continie.text = "잇다";
                repeat.text = "재시작";
                repeat2.text = "재시작";
                menu.text = "메뉴";
                menu2.text = "메뉴";
                read.text = "<독서>";
                break;
            case "chinese":
                continie.text = "继续";
                repeat.text = "重新开始";
                repeat2.text = "重新开始";
                menu.text = "菜单";
                menu2.text = "菜单";
                read.text = "<读>";
                break;
            case "japan":
                continie.text = "持続する";
                repeat.text = "再起動";
                repeat2.text = "再起動";
                menu.text = "メニュー";
                menu2.text = "メニュー";
                read.text = "<読む>";
                break;
        }
    }

    /// Кнопки
    public void Buttons(int id)
    {
        switch (id)
        {
            case 1://"pauseWindow" open
                if (isTouch == false)
                {
                    isTouch = true;
                    data.PauseGameUI(0f);
                    pauseWindow.SetActive(true);
                    pauseButton.SetActive(false);
                    isTouch = false;
                }
                break;
            case 2://"pauseWindow" close
                if (isTouch == false)
                {
                    isTouch = true;
                    pauseButton.SetActive(true);
                    pauseWindow.SetActive(false);
                    data.PauseGameUI(1f);
                    isTouch = false;
                }
                break;
            case 3://Repeat
                if (isTouch == false)
                {
                    isTouch = true;
                    animatorBlackScreenGame.SetBool("isActive", true);
                    StartCoroutine(StartGame(0.9f, PlayerPrefs.GetInt("progress")));
                }
                break;
            case 5://Home
                if (isTouch == false)
                {
                    isTouch = true;
                    block.gameObject.SetActive(true);
                    animatorBlackScreenGame.SetBool("isActive", true);
                    StartCoroutine(StartGame(0.5f, 0));
                }
                break;
            case 7://"loseWindow" open
                if (isTouch == false)
                {
                    isTouch = true;
                    loseWindow.SetActive(true);
                    tap2Next.SetActive(false);
                    isTouch = false;
                }
                break;
            case 8://Repeat from "loseWindow"
                if (isTouch == false)
                {
                    isTouch = true;
                    block.gameObject.SetActive(true);
                    animatorBlackScreenGame.SetBool("isActive", true);
                    StartCoroutine(StartGame(0.5f, PlayerPrefs.GetInt("progress")));
                }
                break;
        }
    }

    /// Загрузка сцены
    public IEnumerator StartGame(float time, int next)
    {
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadSceneAsync(next);
        if (next != 0)
        {
            PlayerPrefs.SetInt("progress", next);
        }
    }

    /// Активация UI при поражении
    public void Dead(string textDead, string textDead2)
    {
        tap2NextText.text = textDead;
        tap2NextText2.text = textDead2;
        tap2Next.SetActive(true);
        pauseButton.SetActive(false);
    }
}
