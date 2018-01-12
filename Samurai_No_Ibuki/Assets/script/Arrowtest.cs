using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowtest : MonoBehaviour {


    public float TranslateSpeed = 0.1f;
    float TranslateSpeedTime = 0.1f;
    public float TranslateTime = 10.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


            TranslateSpeedTime += 0.1f;
            transform.Translate(Vector3.right * TranslateSpeed);
            if (TranslateSpeedTime > TranslateTime)
            {
               
                TranslateSpeedTime = 0.1f;
                Destroy(this.gameObject);
            }
        
    }
}
