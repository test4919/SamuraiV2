using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Event : MonoBehaviour {
    GameObject Arrow;
    GameObject player;
    public bool ArrowShow=false;
    public GameObject showHp_Boss;
    public GameObject BossChat;
    public GameObject Leaf;
    Vector3 StartPosLeft;
    Vector3 StartPosRight;
    

    // Use this for initialization
    void Start () {
        Arrow = GameObject.Find("yajirusi");
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {

        if (player.transform.position.x >= 70)
        {
            GameObject.Find("MEnemy1").GetComponent<TakoController>().enabled = true;
            GameObject.Find("MEnemy2").GetComponent<TakoController>().enabled = true;
        }

        if (player.transform.position.x >= 62 && Camera.main.transform.position.x <= 75)
        {
            Arrow.GetComponent<SpriteRenderer>().enabled = false;
            ArrowShow = false;
            Arrow.transform.position = new Vector3(75, 6, 0);
            return;
        }
        else if (player.transform.position.x >= 80&& player.transform.position.y <= 25)
        {
            ArrowShow = true;
            Arrow.GetComponent<SpriteRenderer>().enabled = true;
            Arrow.transform.eulerAngles = new Vector3(0, 0, 62);
        }
        else if (player.transform.position.y >= 25)
        {
            Arrow.GetComponent<SpriteRenderer>().enabled = false;
            ArrowShow = false;
            GameObject.Find("MEnemy1").GetComponent<TakoController>().enabled = false;
            GameObject.Find("MEnemy2").GetComponent<TakoController>().enabled = false;
        }
        


        if (ArrowShow)
        {
            if (Time.time % 1.5 > 0.5f)
            {
                Arrow.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                Arrow.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
       
    }

    public void ShowBlock01()
    {
        GameObject.Find("Player").GetComponent<Move>().BattleStart();
        GameObject.Find("Right01").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("Left01").GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CreateEnemy>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<CamereControl>().ChgCameraPlc();
        CreateWaterFallFromResources();
    }

    public void HideBlock01()
    {
        GameObject.Find("Player").GetComponent<Move>().isStop = true;
        GameObject.Find("Main Camera").GetComponent<CamereControl>().StartCoroutine("showSehai");
        CreateFromResources1();
    }
    
    public void ShowBlock02()
    {
        GameObject.Find("Player").GetComponent<Move>().BattleStart();
        GameObject.Find("Right02").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("Left02").GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CreateEnemy>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<CamereControl>().ChgCameraPlc();
        Leaf.SetActive(true);
    }

    public void HideBlock02()
    {
        Debug.Log("Hide2");
        GameObject.Find("Player").GetComponent<Move>().isStop = true;
        GameObject.Find("Main Camera").GetComponent<CamereControl>().StartCoroutine("showSehai");
        CreateFromResources2();
        Leaf.SetActive(false);
    }
    
    public void ShowBlock03()
    {
        GameObject.Find("Player").GetComponent<Move>().BattleStart();
        GameObject.Find("Right03").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("Left03").GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CreateEnemy>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<CamereControl>().ChgCameraPlc();
    }

    public void HideBlock03()
    {
        Debug.Log("Hide3");
        GameObject.Find("Player").GetComponent<Move>().isStop = true;
        GameObject.Find("Main Camera").GetComponent<CamereControl>().StartCoroutine("showSehai");
        CreateBossFromResources();
    }

    public void ShowBossBlock()
    {
        showHp_Boss.SetActive(true);
        GameObject.Find("Main Camera").GetComponent<CamereControl>().ChgCameraPlc();
        StartCoroutine(StartChat());
        
        GameObject.Find("Player").GetComponent<Move>().BattleStart();
        GameObject.Find("BossBlock").GetComponent<BoxCollider2D>().enabled = true;
        
    }

    private IEnumerator StartChat()
    {
        yield return new WaitForSeconds(1f);
        BossChat.SetActive(true);
        GameObject.Find("BossTextController").GetComponent<BossTextController>().enabled = true;
        }

    void CreateFromResources1()
    {
        GameObject P2 = Resources.Load("Part2") as GameObject;

        Instantiate(P2);
    }

    void CreateFromResources2()
    {

        GameObject P3 = Resources.Load("Part3") as GameObject;
        Instantiate(P3);
    }

    void CreateBossFromResources()
    {
        Debug.Log("PartB");
        GameObject BossP = Resources.Load("Boss") as GameObject;
        Instantiate(BossP);
    }

    void CreateWaterFallFromResources()
    {
        GameObject WaterP = Resources.Load("WaterFall") as GameObject;
        Instantiate(WaterP);
    }

}
