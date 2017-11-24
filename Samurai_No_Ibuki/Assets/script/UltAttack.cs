using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltAttack : MonoBehaviour {

    public float takeBossHp;
    public AudioClip isUltSound;

    void Start()
    {
        takeBossHp = 0.15f;      
    }

    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boss")
        {
            GameObject.Find("BossHp").GetComponent<Image>().fillAmount -= takeBossHp;
            GameObject.Find("Boss(Clone)").GetComponent<BossController>().ULTEnemyBlood();
        }
       
    }
    
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "enemy")
        {

            var samurai = GameObject.FindWithTag("enemy").GetComponent<EnemyAI>();
            samurai.EnemyDestory();
        }
        else if (other.gameObject.tag == "enemy2")
        {
            var Sky = GameObject.FindWithTag("enemy2").GetComponent<TakoController>();
            Sky.death = true;
        }
        else if (other.gameObject.tag == "enemy3")
        {
            var ninja = GameObject.FindWithTag("enemy3").GetComponent<EnemyAI>();
             ninja.EnemyDestory();
        }
        //SoundManager.instance.UltUse(isUltSound);
        //SoundManager.instance.Ultvol = 0.3f;
        //SoundManager.instance.Ultpitch = Random.Range(0.95f, 1.05f);
    }
}
