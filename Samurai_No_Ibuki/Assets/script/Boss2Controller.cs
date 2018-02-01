using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Controller : MonoBehaviour {

    public enum BossState
    {
        Idle=0,
        NormalAttack=1,
        SwordAttack=2,
        Dead=3,
    };

    public BossState BState;

    public GameObject Body;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Player;
    GameObject Lefthand;

    public float Blood_Body;
    public float Blood_LeftHand;
    public float Blood_RightHand;
    public float Def_Buff;
    public float Skill_CD_Time;
    public float AttackSpeed;
    public float AttackTime;
    public float AttackColdDown;
    public float LeftHandHP;
    public float RightHandHP;
    public float BossHP;


    public bool Attacking;
    public bool NormalAttack;
    public bool isHead;
    public bool SpAtk;

    float def;

    // Use this for initialization
    void Start () {
        Blood_Body = 100.0f;
        Blood_LeftHand = 100.0f;
        Blood_RightHand = 100.0f;
        Def_Buff = 3.0f;
        AttackTime = 5.0f;
        AttackColdDown = 5.0f;
        Lefthand = GameObject.Find("HandAttack");
        Attacking = false;
        LeftHandHP=100.0f;
        RightHandHP = 100.0f;
        BossHP = 100.0f;
}
	
	// Update is called once per frame
	void Update () {
        normalAttack();
        LookPlayer();
        LeftHandATK();

        if (isHead&&Attacking)
        {
            StartCoroutine("AtkDown");
            LeftHand.gameObject.tag = "BossLeftHand";
        }

       
        if (AttackTime <= 0)
        {
            NormalAttack = false;
            AttackColdDown -= Time.deltaTime;
            if (AttackColdDown <= 0)
            {
                AttackTime = 5.0f;
                AttackColdDown = 5.0f;
            }
        }
        
	}
    

    private void normalAttack()
    {
        if (!NormalAttack && SpAtk == false && AttackColdDown >= 5.0f)
        {
            NormalAttack = true;
        }
    }

    private void LeftHandATK()
    {
        if (NormalAttack)
        {
            if (this.transform.localScale == new Vector3(1, 1, 1) && !isHead)
            {

                LeftHand.transform.Translate(-0.3f, 0, 0);
                if (LeftHand.transform.localPosition.x <= Player.transform.position.x)
                {
                    LeftHand.transform.position = new Vector3(Player.transform.position.x, LeftHand.transform.position.y, 0);
                    isHead = true;
                    Attacking = true;


                    //        // いまプレイヤーの頭の位置にいるのか？
                    //        // if(プレイヤーの頭の位置にいる？)
                    //        // {
                    //        //   flg = true;
                    //        //  StartCoroutine("AtkDown");
                    //        // }
                    //        //StartCoroutine("AtkDown");
                    //        //Debug.Log("1111111");
                }

            }

            else if (this.transform.localScale == new Vector3(-1, 1, 1) && !isHead)
            {
                LeftHand.transform.Translate(-0.3f, 0, 0);
                if (LeftHand.transform.position.x >= Player.transform.position.x)
                {
                    LeftHand.transform.position = new Vector3(Player.transform.position.x, LeftHand.transform.position.y, 0);
                    isHead = true;
                    Attacking = true;

                }
            }
            AttackTime -= Time.deltaTime;
        }
    }

    private void LookPlayer()
    {
        if (Attacking)
        {
            return;
        }

        if (Player.transform.position.x > this.transform.position.x)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    IEnumerator  AtkDown()
    {
        if (this.transform.localScale == new Vector3(-1, 1, 1))
        {
            Lefthand.transform.rotation = Quaternion.Euler(0, 0, -30);
        }
        else if (this.transform.localScale == new Vector3(1, 1, 1))
        {
            Lefthand.transform.rotation = Quaternion.Euler(0, 0, 30);
        }
        yield return new WaitForSeconds(0.5f);
        LeftHand.transform.Translate(0, -0.3f, 0);
        yield return new WaitForSeconds(1f);
        isHead = false;
        Attacking = false;
        LeftHand.gameObject.tag = "BossHand";
        LeftHand.transform.localPosition = new Vector3(3.2f, 1.58f, 0);
        Lefthand.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
