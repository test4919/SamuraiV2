using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UltAttack : MonoBehaviour {

     float takeBossHp;
    public AudioClip isUltSound;
    GameObject FinalBoss;

    void Start()
    {
        takeBossHp = 0.15f;
        FinalBoss = GameObject.Find("FinalBoss");
    }

    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boss")
        {
            GameObject.Find("BossHp").GetComponent<Image>().fillAmount -= takeBossHp;
            if (SceneManager.GetActiveScene().name == "Main")
            {
                GameObject.Find("Boss(Clone)").GetComponent<BossController>().ULTEnemyBlood();
            }
            else if (SceneManager.GetActiveScene().name=="MachiBattle")
            {
                GameObject.Find("Boss").GetComponent<BossController>().ULTEnemyBlood();
            }
        }
        if (other.gameObject.tag == "BossHandL")
        {
            FinalBoss.GetComponent<Boss2Controller>().LeftHandHP -= 10f;
            GameObject.Find("FinalBoss").GetComponent<Boss2Controller>().EnemyBlood_show();

        }
        if (other.gameObject.tag == "BossHandR")
        {
            FinalBoss.GetComponent<Boss2Controller>().RightHandHP -= 10f;
            GameObject.Find("FinalBoss").GetComponent<Boss2Controller>().EnemyBlood_show();
        }
        if (other.gameObject.tag == "BossBody")
        {
            FinalBoss.GetComponent<Boss2Controller>().BossHP -= 10f;
            GameObject.Find("FinalBoss").GetComponent<Boss2Controller>().EnemyBlood_show();
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy"|| other.gameObject.tag == "enemy3")
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
