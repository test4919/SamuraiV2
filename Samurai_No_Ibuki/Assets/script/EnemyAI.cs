using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Vector2 Deadforward;
    public GameObject Player;
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

    }
	void FindPlayer()
	{
		PlayerGO = GameObject.FindGameObjectWithTag ("Player");
	}
	void EnemyAttack()
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

        float time = 0.0f;
        Deadforward = transform.position - Player.transform.position;
        Dead();
        if (DeadFlag)
        {
            time += Time.deltaTime;
            transform.Translate(Deadforward * Time.deltaTime * 0.5f);
            /* if (time >= 1.0f)
             {
                 Destroy(this.gameObject);
             }*/

        }

        //Destroy(gameObject, 0.3f);
        Destroy(BloodR, 1.5f);
        Destroy(BloodL, 1.5f);
        yield return new WaitForSeconds(0.5f);
        bloodOpen = false;
    }

    private void Dead()
    {
        Destroy(gameObject, 0.3f);
        DeadFlag = true;
        this.GetComponent<Animator>().SetBool("Dead", true);
    }
}
