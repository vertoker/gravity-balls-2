using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

/// Портал для сюжетного момента на 0 уровне
public class Portal : MonoBehaviour
{
    public GameObject lagWindow;
    public GameObject error;
    public Text errorText;
    public AudioMixerGroup audioMixerGroup;
    public StringLanguageMinimize languageMinimize;
    private GameObject[] lagWindowChilds;
    private GameObject player;
    private GameObject cam;
    private Management m;
    private Data d;
    private Rigidbody2D rbp;
    private GameUI gameui;
    private bool activeAnim = false;
    private AudioBase audioBase;
    private AudioSource audioSource;

    public void Awake()
    {
        audioBase = GameObject.FindWithTag("MainCamera").GetComponent<AudioBase>();
        audioSource = audioBase.layerSounds[0];
    }

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameui = GameObject.FindWithTag("Canvas").GetComponent<GameUI>();
        cam = GameObject.FindWithTag("MainCamera");
        m = cam.GetComponent<Management>();
        d = cam.GetComponent<Data>();
        rbp = player.GetComponent<Rigidbody2D>();

        int l = 231;
        lagWindowChilds = new GameObject[l];
        for (int i = 0; i < l; i++)
        {
            lagWindowChilds[i] = lagWindow.transform.GetChild(i).gameObject;
        }

        error.SetActive(false);
        errorText.text = languageMinimize.GetString();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            d.scaleSlowMo = 0f;
            rbp.gravityScale = 0f;
            rbp.velocity = new Vector2();
            activeAnim = true;
            float t = 3f;

            audioSource.outputAudioMixerGroup = audioMixerGroup;
            m.mathPlayer1.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(-180f, 180f));
            StartCoroutine(Lag1RotatePlayer());
            StartCoroutine(Lag1RotatePlayer());

            GameObject g = lagWindowChilds[Random.Range(0, 230)];
            g.SetActive(!g.activeSelf);
            StartCoroutine(Lags());

            StartCoroutine(gameui.StartGame(t, PlayerPrefs.GetInt("progress") + 1));
            StartCoroutine(LagOffer(t - 1f));
            StartCoroutine(LagOffer2(t - 0.1f));
        }
    }

    public IEnumerator Lag1RotatePlayer()
    {
        yield return new WaitForSecondsRealtime(0.15f);
        m.mathPlayer1.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(-180f, 180f));
        if (activeAnim)
        {
            StartCoroutine(Lag1RotatePlayer());
        }
    }

    public IEnumerator LagOffer(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        audioSource.Stop();
        error.SetActive(true);
        activeAnim = false;
    }

    public IEnumerator LagOffer2(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        errorText.text = string.Empty;
    }

    public IEnumerator Lags()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        GameObject g = lagWindowChilds[Random.Range(0, 230)];
        g.SetActive(!g.activeSelf);
        g = lagWindowChilds[Random.Range(0, 230)];
        g.SetActive(!g.activeSelf);
        g = lagWindowChilds[Random.Range(0, 230)];
        g.SetActive(!g.activeSelf);
        if (activeAnim)
        {
            StartCoroutine(Lags());
        }
    }
}