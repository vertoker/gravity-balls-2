﻿using UnityEngine;

/// Коллизия к вспроизведению звука
public class AudioSetCollision : GlobalFunctions
{
    public TypePlaying typePlaying = TypePlaying.Sound;
    public bool oneTime = true;
    public bool loop = false;
    public int layerSound = 0;
    public float volume = 1f;
    public AudioClip setClip;
    private AudioBase audioBase;
    public bool active = true;

    public void Awake()
    {
        audioBase = GameObject.FindWithTag("MainCamera").GetComponent<AudioBase>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && active == true)
        {
            SetMusic();
        }
    }

    public void SetMusic()
    {
        if (active == true)
        {
            if (oneTime == true)
            {
                active = false;
            }
            audioBase.SetSound(setClip, layerSound, volume, typePlaying, loop);
        }
        return;
    }
}
