using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip takeHP;
    public float stunVol;
    public GameObject BloodShow;
    GameObject BLS;
    GameObject player;

    // public AudioSource MusicSource; //if have bgm use it  

    //声音管理
    public AudioSource OnetimeSound;
    public AudioSource ULTtimeSound;
    public AudioSource BloodSound;
    public AudioSource BGMUseSound;
    public AudioSource DBgmSound;
    public AudioSource WBgmSound;
    public static SoundManager instance = null;
    public float pitch;
    public float vol;
    public float Ultpitch;
    public float Ultvol;
    // Use this for initialization

    void Awake()
    {
        player = GameObject.Find("Player");
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        BGMUseSound.Play();
    }

    private void Update()
    {
        if (player.GetComponent<Player_Hp>().Hp == 0)
        {
            if (BGMUseSound.isPlaying)
            {
                BGMUseSound.Stop();
                DBgmSound.Play();
            }
        }
    }

    public void UltUse(AudioClip clip)
    {
        ULTtimeSound.volume = Ultvol;
        ULTtimeSound.pitch = Ultpitch;
        ULTtimeSound.clip = clip;
        ULTtimeSound.Play();
    }
    
    public void SingleSound(AudioClip clip)
    {
        OnetimeSound.volume = vol;
        OnetimeSound.pitch = pitch;
        OnetimeSound.clip = clip;
        OnetimeSound.Play();
    }

    public void soundBlood(AudioClip clip)
    {
        BloodSound.Play();
        BloodSound.clip = clip;
    }

    public void BGM(AudioClip clip)
    {

        if (BGMUseSound.isPlaying)
        {
            BGMUseSound.Stop();
            WBgmSound.Play();
        }
    }

    public void playerbloodsound()
    {
        SoundManager.instance.soundBlood(takeHP);
        BLS = GameObject.Instantiate(BloodShow, transform.position, transform.rotation) as GameObject;
        if (player.transform.localScale.x == 0.8f)
        {
            BLS.transform.localScale = new Vector3(-2.5f, 2.5f, 0);
        }
        else if (player.transform.localScale.x == -0.8f)
        {
            BLS.transform.localScale = new Vector3(2.5f, 2.5f, 0);
        }
        player.GetComponent<Animator>().SetTrigger("Stun");
        Destroy(BLS, 0.2f);
        GetComponent<AudioSource>().volume = stunVol;
    }

}
