using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakoController : MonoBehaviour {

	public float chgTimer;
	float attackTimer;

	public GameObject suriken;
	public GameObject ninjaGo;
    public GameObject TakoBloodR;
    public GameObject TakoBloodL;

    public float minattackDist;

	public float attackCoolDown;

	public bool death;

	public  bool canAttack;
	GameObject ninjaingo;
	GameObject player;
    GameObject Suriken;
    GameObject takobloodR;
    GameObject takobloodL;
    bool bloodOpen = false;

    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		canAttack = false;
	}
	
	// Update is called once per frame
	void Update () {
		chgTimer += Time.deltaTime;
		if (!canAttack)
			attackTimer += Time.deltaTime;
		if (attackTimer >= attackCoolDown)
			canAttack = true;
		LookAtPlayer ();
		if (Mathf.Abs (player.transform.position.x - this.transform.position.x) > minattackDist && canAttack) {
			Attack ();
		} else
			Move ();
		if (chgTimer < 0.5f)
			MoveUp ();
		else if (chgTimer < 1.0f)
			MoveDown ();
		else
			chgTimer = 0;
		if (death)
			Death ();
		
	}
	void Attack()
	{
		canAttack = false;
		attackTimer = 0f;
		Invoke ("ThrowSuriken", 0.33f);
		Invoke ("ThrowSuriken", 1f);
		this.GetComponent<Animator>().SetTrigger("EAttack");
	}
	void Move()
	{
	}
	void ThrowSuriken()
	{
		Suriken =GameObject.Instantiate (suriken, this.transform.position, this.transform.rotation);
        Destroy(Suriken, 2f);
	}
	void LookAtPlayer()
	{
		if (player.transform.position.x > this.transform.position.x)
			this.transform.localScale = new Vector3 (-1, 1, 1);
		else
			this.transform.localScale = new Vector3 (1, 1, 1);
	}
	void MoveUp()
	{
		this.transform.Translate (new Vector3 (0, 0.5f * Time.deltaTime, 0));
	}
	void MoveDown()
	{
		this.transform.Translate (new Vector3 (0, -0.5f * Time.deltaTime, 0));
	}
	void MoveLeft()
	{
		this.transform.Translate (new Vector3 (-0.5f * Time.deltaTime, 0, 0));
	}
	void MoveRight()
	{
		this.transform.Translate (new Vector3 (0.5f * Time.deltaTime, 0, 0));
	}
	public void Death()
    {
        if (!bloodOpen)
        {
            StartCoroutine(BloodBurst());
        }
        //death = false;

        //if (!GetComponent<AudioSource>().isPlaying)
        //{
        //    GetComponent<AudioSource>().Play();
        //}
        //if (transform.localScale.x == 1)
        //{
        //    takobloodR = GameObject.Instantiate(TakoBloodR, transform.position, transform.rotation) as GameObject;
        //    takobloodR.transform.localEulerAngles = new Vector3(0, 90, 0);
        //}
        //else if (transform.localScale.x == -1)
        //{
        //    takobloodL = GameObject.Instantiate(TakoBloodL, transform.position, transform.rotation) as GameObject;
        //    takobloodL.transform.localEulerAngles = new Vector3(0, 90, 0);
        //}
        //Invoke("isDeath", 0.2f);

    }

    private IEnumerator BloodBurst()
    {
        bloodOpen = true;
        death = false;

        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
        if (transform.localScale.x == 1)
        {
            takobloodR = GameObject.Instantiate(TakoBloodR, transform.position, transform.rotation) as GameObject;
            takobloodR.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (transform.localScale.x == -1)
        {
            takobloodL = GameObject.Instantiate(TakoBloodL, transform.position, transform.rotation) as GameObject;
            takobloodL.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        Invoke("isDeath", 0.2f);
        yield return new WaitForSeconds(0.5f);
        bloodOpen = false;
    }

    void isDeath()
    {
        ninjaingo = GameObject.Instantiate(ninjaGo, this.transform.position, this.transform.rotation) as GameObject;
        ninjaingo.GetComponent<Animator>().SetTrigger("E3Drop");
        Destroy(this.gameObject);
        Destroy(takobloodR, 1f);
        Destroy(takobloodL, 1f);

    }

   
}
