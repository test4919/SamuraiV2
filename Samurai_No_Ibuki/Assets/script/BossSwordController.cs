using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwordController : MonoBehaviour {

	public GameObject iai;
	public float desTimer=0.6f;
	public bool Left;
    public AudioClip normalAttack;
    

	public bool start;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, desTimer);
		if (start) {
			Invoke ("IAi", 0.5f);
		}
		
	}
	void IAi()
	{
		GameObject go = GameObject.Instantiate (iai, this.transform.position, this.transform.rotation) as GameObject;
		if (!Left)
        {
            go.transform.localScale = new Vector3(-1, 1, 1);
            BossSound.instance.OneTimeBossSound(normalAttack);
        }

        Destroy (go, 0.35f);
	}
}
