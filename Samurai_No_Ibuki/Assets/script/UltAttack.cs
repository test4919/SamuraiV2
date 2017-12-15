using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltAttack : MonoBehaviour {

     float takeBossHp;
    public AudioClip isUltSound;

    void Start()
    {
        takeBossHp = 0.15f;      
    }

    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boss")
        {
            GameObject.Find("BossHp").GetComponent<Image>().fillAmount -= takeBossHp;
            GameObject.Find("Boss(Clone)").GetComponent<BossController>().ULTEnemyBlood();
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy"|| other.gameObject.tag == "enemy3" || other.gameObject.tag == "enemyM")
        {
            other.gameObject.GetComponent<EnemyAI>().EnemyDestory();
        }
        else if (other.gameObject.tag == "enemy2")
        {
            var Sky = GameObject.FindWithTag("enemy2").GetComponent<TakoController>();
            other.gameObject.GetComponent<TakoController>().death = true;
           // other.gameObject.GetComponent<EnemyAI>().EnemyDestory();
        }

        //SoundManager.instance.UltUse(isUltSound);
        //SoundManager.instance.Ultvol = 0.3f;
        //SoundManager.instance.Ultpitch = Random.Range(0.95f, 1.05f);
    }
}
