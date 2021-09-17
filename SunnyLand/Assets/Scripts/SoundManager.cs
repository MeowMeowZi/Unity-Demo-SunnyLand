using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundManager;
    public AudioSource bgmAudio;
    [SerializeField]
    private AudioClip jumpAudio, hurtAduio, cherryAudio, gemAudio, deathAudio;

    private void Awake()
    {
        instance = this;
    }

    public void JumpAudio()
    {
        soundManager.clip = jumpAudio;
        soundManager.Play();
    }

    public void HurtAudio()
    {
        soundManager.clip = hurtAduio;
        soundManager.Play();
    }

    public void CherryAudio()
    {
        soundManager.clip = cherryAudio;
        soundManager.Play();
    }

    public void GemAudio()
    {
        soundManager.clip = gemAudio;
        soundManager.Play();
    }

    public void DeathAudio()
    {
        soundManager.clip = deathAudio;
        soundManager.Play();
    }
}
