using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kiseki : MonoBehaviour {

    
    public GameObject sword;
    public GameObject ult;
    bool Flick = false;
    public float AtkDestoryTime;
    GameObject clone;
    GameObject UltClone;
    public AudioClip isUltSound;

    Vector3 StartPos;
    Vector3 EndPos;
   
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {


        if (GameObject.Find("Player").GetComponent<Move>().isStop)
        {
            return;
        }
        else if (GameObject.Find("Player").GetComponent<Player_Hp>().Hp == 0)
        {
            return;
        }

        //  startflickspeed_Xとstartflickspeed_Yを引っ張ってくる
        Move parent_move = transform.parent.GetComponent<Move>();
        Move parent_ult = transform.parent.GetComponent<Move>();


        //  float KisekiRange_X  = (parent_move.StartFlickSpeed_X + transform.parent.transform.position.x)/2;

        float KisekiScale_X = parent_move.StartFlickSpeed_X;
        float KisekiScale_Y = parent_move.StartFlickSpeed_Y;
        bool ShowUlt = parent_ult.Ult;
        

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartPos = Input.mousePosition;
            StartPos = new Vector3(Camera.main.ScreenToWorldPoint(StartPos).x, Camera.main.ScreenToWorldPoint(StartPos).y, 0);

            Flick = true;

            Invoke("FlickOff", 0.25f);
            return;
        }

        if (Input.GetMouseButton(0) && ShowUlt == true || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary && ShowUlt == true)
        {
            StartPos = Input.mousePosition;
            StartPos = new Vector3(Camera.main.ScreenToWorldPoint(StartPos).x, Camera.main.ScreenToWorldPoint(StartPos).y, 0);

            return;
        }

        if (Input.GetMouseButtonUp(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            EndPos = Input.mousePosition;

            EndPos = new Vector3(Camera.main.ScreenToWorldPoint(EndPos).x, Camera.main.ScreenToWorldPoint(EndPos).y, 0);

            
            //Xの長さ
            float Range_X = EndPos.x - StartPos.x;
            //Yの長さ
            float Range_Y = EndPos.y - StartPos.y;
            
            //角度
            float rd;

            if (flick())
            {

                if (Range_X > 0)
                {
                    rd = Mathf.Atan2(Range_Y, Range_X) * Mathf.Rad2Deg;
                    if (GameObject.FindWithTag("Energy").GetComponent<Image>().fillAmount <= 0)
                    { return; }
                    clone = GameObject.Instantiate(sword, new Vector3(this.transform.position.x, this.transform.position.y, 0), this.transform.rotation) as GameObject;
                    if (KisekiScale_X < 1f)
                    {
                        clone.transform.localScale = new Vector3(KisekiScale_Y, 1, 0);
                    }
                    else if (KisekiScale_X > 1f)
                    {
                        clone.transform.localScale = new Vector3(KisekiScale_X, 1, 0);
                    }

                    clone.transform.localEulerAngles = new Vector3(0, 0, rd);
                }
                else if (Range_X < 0)
                {
                    rd = Mathf.Atan2(-Range_Y, -Range_X) * Mathf.Rad2Deg;
                    if (GameObject.FindWithTag("Energy").GetComponent<Image>().fillAmount <= 0)
                    { return; }
                    clone = GameObject.Instantiate(sword, new Vector3(this.transform.position.x, this.transform.position.y, 0), this.transform.rotation) as GameObject;
                    if (KisekiScale_X > -1f)
                    {
                        clone.transform.localScale = new Vector3(-KisekiScale_Y, 1, 0);
                    }
                    else if (KisekiScale_X < -1f)
                    {
                        clone.transform.localScale = new Vector3(KisekiScale_X, 1, 0);
                    }
                    clone.transform.localEulerAngles = new Vector3(0, 0, rd);
                }

                GameObject.FindWithTag("Energy").GetComponent<Image>().fillAmount -= 0.25f;

                Destroy(clone, AtkDestoryTime);
            }

            if (ShowUlt == true )
            {
                if (Range_X >= 0)
                {
                    rd = Mathf.Atan2(Range_Y, Range_X) * Mathf.Rad2Deg;
                    UltClone = GameObject.Instantiate(ult, new Vector3(this.transform.position.x, this.transform.position.y, 0), this.transform.rotation) as GameObject;
                    UltClone.transform.localScale = new Vector3(2.5f,  2.5f, 1);

                    UltClone.transform.localEulerAngles = new Vector3(0, 0, rd);

                }
               else if (Range_X <= 0)
                {
                    rd = Mathf.Atan2(-Range_Y, -Range_X) * Mathf.Rad2Deg;

                    UltClone = GameObject.Instantiate(ult, new Vector3(this.transform.position.x, this.transform.position.y, 0), this.transform.rotation) as GameObject;
                    UltClone.transform.localScale = new Vector3(-2.5f, -2.5f, 1);

                    UltClone.transform.localEulerAngles = new Vector3(0, 0, rd);

                }

                GameObject.Find("UltFire").transform.localScale = new Vector3(0, 0, 1);
                SoundManager.instance.UltUse(isUltSound);
                SoundManager.instance.Ultvol = 0.6f;
                SoundManager.instance.Ultpitch = Random.Range(0.95f, 1.05f);
                Invoke("UltColse", 0.5f);

                
                return;

            }

        }
        
        return;

    }

    void FixedUpdate()
    {
        
    }
    private void UltColse()
    {
        Destroy(UltClone, 0.5f); 
    }

    private void FlickOff()
    {
        Flick = false;
        return;
    }

    private bool flick()
    {
        return Flick;
    }

}
