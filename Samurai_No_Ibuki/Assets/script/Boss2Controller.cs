﻿using System.Collections;
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

    public float Blood_Body;
    public float Blood_LeftHand;
    public float Blood_RightHand;
    public float Def_Buff;
    public float Skill_CD_Time;
    public float AttackSpeed;
    public float AttackTime;
    public float AttackColdDown;

    public bool NormalAttack;
    public bool SpAtk;

    // Use this for initialization
    void Start () {
        Blood_Body = 100.0f;
        Blood_LeftHand = 100.0f;
        Blood_RightHand = 100.0f;
        Def_Buff = 3.0f;
        AttackTime = 5.0f;
        AttackColdDown = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
        normalAttack();
        LookPlayer();
        if (NormalAttack)
        {
            if (this.transform.localScale == new Vector3(1, 1, 1))
            {
                LeftHand.transform.Translate(-0.3f, 0, 0);
                if (LeftHand.transform.position.x <= Player.transform.position.x)
                {
                    LeftHand.transform.position = new Vector3(Player.transform.position.x-2, LeftHand.transform.position.y, 0);
                    StartCoroutine("AtkDown");
                }
            }
           
            else if (this.transform.localScale == new Vector3(-1, 1, 1))
            {
                LeftHand.transform.Translate(-0.3f, 0, 0);
                if (LeftHand.transform.position.x >= Player.transform.position.x)
                {
                    LeftHand.transform.position = new Vector3(Player.transform.position.x + 2, LeftHand.transform.position.y, 0);
                    StartCoroutine("AtkDown");
                }
            }
            AttackTime -= Time.deltaTime;
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

    private void LookPlayer()
    {
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
        yield return new WaitForSeconds(0.5f);
        LeftHand.transform.Translate(0, -0.3f, 0);
        yield return new WaitForSeconds(1f);
        LeftHand.transform.localPosition = new Vector3(0.14f, 1.58f, 0);
    }
}
