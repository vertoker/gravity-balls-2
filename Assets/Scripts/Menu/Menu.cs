using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Rendering;

/// Главный скрипт меню
public class Menu : GlobalFunctions
{
    public Animator animatorMenu;
    public Animator animatorBlackScreen;
    public Language l;
    public Camera cam;
    public GameObject start;
    public GameObject settings;
    public GameObject language;
    public GameObject authors;
    public Text deaths;
    public Text levelNow;
    public Text version;
    [Header("Settings")]
    public Sprite sound;
    public Sprite soundMute;
    public Image soundImage;
    public Scrollbar soundScrollbar;
    public Sprite music;
    public Sprite musicMute;
    public Image musicImage;
    public Scrollbar musicScrollbar;
    public Image senRotCircleImage;
    public Scrollbar senRotScrollbar;
    private bool isOpenSettings = false;

    public Image backGround;
    public Material[] effects;
    public RenderPipelineAsset rpa;

    public void Awake()
    {
        GraphicsSettings.renderPipelineAsset = rpa;
        Time.timeScale = 1f;
        animatorBlackScreen.SetBool("isActive", false);
        if (!PlayerPrefs.HasKey("start"))
        {
            PlayerPrefsStart();
        }
        else
        {
            if (PlayerPrefs.GetString("language") != "nothing")
            {
                animatorMenu.SetInteger("isFirst", 1);
            }
        }
        musicLocal = 0f;
        soundLocal = 0f;
    }

    public void Buttons(int id)
    {
        switch (id)
        {
            case 1://Start|Continie\\
                animatorBlackScreen.SetBool("isActive", true);
                StartCoroutine(StartGame());
                break;
            case 2://Open|Close Settings
                isOpenSettings = !isOpenSettings;
                animatorMenu.SetBool("isSettings", isOpenSettings);
                break;
            case 3://Language Open
                animatorMenu.SetBool("isLanguage", true);
                break;
            case 4://Language Cancel
                animatorMenu.SetBool("isLanguage", false);
                l.Cancel();
                break;
            case 5://Language Apply
                animatorMenu.SetBool("isLanguage", false);
                l.Apply();
                break;
            case 6://Graphics Quality
                switch (PlayerPrefs.GetString("graphicsquality"))
                {
                    case "low":
                        PlayerPrefs.SetString("graphicsquality", "medium");
                        break;
                    case "medium":
                        PlayerPrefs.SetString("graphicsquality", "high");
                        break;
                    case "high":
                        PlayerPrefs.SetString("graphicsquality", "low");
                        break;
                }
                l.GraphicsQualityTextEdit();
                break;
            case 7://Authors Open
                animatorMenu.SetBool("isAuthors", true);
                break;
            case 8://Authors Close
                animatorMenu.SetBool("isAuthors", false);
                break;
            case 9://Language Apply Only
                animatorMenu.SetInteger("isFirst", 2);
                l.Apply();
                break;
            case 10://Debug Open
                animatorMenu.SetBool("isDebug", true);
                break;
            case 11://Debug Close
                animatorMenu.SetBool("isDebug", false);
                break;
        }
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("progress"));
    }

    public void PlayerPrefsStart()
    {
        animatorMenu.SetInteger("isfirst", 0);
        PlayerPrefs.SetString("start", "key");
        PlayerPrefs.SetString("language", "nothing");

        float aspect = cam.aspect - 16f / 9f;
        if (aspect > 0.1f) { PlayerPrefs.SetString("graphicsquality", "high"); }
        else if (aspect < -0.1f) { PlayerPrefs.SetString("graphicsquality", "low"); }
        else { PlayerPrefs.SetString("graphicsquality", "medium"); }
        
        PlayerPrefs.SetFloat("sound", 0.5f);
        PlayerPrefs.SetFloat("music", 0.5f);
        PlayerPrefs.SetFloat("senrot", 0.6f);
        PlayerPrefs.SetFloat("rotatenextlevel", 0f);
        PlayerPrefs.SetInt("elevatorsave", 0);
        PlayerPrefs.SetInt("progress", 1);
        PlayerPrefs.SetInt("deaths", 0);
        PlayerPrefs.SetInt("discharge", 0);
        PlayerPrefs.SetInt("lastmenueffect", -1);
        PlayerPrefs.SetString("isshotmode", "false");
        PlayerPrefs.SetString("boss1", "life");
        PlayerPrefs.SetString("boss2", "life");
        PlayerPrefs.SetString("ai", "off");
        PlayerPrefs.SetString("boss3", "life");
        PlayerPrefs.SetString("end", "none");
    }

    public void Start()
    {
        soundScrollbar.value = PlayerPrefs.GetFloat("sound");
        musicScrollbar.value = PlayerPrefs.GetFloat("music");
        senRotScrollbar.value = PlayerPrefs.GetFloat("senrot");
        deaths.text = "D: " + PlayerPrefs.GetInt("deaths");
        levelNow.text = "L: " + PlayerPrefs.GetInt("progress");
        version.text = "v: " + Application.version;

        if (effects.Length != 0)
        {
            bool make = true;
            int rand = 0;
            if (PlayerPrefs.GetInt("lastmenueffect") != -1)
            {
                while (make)
                {
                    rand = Random.Range(0, effects.Length);
                    if (PlayerPrefs.GetInt("lastmenueffect") != rand)
                    {
                        make = false;
                        PlayerPrefs.SetInt("lastmenueffect", rand);
                    }
                }
            }
            else
            {
                rand = 3;
                PlayerPrefs.SetInt("lastmenueffect", rand);
            }
            backGround.material = effects[rand];
        }
    }

    public void FixedUpdate()
    {
        PlayerPrefs.SetFloat("sound", soundScrollbar.value);
        PlayerPrefs.SetFloat("music", musicScrollbar.value);
        PlayerPrefs.SetFloat("senrot", senRotScrollbar.value);

        if (PlayerPrefs.GetFloat("sound") == 0f)
        { soundImage.sprite = soundMute; }
        else { soundImage.sprite = sound; }

        if (PlayerPrefs.GetFloat("music") == 0f)
        { musicImage.sprite = musicMute; }
        else { musicImage.sprite = music; }

        senRotCircleImage.fillAmount = PlayerPrefs.GetFloat("senrot");
    }

    private float musicLocal = 0f;
    public void Music()
    {
        if (PlayerPrefs.GetFloat("music") == 0f)
        {
            if (musicLocal == 0f)
            {
                musicLocal = 0.5f;
            }
            musicScrollbar.value = musicLocal;
            musicLocal = 0f;
        }
        else
        {
            musicLocal = PlayerPrefs.GetFloat("music");
            musicScrollbar.value = 0f;
        }
    }

    private float soundLocal = 0f;
    public void Sound()
    {
        if (PlayerPrefs.GetFloat("sound") == 0f)
        {
            if (soundLocal == 0f)
            {
                soundLocal = 0.5f;
            }
            soundScrollbar.value = soundLocal;
            soundLocal = 0f;
        }
        else
        {
            soundLocal = PlayerPrefs.GetFloat("sound");
            soundScrollbar.value = 0f;
        }
    }

    private float senrotLocal = 0.6f;
    public void SenRot()
    {
        if (PlayerPrefs.GetFloat("senrot") == 0.6f)
        {
            senRotScrollbar.value = senrotLocal;
            senrotLocal = 0f;
        }
        else
        {
            senrotLocal = PlayerPrefs.GetFloat("senrot");
            senRotScrollbar.value = 0.6f;
        }
    }
}