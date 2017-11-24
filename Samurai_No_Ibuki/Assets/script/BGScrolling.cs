using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScrolling : MonoBehaviour {
    public Transform[] background;
    private float[] parallaxScroll;
    public float smoothing_X=25.0f;
    public float smoothing_Y = 18.0f;
    public GameObject chat;
   

    private Vector3 preCameraPos;

    private float alpha = 1.0f;

    // Use this for initialization
    void Start () {
       
        
        preCameraPos = transform.position;
        parallaxScroll = new float[background.Length];
        for (int i = 0; i < background.Length; i++)
        {
            parallaxScroll[i] = background[i].position.z * -1;
        }

        
    }
	
	// Update is called once per frame
	void LateUpdate () {


        alpha -= 0.3f * Time.deltaTime;

        GameObject.Find("StartColor").GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        if (alpha <= 0)
        {
            alpha = 0f;
            onetime();
        }

        Vector3 parallax_X = (preCameraPos - transform.position) * (parallaxScroll[0] / smoothing_X);
        Vector3 parallax_Y = (preCameraPos - transform.position) * (parallaxScroll[0] / smoothing_Y);

        background[0].position = new Vector3(background[0].position.x + parallax_X.x, background[0].position.y + parallax_Y.y, background[0].position.z);
        
        if (transform.position.x > 75.1f)
        {
            background[1].position = new Vector3(background[1].position.x, background[1].position.y, background[1].position.z);
        }
        else
        {
            background[1].position = new Vector3(background[1].position.x + parallax_X.x * -1.5f, background[1].position.y, background[1].position.z);
        }


        if (transform.position.x > 92.0f && transform.position.y > 23.0f)
        {
            background[2].position = new Vector3(background[2].position.x + parallax_X.x * -1.5f, background[2].position.y, background[2].position.z);
            
        }
       

        preCameraPos = transform.position;
	}

    private void onetime()
    {
        if (GameObject.Find("TextController").GetComponent<TextController>().chatchs == false)
        {
            chat.SetActive(false);
        }
        else
        {
            chat.SetActive(true);
        }
        
    }
}
