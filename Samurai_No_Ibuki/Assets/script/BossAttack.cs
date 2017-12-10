using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAttack : MonoBehaviour
{
    GameObject PlayerHp;

    void Start()
    {
        PlayerHp = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHp.GetComponent<Player_Hp>().Hp -= 10.0f;
            //GameObject.Find("HeroHpBar").GetComponent<Image>().fillAmount -= 20f / 100f;
            GameObject.Find("HeroHpBar").GetComponent<Image>().fillAmount = PlayerHp.GetComponent<Player_Hp>().Hp / 100.0f;
            GameObject.Find("Player").GetComponent<Move>().ShowHpBar = true;
            GameObject.Find("Player").GetComponent<Move>().isAtk = true;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().playerbloodsound();
        }
    }
    
}
