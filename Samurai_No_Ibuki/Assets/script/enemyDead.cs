using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDead : MonoBehaviour {

    public AudioClip slash;
    public GameObject Blood;
    public GameObject createEnemy01;
    GameObject nowBlood;
    Animator animator;
    GameObject ShowEnemy01;


    public Transform player; // プレイヤーを代入
    public float limitDistance = 1000;

    private bool isGround = false;

    //ターゲットオブジェクト
    public GameObject Player;


    public Rigidbody2D rb2d;



    private Vector2 forward;
    private Vector2 Deadforward;

    bool Run;

    public float Hit = 0;

    public float dis;

    public float random;

    float attackdelay;

    float timeleft;

    bool IsGround;

    bool DeadFlag = false;


    GameObject ea;

    public Vector2 scale;

   
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Deadforward = Player.transform.position - transform.position;
        if (DeadFlag)
        {

            transform.Translate(Deadforward * Time.deltaTime * 0.5f);

        }
    }
    private void Dead()
    {
        DeadFlag = true;
        animator.SetBool("E3Drop", true);
    }
}
