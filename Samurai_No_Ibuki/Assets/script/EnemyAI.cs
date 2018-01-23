using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyAI : MonoBehaviour {

	public SamuraiController thisController;
    public GameObject BloodShowR;
    public GameObject BloodShowL;
    Rigidbody2D thisRig;
	GameObject PlayerGO;
    GameObject BloodR;
    GameObject BloodL;

    public float findPlayerTimer;
	public float attackTimer;
	public float maxTimer;
	public float attackDist;
	public float skillDist;
	public float speed;

	public  bool pause;
	float dist;
	public float timer;
	float skillTimer;
    bool bloodOpen=false;

    bool DeadFlag = false;
    private Vector3 Deadforward;
    public GameObject Player;
	
    public const float g = -9.8f;
    float flydistance = 10.0f;
    public float horizontalspeed = 0.8f;

    public float verticalSpeed = 0.6f;
    float time = 0.0f;
    float GR1Y;
    float GR2Y;
    bool ground = false;
    public LayerMask groundLayer;

     float Power = 30;
    public float Angle = 60;
    public float Gravity = -10;
    private Vector3 MoveSpeed;
    private Vector3 GritySpeed = Vector3.zero;
    private float dTime = 0;
    private Vector3 currentAngle;
    bool forward;
    private Vector3 GravityVec;
    float forwardflag;
    float CutCount = 0;




    bool isGround()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 3.25f;

        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            //this.GetComponent<Animator>().SetTrigger("DeadOG");

            // this.GetComponent<Animator>().SetBool("DeadOnGround", true);
            //this.GetComponent<Rigidbody2D>().bodyTyp
            
            Debug.Log("Hit"+ground);
            return true;
        }
        else if (hit.collider == null)
        {
            Debug.Log("noHit"+ground);
           
        }
        return false;
    }
    // Use this for initialization
    void Start () {
		FindPlayer ();
		timer = 0f;
		skillTimer = 0f;
		dist = 10000f;
		pause = false;
		thisRig = this.GetComponent<Rigidbody2D>();
		thisController = this.GetComponent<SamuraiController> ();
        Player = GameObject.Find("Player");
        if (PlayerGO.transform.position.x > this.transform.position.x)//敌人向右移动
        {
            this.transform.localScale = new Vector3(-0.9f, 0.9f, 1);
        }
        else
         if (PlayerGO.transform.position.x < this.transform.position.x)//敌人向左移动
        {
            this.transform.localScale = new Vector3(0.9f, 0.9f, 1);
        }
        //GR1Y = GameObject.Find("Gr").transform.position.y;

        GravityVec = Vector3.zero;
    }



    // Update is called once per frame
    void Update() {

       

        if (!pause)
            timer += Time.deltaTime;
        if (timer >= findPlayerTimer)
            FindPlayer();
        if (timer >= attackTimer) {
            pause = true;
            EnemyAttack();
        }
        if (timer > 0.08f)
        {
            thisController.inAtkRange = false;
        }
        if (timer >= maxTimer)
        {
            timer = 0;
        }

        Physics2D.IgnoreLayerCollision(9,9);
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(9,13);
        Physics2D.IgnoreLayerCollision(13,13);
        Physics2D.IgnoreLayerCollision(13, 10);
        Physics2D.IgnoreLayerCollision(13, 14);
        Physics2D.IgnoreLayerCollision(9, 14);

      

        if (CutCount==1)
        {

            time += Time.deltaTime;
            ////float time = 0.0f;

            Vector2 flyforward = new Vector2(horizontalspeed, 0);
            if (forwardflag > 0)
                transform.Translate(Vector3.left * horizontalspeed * Time.deltaTime * 0.5f, Space.World);
            else
                transform.Translate(Vector3.right * Time.deltaTime * 0.5f, Space.World);
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime, Space.World);

            if (time > 2.0f)
            {
                Destroy(gameObject);
            }
          

        }else if(CutCount==2)
        {
            time += Time.deltaTime;

            Vector2 flyforward = new Vector2(horizontalspeed * 1.5f, 0);
            if (forwardflag > 0)
                transform.Translate(Vector3.left * horizontalspeed * Time.deltaTime * 0.5f, Space.World);
            else
                transform.Translate(Vector3.right * Time.deltaTime * 0.5f, Space.World);
            transform.Translate(Vector3.up * verticalSpeed * 1.5f * Time.deltaTime, Space.World);

            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 720);
            if (time > 2.5f)
            {
                Destroy(gameObject);
            }
        }
        else if (CutCount ==3)
        {
            time += Time.deltaTime;

            Vector2 flyforward = new Vector2(horizontalspeed * 2.0f, 0);
            if (forwardflag > 0)
                transform.Translate(Vector3.left * horizontalspeed * 3.0f * Time.deltaTime * 0.5f, Space.World);
            else
                transform.Translate(Vector3.right * Time.deltaTime * 0.5f, Space.World);
            transform.Translate(Vector3.up * verticalSpeed * 5.0f * Time.deltaTime, Space.World);

            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 7200);
            if (time > 3.0f)
            {
                Destroy(gameObject);
            }
        }


    }

    
	void FindPlayer()
	{
		PlayerGO = GameObject.FindGameObjectWithTag ("Player");
	}
	void EnemyAttack()
	{
		if (!DeadFlag)
        	{
		dist =Mathf.Abs( Vector3.Distance (PlayerGO.transform.position, this.transform.position));
		//Debug.Log (dist);
		if(PlayerGO.transform.position.x>this.transform.position.x)//敌人向右移动
		{
			this.transform.localScale = new Vector3 (-0.9f, 0.9f, 1);
		}
		else
			if(PlayerGO.transform.position.x<this.transform.position.x)//敌人向左移动
			{
				this.transform.localScale = new Vector3 (0.9f, 0.9f, 1);
			}
		if (dist > skillDist&&thisController.eType==SamuraiController.EnemyType.ninja) {
		//	Debug.Log ("用技能了！");	
			thisController.Skill ();
			timer = 0;
		}
		else if (dist <= attackDist) {
            thisController.SamuraiIdle();
            this.GetComponent<Animator>().SetBool("EWait", true);
            this.GetComponent<Animator>().SetBool("ERun", false);
            thisController.SamuraiAttack();
            timer = 0;
            pause = false;
        }
		else {
            this.thisController.inAtkRange = false;
			thisController.SamuraiRun ();
			if(PlayerGO.transform.position.x>this.transform.position.x)//敌人向右移动
			{
				thisRig.velocity=new Vector2 ( speed,thisRig.velocity.y);
			}
			else
				if(PlayerGO.transform.position.x<this.transform.position.x)//敌人向左移动
				{
					thisRig.velocity= new Vector2 (-1*speed,thisRig.velocity.y);
				}
		}
	}
	}

    public void EnemyDestory()
    {
        if (!bloodOpen)
        {
             StartCoroutine(BloodBurst());
        }

    }

    private IEnumerator BloodBurst()
    {
        bloodOpen = true;
        CutCount += 1;
        if (CutCount <= 3)
        {

            if (this.transform.localScale.x == -0.9f || this.transform.localScale.x == -0.7f)
            {
                BloodL = GameObject.Instantiate(BloodShowL, transform.position, transform.rotation) as GameObject;
                BloodL.transform.localEulerAngles = new Vector3(0, 90, 0);


                BloodL.transform.localScale = new Vector3(-1.0f, 1, 1);
            }
            else if (this.transform.localScale.x == 0.9f || this.transform.localScale.x == 0.7f)
            {
                BloodR = GameObject.Instantiate(BloodShowR, transform.position, transform.rotation) as GameObject;
                BloodR.transform.localEulerAngles = new Vector3(0, 90, 0);


                BloodR.transform.localScale = new Vector3(1.0f, 1, 1);
            }
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
            // yield return new WaitForSeconds(0.5f);
            if (CutCount == 1)
            {
                forwardflag = Player.transform.position.x - transform.position.x;

                //time = Vector3.Distance(transform.position, Player.transform.position) / flydistance;


                // Deadforward = new Vector3((Player.transform.position.x - transform.position.x) / time,
                // (Player.transform.position.y - transform.position.y) / time - 0.5f * g * time, (Player.transform.position.z - transform.position.z) / time);

                Dead();
            }
            Destroy(BloodR, 1.5f);
            Destroy(BloodL, 1.5f);
            yield return new WaitForSeconds(0.5f);
            bloodOpen = false;
        }
    }

    private void Dead()
    {
        // Destroy(gameObject, 0.3f);
        StartCoroutine("DelayGround");
        DeadFlag = true;
        this.GetComponent<Animator>().SetBool("Dead", true);
    }

    private IEnumerator DelayGround()
    {
        yield return new WaitForSeconds(1.0f);
        ground = true;
    }
}
