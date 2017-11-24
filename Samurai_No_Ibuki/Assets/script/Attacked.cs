using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacked : MonoBehaviour {

    public float AtkBossDmg;

    // Use this for initialization
    void Start() {
        AtkBossDmg = 0.04f;
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    var ultfire = GameObject.Find("UltFire").transform;

    //    if (other.gameObject.tag == "Boss")
    //    {
    //        GameObject.Find("BossHp").GetComponent<Image>().fillAmount -= AtkBossDmg;
    //        GameObject.Find("Boss").GetComponent<BossController>().EnemyBlood_show();
    //        ultfire.localScale += new Vector3(0.38f, 0.25f, 0);
    //        if (ultfire.localScale.x >= 1.5f)
    //        {
    //            ultfire.localScale = new Vector3(1.5f, 1, 0);
    //        }
    //    }
    //}


    private void OnTriggerEnter2D(Collider2D other)
    {
        var ultfire = GameObject.Find("UltFire").transform;

        if (other.gameObject.tag == "Boss")
        {
            GameObject.Find("BossHp").GetComponent<Image>().fillAmount -= AtkBossDmg;
            GameObject.Find("Boss(Clone)").GetComponent<BossController>().EnemyBlood_show();
            ultfire.localScale += new Vector3(0.38f, 0.25f, 0);
            if (ultfire.localScale.x >= 1.5f)
            {
                ultfire.localScale = new Vector3(1.5f, 1, 0);
            }
        }

        if (other.gameObject.tag == "enemy")
        {
            other.GetComponent<EnemyAI>().EnemyDestory();
           
            ultfire.localScale += new Vector3(0.38f, 0.25f,0);
            if (ultfire.localScale.x >=1.5f )
            {
                ultfire.localScale = new Vector3(1.5f, 1, 0);
            }
        }
       else if (other.gameObject.tag == "enemy2")
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

    }

}
