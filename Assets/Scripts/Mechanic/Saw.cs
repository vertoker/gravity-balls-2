using UnityEngine;

/// Общий скрипт пилы
public class Saw : GlobalFunctions
{
    public AudioClip setClip;
    private TypePlaying typePlaying = TypePlaying.Sound;
    private AudioBase audioBase;
    private float speed = 4f;
    private Transform tr;

    private void Awake()
    {
        audioBase = GameObject.FindWithTag("MainCamera").GetComponent<AudioBase>();
        tr = transform;
    }

    private void Update()
    {
        float s = Time.fixedDeltaTime / 0.03f * (Time.deltaTime / 0.03f);
        tr.localEulerAngles = new Vector3(0f, 0f, tr.localEulerAngles.z - speed * s);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            audioBase.SetSound(setClip, 1, 0.2f, typePlaying, false);
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
