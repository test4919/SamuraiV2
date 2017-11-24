using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeAttack : MonoBehaviour
{

    private Vector2 forward;
    public GameObject Player;
    bool rebound = false;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        forward = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rebound)
        {
            transform.Translate(forward * Time.deltaTime * 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player") && (!(GameObject.Find("Player").GetComponent<Move>().Ult)))
        {

            //GameObject.Find("Player").GetComponent<Player_Hp>().Hp -= 3;
            GameObject.Find("HeroHpBar").GetComponent<Image>().fillAmount -= 3f / 100f;
            GameObject.Find("Player").GetComponent<Move>().ShowHpBar = true;
            GameObject.Find("Player").GetComponent<Move>().isAtk = true;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().playerbloodsound();
            Destroy(gameObject);
        }
        else if ((other.gameObject.tag == "Player") && ((GameObject.Find("Player").GetComponent<Move>().Ult)))
        {
            rebound = true;
        }
    }




}
