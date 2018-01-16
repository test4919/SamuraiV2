using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Controller : MonoBehaviour {

    public enum BossState
    {
        Idle=0,
        NormalAttack=1,
        SwordAttack=2,
        TwoBreak=3,
        Dead=4,
    };

    public BossState BState;

    public GameObject Body;
    public GameObject LeftHand;
    public GameObject RightHand;

    public float Blood_Body;
    public float Blood_LeftHand;
    public float Blood_RightHand;
    public float Def_Buff;
    public float Skill_CD_Time;
    public float AttackSpeed;



    // Use this for initialization
    void Start () {
        Blood_Body = 100.0f;
        Blood_LeftHand = 100.0f;
        Blood_RightHand = 100.0f;
        Def_Buff = 3.0f;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
