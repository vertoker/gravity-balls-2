using UnityEngine;

/// Триггер к музыке (старт, стоп)
public class AudioEdit : MonoBehaviour
{
    public enum EditType { Play = 0, Stop = 1 };
    public EditType editType = EditType.Stop;
    public int layerSound = 0;
    private AudioBase audioBase;
    private AudioSource audioSource;

    public void Awake()
    {
        audioBase = GameObject.FindWithTag("MainCamera").GetComponent<AudioBase>();
        audioSource = audioBase.layerSounds[layerSound];
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (editType)
        {
            case EditType.Play:
                audioSource.Play();
                break;
            case EditType.Stop:
                audioSource.Stop();
                break;
        }
    }
}
