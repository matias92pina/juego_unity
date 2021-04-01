using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager obj;

    public AudioClip jump;
    public AudioClip coin;
    public AudioClip gui;
    public AudioClip hit;
    public AudioClip enemyHit;
    public AudioClip win;

    private AudioSource audioSrc;

    void Awake()
    {
        obj = this;
        audioSrc = gameObject.AddComponent<AudioSource>();
    }

    public void playJump() { playSound(jump); }
    public void playCoin() { playSound(coin); }
    public void playGui() { playSound(gui); }
    public void playHit() { playSound(hit); }
    public void playEnemyHit() { playSound(enemyHit); }
    public void playWin() { playSound(win); }
    
    public void playSound(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }

    void OnDestroy()
    {
        obj = null;
    }
}
