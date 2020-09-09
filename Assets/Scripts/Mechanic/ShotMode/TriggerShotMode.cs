using UnityEngine;

/// Триггер для другого режима передвижения
public class TriggerShotMode : GlobalFunctions
{
    public bool isShotMode = true;
    public bool isOneTime = false;
    public bool isActive = true;
    public AudioClip audioClip;
    private Management management;
    private AudioBase audioBase;

    public void Start()
    {
        GameObject cam = GameObject.FindWithTag("MainCamera");
        management = cam.GetComponent<Management>();
        audioBase = cam.GetComponent<AudioBase>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isActive)
        {
            if (isOneTime) { isActive = false; }
            audioBase.SetSound(audioClip, 4, 0.7f, TypePlaying.Sound, false);
            management.IsShotMode(isShotMode);
        }
    }
}
