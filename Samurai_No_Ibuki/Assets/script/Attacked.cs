using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Attacked : MonoBehaviour {

    public float AtkBossDmg;
    GameObject FinalBoss;

    // Use this for initialization
    void Start() {
        AtkBossDmg = 0.04f;
        FinalBoss = GameObject.Find("FinalBoss");
    }
    


    private void OnTriggerEnter2D(Collider2D other)
    {
        var ultfire = GameObject.Find("UltFire").transform;

        if (other.gameObject.tag == "Boss")
        {
            GameObject.Find("BossHp").GetComponent<Image>().fillAmount -= AtkBossDmg;
            if (SceneManager.GetActiveScene().name == "Main")
            {
                GameObject.Find("Boss(Clone)").GetComponent<BossController>().EnemyBlood_show();
                
            }
            else if (SceneManager.GetActiveScene().name == "MachiBattle")
            {
                GameObject.Find("Boss").GetComponent<BossController>().EnemyBlood_show();
                AtkBossDmg = 0.004f;
            }

                ultfire.localScale += new Vector3(0.38f, 0.25f, 0);
            if (ultfire.localScale.x >= 1.5f)
            {
                ultfire.localScale = new Vector3(1.5f, 1, 0);
            }
        }

        if (other.gameObject.tag == "enemy")
        {
            other.GetComponent<EnemyAI>().EnemyDestory();

            ultfire.localScale += new Vector3(0.38f, 0.25f, 0);
            if (ultfire.localScale.x >= 1.5f)
            {
                ultfire.localScale = new Vector3(1.5f, 1, 0);
            }
        }
        else if (other.gameObject.tag == "enemy2" || other.gameObject.tag == "enemyM")
        {
            other.GetComponent<TakoController>().death = true;

            ultfire.localScale += new Vector3(0.38f, 0.25f, 0);
            if (ultfire.localScale.x >= 1.5f)
            {
                ultfire.localScale = new Vector3(1.5f, 1, 0);
            }
        }
        else if (other.gameObject.tag == "enemy3")
        {
            other.GetComponent<EnemyAI>().EnemyDestory();

            ultfire.localScale += new Vector3(0.38f, 0.25f, 0);

            if (ultfire.localScale.x >= 1.5f)
            {
                ultfire.localScale = new Vector3(1.5f, 1, 0);
            }
        }

        if (other.gameObject.tag == "BossHandL")
        {
            FinalBoss.GetComponent<Boss2Controller>().LeftHandHP -= 5.0f;
            ultfire.localScale += new Vector3(0.38f, 0.25f, 0);

            if (ultfire.localScale.x >= 1.5f)
            {
                ultfire.localScale = new Vector3(1.5f, 1, 0);
            }
        }
        if (other.gameObject.tag == "BossHandR")
        {
            FinalBoss.GetComponent<Boss2Controller>().RightHandHP -= 5.0f;
            ultfire.localScale += new Vector3(0.38f, 0.25f, 0);

            if (ultfire.localScale.x >= 1.5f)
            {
                ultfire.localScale = new Vector3(1.5f, 1, 0);
            }
        }
        if (other.gameObject.tag == "BossBody")
        {
            FinalBoss.GetComponent<Boss2Controller>().BossHP -= 10.0f;
            ultfire.localScale += new Vector3(0.38f, 0.25f, 0);

            if (ultfire.localScale.x >= 1.5f)
            {
                ultfire.localScale = new Vector3(1.5f, 1, 0);
            }
        }
    }

}
