using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Controller : MonoBehaviour {

    
   
    public GameObject Body;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Player;
    GameObject Lefthand;

    
    public float Def_Buff;
    //public float AttackSpeed;
    public float AttackTime;
    public float AttackColdDown;
    public float WaveTime;
    public float WaveColdDown;
    public float LeftHandHP;
    public float RightHandHP;
    public float BossHP;
    
    public bool Attacking;
    public bool NormalAttack;
    public bool NormalAttackCD;
    public bool isHead;
    public bool WaveAtk;
    public bool WaveAtking;
    public bool WaveAtkCD;

    float def;

    // Use this for initialization
    void Start () {
        Lefthand = GameObject.Find("HandAttack");
        Attacking = false;
        WaveAtking = false;
        NormalAttackCD = false;

        //WaveAtkCD = true; 

        Def_Buff = 3.0f;
        AttackTime = 5.0f;
        AttackColdDown = 5.0f;
        WaveTime = 6.0f;
        WaveColdDown = 6.0f;
        LeftHandHP=100.0f;
        RightHandHP = 100.0f;
        BossHP = 100.0f;
}
	
	// Update is called once per frame
	void Update () {
        normalAttack();
        waveAttack();
        LookPlayer();
        LeftHandATK();
        RightHandATK();
        //RightHand.transform.rotation = Quaternion.FromToRotation(new Vector3(0, 0, 90), new Vector3(0, 0, 30));
        if (isHead&&Attacking)
        {
            StartCoroutine("AtkDown");
            LeftHand.gameObject.tag = "BossLeftHand";
        }

       
        if (AttackTime <= 0)
        {
            NormalAttack = false;
            NormalAttackCD = true;
            //WaveAtkCD = false;
            AttackColdDown -= Time.deltaTime;
            if (AttackColdDown <= 0)
            {
                AttackTime = 5.0f;
                AttackColdDown = 5.0f;
                NormalAttackCD = false;
            }
        }

        if (WaveTime <= 0)
        {
            WaveAtk = false;
            WaveColdDown -= Time.deltaTime;
            WaveAtkCD = true;
            if (WaveColdDown <= 0)
            {
                WaveTime = 6.0f;
                WaveColdDown = 6.0f;
                WaveAtkCD = false;
            }
        }


        
	}
    

    private void normalAttack()
    {
        if (/*!NormalAttack &&*/ !WaveAtk && AttackColdDown >= 5.0f)
        {
            NormalAttack = true;
        }
    }

    private void waveAttack()
    {
        if (NormalAttackCD&&WaveColdDown>=6.0f)
        {
            //WaveAtk = true;
        }
        else
        { 
            //WaveAtk = false; 
        }
    }

    private void LeftHandATK()
    {
        if (NormalAttack)
        {
            if (this.transform.localScale == new Vector3(1, 1, 1) && !isHead)
            {
                LeftHand.transform.Translate(-0.3f, 0, 0);
                if (LeftHand.transform.position.x <= Player.transform.position.x)
                {
                    LeftHand.transform.position = new Vector3(Player.transform.position.x, LeftHand.transform.position.y, 0);
                    isHead = true;
                    Attacking = true;
                    Debug.Log("Left");

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
                    Debug.Log("Right");
                }
            }
            AttackTime -= Time.deltaTime;
        }
    }

    private void RightHandATK()
    {
        if (WaveAtk)
        {
            StartCoroutine("Wave");

            WaveTime -= Time.deltaTime;
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
        LeftHand.gameObject.tag = "BossHandL";
        LeftHand.transform.localPosition = new Vector3(3.2f, 1.58f, 0);
        Lefthand.transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    IEnumerator Wave()
    {
        yield return new WaitForSeconds(0.1f);
        if (this.transform.localScale == new Vector3(-1, 1, 1))
        {
            RightHand.transform.rotation = Quaternion.Euler(0, 0, -3*Time.deltaTime);
            WaveAtkCD = false;
        }
        else if (this.transform.localScale == new Vector3(1, 1, 1))
        {
            //Lefthand.transform.rotation = Quaternion.Euler(0, 0, 30);
        }
    }
    
}
