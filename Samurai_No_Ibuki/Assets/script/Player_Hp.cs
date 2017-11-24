using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Hp : MonoBehaviour {
    public float Hp; //人物血量
    private float alpha = 0f;
    private float OverAlpha = 0f;
    private float BackAlpha=1f;
    private bool timeChecking;
    private float defeatTime = 0f;
   

    //Hp Color
    public Color FColor;
    public Color LColor;

    public GameObject Choose;

    Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        Hp = 100;
    }
    
    void Update () {
        if (timeChecking)
        {
            //defeatTime += 1f * Time.deltaTime;
            defeatTime += 0.5f;
            GetComponent<SpriteRenderer>().color = FColor;
            
        }
        TakeAttack();
        if (Hp <= 0) { return; }
        LowHp();
    }

    

    private void LowHp()
    {
        if (Hp <= 50)
        {
            if (Time.time % 1f > 0.8f)
            {
                GetComponent<SpriteRenderer>().color = LColor;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = FColor;
            }
        }
    }
    private void TakeAttack()
    {
        if (Hp <= 0)
        {
           
            animator.SetTrigger("defeat");
            Hp = 0;
            timeChecking = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            Time.timeScale = 0;
            if (defeatTime >= 2f)
            {
                alpha += 0.01f;//黑色画面出现
                if (Hp == 0)
                {
                    GameObject.Find("EndColor").GetComponent<Image>().color = new Color(0, 0, 0, alpha);
                }
            }
            if (alpha >= 1)//失败出现
            {
                
                OverAlpha += 0.01f;
                GameObject.Find("setumei").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, OverAlpha);
                defeatTime = 0; 
                if (OverAlpha >= 1)
                {
                    Choose.SetActive(true);
                }
            }
        }

       
    }

}
