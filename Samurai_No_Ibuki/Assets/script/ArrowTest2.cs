using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTest2 : MonoBehaviour {



    public float TranslateSpeed = 0.1f;
    float TranslateSpeedTime = 0.1f;
    public float TranslateTime = 10.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        TranslateSpeedTime += 0.1f;
        if ((TranslateSpeedTime > 3.0f) && (TranslateSpeedTime <= TranslateTime))
            transform.Translate(Vector3.right * TranslateSpeed);
        else if (TranslateSpeedTime > TranslateTime)
        {

            TranslateSpeedTime = 0.1f;
            Destroy(this.gameObject);
        }

    }
}
