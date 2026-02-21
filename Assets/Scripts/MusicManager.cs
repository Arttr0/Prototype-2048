using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        musicSource = GetComponent<AudioSource>();
    }
    //Так как музыка на всю игру одна, этот метод, в принципе, не используем
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    // Сбрасываем музыку
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void PauseMusic()
    {
        musicSource.Pause();
    }
    public void UnPauseMusic()
    {
        if(!musicSource.isPlaying)
            musicSource.UnPause();
    }
}
