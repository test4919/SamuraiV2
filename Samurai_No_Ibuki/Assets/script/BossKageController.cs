using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKageController : MonoBehaviour {

	// Use this for initialization
	SpriteRenderer thisSprite;
	float alpha = 1f;
	void Start () {
		thisSprite = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		alpha -= Time.deltaTime*0.5f;
		thisSprite.color=new Color(1,1,1,alpha);
		Destroy (this.gameObject, 2f);
	}
}
