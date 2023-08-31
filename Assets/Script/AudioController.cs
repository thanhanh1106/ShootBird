using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    // mấy cái ngoặc ngoặc bên dưới là tạo giao diện của chính component script này
    [Header("Main Setting:")] // chữ
    [Range(0f, 1f)] // thanh kéo giá trị
    public float MusicVolume;
    [Range(0f, 1f)]
    public float SFXVolume;

    public AudioSource MusicAudioSoure;
    public AudioSource SFXAudioSoure;

    [Header("Game sound and music")]
    public AudioClip Shooting;
    public AudioClip BirdDie;
    public AudioClip Win;
    public AudioClip Lose;
    public AudioClip[] BackGroundMusics;

    private void Start()
    {
        PlayMusic(BackGroundMusics);
    }
    public void PlaySound(AudioClip sound, AudioSource audioSource = null)
    {
        if (!audioSource) audioSource = SFXAudioSoure;
        if (audioSource) audioSource.PlayOneShot(sound, SFXVolume);
    }
    public void PlaySound(AudioClip[] Sounds,AudioSource audioSource = null)
    {
        if(!audioSource) audioSource = SFXAudioSoure;
        if (audioSource)
        {
            int random = Random.Range(0, Sounds.Length);
            if (Sounds[random] != null) audioSource.PlayOneShot(Sounds[random], SFXVolume);
        }
    }
    public void PlayMusic(AudioClip Music,bool Loop = true)
    {
        if (MusicAudioSoure)
        {
            MusicAudioSoure.clip = Music;
            MusicAudioSoure.loop = Loop;
            MusicAudioSoure.volume = MusicVolume;
            MusicAudioSoure.Play();
        }
    }
    public void PlayMusic(AudioClip[] Musics,bool Loop = true)
    {
        if(MusicAudioSoure)
        {
            int random = Random.Range(0,Musics.Length);
            if (Musics[random])
            {
                MusicAudioSoure.clip = Musics[random];
                MusicAudioSoure.loop = Loop;
                MusicAudioSoure.volume = MusicVolume;
                MusicAudioSoure.Play();
            }
        }
    }
}
