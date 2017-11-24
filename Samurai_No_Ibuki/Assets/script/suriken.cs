using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suriken : MonoBehaviour {


    public Vector2 speed = new Vector2(0.05f, 0.05f);
	public float surikenSpeed;	
    public GameObject Player;
	public bool bossWave;
	Vector3 targetPos;
	float eug;
    private float rad;
    private Vector2 Position;

	float timer;
	void Start ()
    {
		timer = 0f;
		targetPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
        //rad = Mathf.Atan2(
        //   Player.transform.position.y - transform.position.y,
        //    Player.transform.position.x - transform.position.x);
		targetPos = new Vector3 (targetPos.x - this.transform.position.x, targetPos.y - this.transform.position.y, 0);
		targetPos = targetPos.normalized;
	}


    void Update()
    {
		timer += Time.deltaTime;
		if (timer > 5)
			Destroy (this.gameObject);
        //Position = transform.position;

        //Position.x += speed.x * Mathf.Cos(rad);
        //Position.y += speed.y * Mathf.Sin(rad);

        //transform.position = Position;
		LookAtPlayer();
		this.transform.parent.Translate(new Vector3(targetPos.x*surikenSpeed*Time.deltaTime,targetPos.y*surikenSpeed*Time.deltaTime,0));
    }
	void LookAtPlayer()
	{

		//eug = Mathf.Rad2Deg * Mathf.Atan (targetPos.y / targetPos.x);
		//if (targetPos.x > this.transform.position.x)
		//	eug -= 180;
		//this.transform.localEulerAngles = new Vector3 (0, 0, eug);
	}
}
