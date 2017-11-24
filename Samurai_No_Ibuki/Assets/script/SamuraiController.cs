using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : MonoBehaviour {
	public enum EnemyType{
		samurai=0,
		ninja=1,
		takoninja=2,
	};
	public EnemyType eType;
	public GameObject surikengbo;
	Animator thisAnimator;
	float timer=0f;
    GameObject suriken;
    GameObject player;
    public bool inAtkRange;

    public AudioSource[] soundlist;
    public AudioSource attacked;
    public AudioSource drop;

	// Use this for initialization
	void Start () {
		thisAnimator = this.GetComponent<Animator> ();
        player = GameObject.FindGameObjectWithTag("Player");
        soundlist = GetComponents<AudioSource>();
        attacked = soundlist[0];
        drop = soundlist[1];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SamuraiIdle()
	{
        this.GetComponent<Rigidbody2D> ().velocity = new Vector2(0,this.GetComponent<Rigidbody2D> ().velocity.y);
		thisAnimator.SetBool ("EWait", true);
		thisAnimator.SetBool ("ERun", false);
	}
	public void SamuraiRun()
	{
     
        thisAnimator.SetBool ("EWait", false);
		thisAnimator.SetBool ("ERun", true);
	}
	public void SamuraiAttack()
	{
        if (eType == EnemyType.ninja)
        {
            this.thisAnimator.SetTrigger("EA");
            if (this.transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(-6, 0);
            else if (this.transform.position.x < GameObject.FindGameObjectWithTag("Player").transform.position.x)
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(6, 0);
        }
        if (eType == EnemyType.samurai)
        {
            this.thisAnimator.SetTrigger("EA");
            StartCoroutine("DelayR");
            
        }

    }
	public void Skill()
	{	//释放角色特殊技能 例：武士（跳劈）
		switch (eType) {
		case EnemyType.samurai:
			SamuraiSkill ();
			break;
		case EnemyType.ninja:
			NinjaSkill ();
			break;
		case EnemyType.takoninja:
			TakoNinjaSkill ();
			break;
		}
	}
	void SamuraiSkill()
	{

	}
	void NinjaSkill()
	{
		this.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		thisAnimator.SetTrigger("E3DA");
		Invoke ("NinjaSuriken", 0.25f);
	}
	void NinjaSuriken()
	{
        float ninja_rndx;float ninja_rndy;
        ninja_rndx = Random.Range(-8.0f,8.0f);
        ninja_rndy = Random.Range(5.0f, 11.0f);
		FlashBy (new Vector2(ninja_rndx,ninja_rndy));
		this.GetComponent<EnemyAI> ().pause = false;
		Invoke ("ThrowSuriken", 0.5f);
		//发射手里剑
	}
	void ThrowSuriken()
	{
		suriken=GameObject.Instantiate (surikengbo,this.transform.position,this.transform.rotation);
        //Invoke("DestorySuriken", 1);
        Destroy(suriken, 2f);
	}

   
	void TakoNinjaSkill()
	{
	}
	public void NinjaDrop()
	{
		thisAnimator.SetTrigger ("E3Drop");
    }
	void OnTriggerEnter2D(Collider2D other)
	{

	}
	void OnCollisionEnter2D(Collision2D other)
	{
		//Debug.Log (other.transform.tag);
		if (other.transform.CompareTag ("Ground")) {
            //碰到地面的操作

            if (eType == EnemyType.ninja && (thisAnimator.GetCurrentAnimatorStateInfo (0).IsName ("E3Drop") || thisAnimator.GetCurrentAnimatorStateInfo (0).IsName ("E3DashAttack"))) {
				thisAnimator.SetTrigger ("EGround");
                SamuraiIdle ();
                if (!drop.isPlaying)
                {
                    drop.Play();
                }

            }
		}
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "player")
        {
            if (inAtkRange)
            {
                GameObject.Find("Player").GetComponent<Player_Hp>().Hp -= 10;
            }
        }
    }

   
	void FlashBy(Vector2 flashVetor)
	{
		this.transform.position = new Vector3 (this.transform.position.x + flashVetor.x, this.transform.position.y + flashVetor.y, this.transform.position.z);

	}

    IEnumerator DelayR()
    {
        yield return new WaitForSeconds(0.5f);
        inAtkRange = true;
        StopCoroutine("DelayR");
    }
}
