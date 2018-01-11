using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialText : MonoBehaviour {


    public GameObject panel;
    public GameObject tutorialBlack;
    private Transform player;
    public bool flag = false;
    public GameObject ArrowPre;

    public Transform Samurai;
    public GameObject Arrow;
    bool arrowflag = false;
    bool trrigerflag = false;
    
    float TranslateSpeedTime = 0.1f;
    public float TranslateTime = 5.0f;
    float countTime = 0.0f;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {

        if (countTime > 0.5f)
        {
            GameObject.Find("Player").GetComponent<Move>().enabled = true;
            GameObject.Find("Swordobject").GetComponent<Kiseki>().enabled = true;

        }

        if ((Input.GetMouseButtonDown(0)) && (!flag)&&(arrowflag))
        {
            Debug.Log("test3");
            tutorialEnd();
            flag = true;
           
        }
        else if((!flag) && (arrowflag))
        {
            TranslateSpeedTime += 0.1f;
            countTime += 0.1f;
            if ((TranslateSpeedTime > TranslateTime) && (arrowflag))
            {
                //Debug.Log("111");
                Instantiate(Arrow, new Vector3(player.position.x + 2, player.position.y, 1f), transform.rotation);
                Instantiate(ArrowPre, new Vector3(player.position.x + 2, player.position.y + 1.5f, 1f), transform.rotation);
                TranslateSpeedTime = 0.1f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.name == "Player")&&(!trrigerflag))
        {
            GameObject.Find("Player").GetComponent<Move>().enabled = false;
            GameObject.Find("Swordobject").GetComponent<Kiseki>().enabled = false;
            tutorialBlack.SetActive(true);
            panel.SetActive(true);
            Instantiate(Samurai, new Vector3(player.position.x + 10, player.position.y, 1f), transform.rotation);
            GameObject.FindWithTag("enemy").GetComponent<SamuraiController>().enabled = false;
            GameObject.FindWithTag("enemy").GetComponent<EnemyAI>().enabled = false;
            TranslateSpeedTime = 0.1f;
            Instantiate(Arrow, new Vector3(player.position.x + 2, player.position.y, 1f), transform.rotation);
            Instantiate(ArrowPre, new Vector3(player.position.x + 2, player.position.y + 1.5f, 1f), transform.rotation);
            arrowflag = true;
            trrigerflag = true;
        }
    }

    void tutorialEnd()
    {


        GameObject.Find("Player").GetComponent<Move>().enabled = false;
        GameObject.Find("Swordobject").GetComponent<Kiseki>().enabled = false;

        if (GameObject.FindWithTag("enemy") != null)
        {
            GameObject.FindWithTag("enemy").GetComponent<SamuraiController>().enabled = true;
            GameObject.FindWithTag("enemy").GetComponent<EnemyAI>().enabled = true;
        }
        //Debug.Log("test2");
        tutorialBlack.SetActive(false);
        panel.SetActive(false);
        Destroy(GameObject.FindWithTag("yajirushi"));
        //Destroy(this.gameObject);
    }
}

