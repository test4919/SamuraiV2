using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamereControl : MonoBehaviour {

    [SerializeField]
    public float xMax;
    [SerializeField]
    public float yMax;
    [SerializeField]
    public float xMin;
    [SerializeField]
    public float yMin;

    private Transform player;
    private Transform sePic;
    private Transform haiPic;
    private Transform syobu_right;
    public GameObject StartFight;
    private Transform uePic;
    private Transform migiPic;
    private float a;
    bool ScaleChg = false;
    private float SmoothSpd = 0.125f;
    private Vector3 offset = new Vector3(0, 0, -10f);

    public Transform lookAt;
    public Transform lookAt2;
    public Transform lookAt3;
    public Transform lookAtBoss;
    public bool smooth01 = false;
    public bool smooth02 = false;
    public bool smooth03 = false;
    public bool smoothBoss = false;
    public float CameraPlaceX1;
    public float CameraPlaceX2;
    public float CameraPlaceX3;
    public float CameraPlaceX4;
    public float CameraPlaceX5;
    public float CameraPlaceX6;
    public float CameraPlaceX7;
    public float moveSpd;
    public float showSpd;
    public float sehaiSpd;
    public AudioClip sehaiSound;
    public AudioClip Battlestart;
    public AudioClip BgmD;
    public GameObject WaterFall;
    public GameObject WaterCloud;
    public bool BossOpen;
   
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").transform;
        sePic = GameObject.Find("se").transform;
        haiPic = GameObject.Find("hai").transform;
        uePic = GameObject.Find("shoubu_ue").transform;
        migiPic = GameObject.Find("shoubu_migi").transform;
        BossOpen =false;
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    void LateUpdate()
    {
        Vector3 SEposition = lookAt.position + offset;
        Vector3 SEposition2 = lookAt2.position + offset;
        Vector3 Seposition3 = lookAt3.position + offset;
        Vector4 SeeBossPosition = lookAtBoss.position + offset;


        if (transform.position.x > 70.0f)
        {
           // WaterFall.SetActive(true);
            WaterCloud.SetActive(true);
        }

        //look1
        if (smooth01 == true)
        {
            smooth02 = false;
            smooth03 = false;
            smoothBoss = false;
            transform.position = Vector3.Lerp(transform.position, SEposition, SmoothSpd);
           
            startsyobu();
            
        }
        
        //look2
        else if (smooth02)
        {
            smooth01 = false;
            smooth03 = false;
            smoothBoss = false;
            transform.position = Vector3.Lerp(transform.position, SEposition2, SmoothSpd);

            startsyobu();

        }
        else if (smooth03)
        {
            smooth01 = false;
            smooth02 = false;
            smoothBoss = false;
            transform.position = Vector3.Lerp(transform.position, Seposition3, SmoothSpd);

            startsyobu();
        }
        else if (smoothBoss)
        {
            smooth01 = false;
            smooth02 = false;
            smooth03 = false;
            transform.position = Vector3.Lerp(transform.position, SeeBossPosition, SmoothSpd);

            if (BossOpen)
            {
                startsyobu();
            }

        }

        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(player.position.x, xMin, xMax), Mathf.Clamp(player.position.y, yMin, yMax), transform.position.z), SmoothSpd);
            StartFight.transform.localPosition = new Vector3(0.0f, 4.69f, 1f);
            StartFight.transform.localScale = new Vector3(0f, 0f, 1f);
            a = 0;
        }



        if (player.transform.position.y > 26.0f)
        {
            xMax = 324.0f;
        }
        else { xMax = 84f; yMin = -1f; }

        if (player.transform.position.x > 64.0f)
        {
            yMax = 32.0f;
        }
        else { yMax = 0.5f; }

        if (player.transform.position.x > 83.0f && player.transform.position.y > 23.0f)
        {
            yMin = 32.0f;

        }
        else { yMin = -1f; }

       

    }

    private void startsyobu()
    {
        a += showSpd * Time.deltaTime;
        
        StartFight.transform.localScale = new Vector3(a, a, 1f);
        if (a >= 1f)
        {
            StartFight.transform.localScale = new Vector3(1f, 1f, 1f);
            
            if (ScaleChg)
               {
                
                StartCoroutine("Turnfalse");
                
                }
            else
            {
                StartFight.transform.localScale = new Vector3(0f, 4.69f, 1f);
                uePic.transform.localPosition = new Vector3(-0.54f, 0.45f, 1f);
                migiPic.transform.localPosition = new Vector3(0.09f, 0.07f, 1f);
            }
        }
    }

    private IEnumerator Turnfalse()
    {
        
        yield return new WaitForSeconds(0.5f);
        uePic.localPosition = new Vector3(-0.56f, 0.45f, 1f);
        migiPic.localPosition = new Vector3(0.09f, 0.07f, 1f);
        
        for (int i = 0; i < 25; i++)
        {
            migiPic.transform.Translate(moveSpd, 0, 0);
            uePic.transform.Translate(-moveSpd, 0, 0);
            //migiPic.localPosition += new Vector3(moveSpd, 0, 0);
            //uePic.localPosition += new Vector3(-moveSpd, 0, 0);
            //yield return new WaitForSeconds(0.00001f);
        }
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }

        yield return new WaitForSeconds(0.5f);
        ScaleChg = false;
        GameObject.Find("Player").GetComponent<Move>().isStop = false;
        if (smoothBoss)
        {
            GameObject.Find("Boss(Clone)").GetComponent<BossController>().enabled = true;
        }
     
    }

    public IEnumerator showSehai()
    {
        sePic.localPosition = new Vector3(-200, 44.4f, 8);
        haiPic.localPosition = new Vector3(200, 44.4f, 8);
        
        for ( int i = 0; i < 10; i++)
        {
            sePic.localPosition += new Vector3(sehaiSpd, 0, 0);
            haiPic.localPosition += new Vector3(-sehaiSpd, 0, 0);
            yield return new WaitForSeconds(0.00001f);
        }

        SoundManager.instance.SingleSound(sehaiSound);
        SoundManager.instance.vol = 0.8f;

        yield return new WaitForSeconds(2f);

        GameObject.Find("Player").GetComponent<Move>().isStop = false;
        if (player.transform.position.x < 82f)
        {
            GameObject.Find("Right01").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("Left01").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("Event1").GetComponent<Block_Event>().ArrowShow = true;
            smooth01 = false;
        }
        else if (player.position.x > 83 && player.position.x < 160)
        {
            GameObject.Find("Right02").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("Left02").GetComponent<BoxCollider2D>().enabled = false;
            smooth02 = false;
        }
        else if (player.position.x > 160 && player.position.x < 255)
        {
            GameObject.Find("Right03").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("Left03").GetComponent<BoxCollider2D>().enabled = false;
            smooth03 = false;
        }
        else if (player.position.x > 255 && player.position.x < 315)
        {
            
        }
      
        sePic.localPosition = new Vector3(-200f, 44.4f, 8f);
        haiPic.localPosition = new Vector3(210.6f, 44.4f, 8f);

        StopCoroutine("showSehai");

    }


    

    public void ChgCameraPlc()
    {
        if (player.transform.position.x <CameraPlaceX1)
        {
            smooth01 = true;
            ScaleChg = true;
            
        }
        else if (player.position.x > CameraPlaceX2 && player.position.x < CameraPlaceX3)
        {
            smooth02 = true;
            ScaleChg = true;
        }
        else if (player.position.x > CameraPlaceX4 && player.position.x < CameraPlaceX5)
        {
            smooth03 = true;
            ScaleChg = true;
        }
        else if (player.position.x > CameraPlaceX6 && player.position.x < CameraPlaceX7)
        {
            smoothBoss = true;
            ScaleChg = true;
        }
        
    }
   
}
