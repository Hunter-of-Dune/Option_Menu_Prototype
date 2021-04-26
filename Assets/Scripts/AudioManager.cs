using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource bgMusic;
    [SerializeField] AudioSource sfx;

    public AudioSource BGMusic
    {
        get { return bgMusic; }
        private set { bgMusic = value; }
    }

    public AudioSource SFX
    {
        get { return sfx; }
        private set { sfx = value; }
    }

    [SerializeField] float musicVolume;
    [SerializeField] float SFXVolume;

    private void Start()
    {
        GameManager.Instance.MusicVolumeChanged.AddListener(OnMusicVolumeChanged);
        GameManager.Instance.SFXVolumeChanged.AddListener(OnSFXVolumeChanged);

        StartCoroutine(FadeInAudio(bgMusic, 1f, musicVolume));
        StartCoroutine(FadeInAudio(sfx, 1f, SFXVolume));
    }


    public void OnMusicVolumeChanged(float value)
    {
        bgMusic.volume = value;
    }

    public void OnSFXVolumeChanged(float value)
    {
        sfx.volume = value;
    }

    public IEnumerator FadeOutAudio(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator FadeInAudio(AudioSource audioSource, float fadeTime, float targetVolume)
    {
        audioSource.volume = 0f;
        audioSource.Play();

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.volume = targetVolume;
    }

}
