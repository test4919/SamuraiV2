using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour {


    public AudioSource BossOneSound;
    public static BossSound instance = null;
    public float pitch;
    public float vol;
    // Use this for initialization

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OneTimeBossSound(AudioClip clip)
    {
        BossOneSound.volume = vol;
        BossOneSound.pitch = pitch;
        BossOneSound.clip = clip;
        BossOneSound.Play();
    }
}
