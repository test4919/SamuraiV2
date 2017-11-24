using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUlt : MonoBehaviour {
    GameObject bossHp;

    void Start()
    {
        bossHp = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("TakePlayerHp");
           // bossHp.GetComponent<Player_Hp>().Hp -= 15.0f;
            GameObject.Find("HeroHpBar").GetComponent<Image>().fillAmount -= 30f / 100f;
            GameObject.Find("Player").GetComponent<Move>().ShowHpBar = true;
            GameObject.Find("Player").GetComponent<Move>().isAtk = true;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().playerbloodsound();
        }
    }

}
