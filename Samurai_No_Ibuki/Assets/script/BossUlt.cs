﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUlt : MonoBehaviour {
    GameObject PlayerHp;

    void Start()
    {
        PlayerHp = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("TakePlayerHp");
            PlayerHp.GetComponent<Player_Hp>().Hp -= 18.0f;
            GameObject.Find("HeroHpBar").GetComponent<Image>().fillAmount = PlayerHp.GetComponent<Player_Hp>().Hp/100.0f;
            GameObject.Find("Player").GetComponent<Move>().ShowHpBar = true;
            GameObject.Find("Player").GetComponent<Move>().isAtk = true;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().playerbloodsound();
        }
    }

}
