using UnityEngine;
using System.Collections;

/// Основной скрипт, связанный со звуком
public class AudioBase : GlobalFunctions
{
    public AudioSource[] layerSounds = new AudioSource[0];/// 5 штук
    public GameObject music;
    private float musicValue, soundValue;
    private int lengthLayerSounds = 0;
    private bool soundActive = true;
    private Coroutine offsetActive;
    private int lowerSoundCoroutineCounter = 100;
    private int upSoundCoroutineCounter = 0;

    public void Awake()
    {
        soundActive = PlayerPrefs.GetString("graphicsquality") != "low";
        musicValue = PlayerPrefs.GetFloat("music");
        soundValue = PlayerPrefs.GetFloat("sound");
        lengthLayerSounds = layerSounds.Length;
        for (int i = 0; i < lengthLayerSounds; i++)
        {
            layerSounds[i].enabled = false;
        }
    }

    /// Уменьшение звука
    public void LowerSound(float timer, int upd, int id, TypePlaying typePlaying)
    {
        lowerSoundCoroutineCounter = upd;
        if (typePlaying == TypePlaying.Music)
        { StartCoroutine(LowerSoundCoroutine(timer, upd, id, musicValue)); }
        else { StartCoroutine(LowerSoundCoroutine(timer, upd, id, soundValue)); }
    }

    /// Увеличение звука
    public void UpSound(float timer, int upd, int id, TypePlaying typePlaying)
    {
        upSoundCoroutineCounter = 0;
        if (typePlaying == TypePlaying.Music)
        { StartCoroutine(UpSoundCoroutine(timer, upd, id, musicValue)); }
        else { StartCoroutine(UpSoundCoroutine(timer, upd, id, soundValue)); }
    }

    /// Анимация уменьшения звука
    public IEnumerator LowerSoundCoroutine(float timer, int upd, int id, float volumeSen)
    {
        yield return new WaitForSeconds(timer);
        layerSounds[id].volume = Stable2((layerSounds[id].volume / volumeSen - timer) * volumeSen, 0f, 1f);
        if (lowerSoundCoroutineCounter > 1)
        {
            StartCoroutine(LowerSoundCoroutine(timer, upd, id, volumeSen));
            lowerSoundCoroutineCounter -= 1;
        }
    }

    /// Анимация увеличения звука
    public IEnumerator UpSoundCoroutine(float timer, int upd, int id, float volumeSen)
    {
        yield return new WaitForSeconds(timer);
        layerSounds[id].volume = Stable2((layerSounds[id].volume / volumeSen + timer) * volumeSen, 0f, 1f);
        if (upSoundCoroutineCounter < upd)
        {
            StartCoroutine(UpSoundCoroutine(timer, upd, id, volumeSen));
            upSoundCoroutineCounter += 1;
        }
    }

    /// Обновление громкости звука
    public void UpdateSound()
    {
        if (soundActive)
        {
            float time = Time.timeScale;
            for (int i = 0; i < lengthLayerSounds; i++)
            {
                AudioSource audioSource = layerSounds[i];
                if (audioSource.enabled == true)
                {
                    audioSource.pitch = time;
                }
            }
        }
    }

    /// Запуск таймер для воспроизведения звука
    public void SetSound(AudioClip audioClip, int layerSound, float volume, TypePlaying typePlaying, bool loop, float time)
    {
        StartCoroutine(SetSoundTime(audioClip, layerSound, volume, typePlaying, loop, time));
    }

    /// Таймер для воспроизведения звука
    public IEnumerator SetSoundTime(AudioClip audioClip, int layerSound, float volume, TypePlaying typePlaying, bool loop, float time)
    {
        yield return new WaitForSeconds(time);
        SetSound(audioClip, layerSound, volume, typePlaying, loop);
    }

    /// Воспроизведение звука
    public void SetSound(AudioClip audioClip, int layerSound, float volume, TypePlaying typePlaying, bool loop)
    {
        if (volume == 0f) { return; }
        if (soundActive)
        {
            AudioSource audioSource = layerSounds[layerSound];
            audioSource.enabled = true;
            audioSource.clip = audioClip;
            audioSource.loop = loop;
            if (typePlaying == TypePlaying.Sound)
            {
                audioSource.volume = soundValue * volume;
            }
            else
            {
                audioSource.volume = musicValue * volume;
            }
            audioSource.Play();
            if (offsetActive != null)
            {
                StopCoroutine(offsetActive);
                offsetActive = null;
            }
            if (!loop)
            {
                offsetActive = StartCoroutine(Offet(layerSound, audioClip.length, audioSource));
            }
        }
    }

    /// Отключение аудио файла
    public IEnumerator Offet(int layerSound, float length, AudioSource audioSource)
    {
        yield return new WaitForSeconds(length);
        if (audioSource.clip == layerSounds[layerSound].clip)
        {
            AudioSource audioSource2 = layerSounds[layerSound];
            audioSource2.Stop();
            audioSource2.enabled = false;
        }
    }
}