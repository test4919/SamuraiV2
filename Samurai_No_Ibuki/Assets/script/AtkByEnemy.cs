﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtkByEnemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyWeapon")
        {
            GameObject.Find("Player").GetComponent<Player_Hp>().Hp -= 2.0f;
            //GameObject.Find("HeroHpBar").GetComponent<Image>().fillAmount -= 1f / 100f;
            GameObject.Find("HeroHpBar").GetComponent<Image>().fillAmount = GameObject.Find("Player").GetComponent<Player_Hp>().Hp / 100.0f;
            GameObject.Find("Player").GetComponent<Move>().ShowHpBar = true;
            GameObject.Find("Player").GetComponent<Move>().isAtk = true;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().playerbloodsound();
        }

        if (other.gameObject.tag == "BossLeftHand")
        {
            GameObject.Find("Player").GetComponent<Player_Hp>().Hp -= 10.0f;
            GameObject.Find("HeroHpBar").GetComponent<Image>().fillAmount = GameObject.Find("Player").GetComponent<Player_Hp>().Hp / 100.0f;
            GameObject.Find("Player").GetComponent<Move>().ShowHpBar = true;
            GameObject.Find("Player").GetComponent<Move>().isAtk = true;

        }
        if (other.gameObject.tag == "BossWave")
        {
            GameObject.Find("Player").GetComponent<Player_Hp>().Hp -= 15.0f;
            GameObject.Find("HeroHpBar").GetComponent<Image>().fillAmount = GameObject.Find("Player").GetComponent<Player_Hp>().Hp / 100.0f;
            GameObject.Find("Player").GetComponent<Move>().ShowHpBar = true;
            GameObject.Find("Player").GetComponent<Move>().isAtk = true;

        }
        if (other.gameObject.tag == "Flame")
        {
            GameObject.Find("Player").GetComponent<Player_Hp>().Hp -= 5.0f;
            GameObject.Find("HeroHpBar").GetComponent<Image>().fillAmount = GameObject.Find("Player").GetComponent<Player_Hp>().Hp / 100.0f;
            GameObject.Find("Player").GetComponent<Move>().ShowHpBar = true;
            GameObject.Find("Player").GetComponent<Move>().isAtk = true;

        }
    }

}
