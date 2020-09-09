using UnityEngine;

/// Модель громкости звука, уменьшающийся при отдалении от источника (недоработан)
public class AudioPhysicsVolume : MonoBehaviour
{
    public float length = 5f;
    public AudioSet[] audioSets;
    //private AudioSource audioSource;
    //private int asl;
    private int las;
    private Transform player;
    private Transform tr;

    public void Start()
    {
        tr = transform;
        las = audioSets.Length;
        //asl = audioSets[0].layerSound;
        Camera cam = Camera.main;
        player = cam.GetComponent<Management>().player.transform;
        //audioSource = cam.GetComponent<AudioBase>().layerSounds[asl];
        for (int i = 0; i < las; i++)
        {
            audioSets[i].volume = 0f;
        }
        //audioSource.volume = 0f;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float s = (length - Vector2.Distance(tr.position, player.position)) / length;
            if (s < 0f) { s = 0f; }
            for (int i = 0; i < las; i++)
            {
                audioSets[i].volume = s;
            }
            //audioSource.volume = s;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < las; i++)
            {
                audioSets[i].volume = 0f;
            }
            //audioSource.volume = 0f;
        }
    }
}
