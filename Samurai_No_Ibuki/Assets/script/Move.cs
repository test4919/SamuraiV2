using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
   
    public float MovSpeed = 0.03f;
    public float StartFlickSpeed_X = 0;
    public float StartFlickSpeed_Y = 0;
    public float DragLength = 0.2f;
    public float moveVol;
    public bool Flick = false;
    public bool Drag = false;
    public bool Ult = false;
    public bool isStop = false;
    public bool IgnoreCol = false;
    public bool ShowHpBar = false;
    public bool isAtk = false;
    public bool mudeki=false;
    public bool icon = false;
    public AudioClip Atk;
    public AudioClip Run;
    public AudioClip JumpDown;
    public AudioClip slash;
    public LayerMask groundLayer;
    public LayerMask Kabe;
    public GameObject readyUlt;
    public GameObject HpBar;
    public GameObject HpBarBG;
   
    Animator animator;
    Vector3 StartPos;
    Vector3 EndPos;
    Vector3 prevPos;
    //GameObject hpbar;

    float FlickSpeed_X = 0;
    float FlickSpeed_Y = 0;
    float RecoverTimer;
    float timer;
    float checkTime;
    bool timeChecking;
    bool RecoverTimeChecking;
    bool checktimeChecking;
    bool ground = false;
    float hideBarTime;
    float a = 0;
    float b = 1;
    float c= 1;
    float d=0;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0;
        RecoverTimer = 0;
        
    }

    bool isGround()
    {

        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 3.25f;

        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider == null)
        {
            ground = true;
            animator.SetBool("DropPose", true);
        }
        else if (hit.collider!=null)
        {
            ground = true;
            animator.SetBool("DropPose", false);
            animator.SetTrigger("Wait");
           
            if (Drag == true)
            {
                return true;
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

            }
            //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
           // return true;
        }
        return false;
    }

    bool isKabe()
    {

        Vector2 position = transform.position;
        Vector2 directionRight = Vector2.right;
        Vector2 directionLeft = Vector2.left;
        float distance = 0.7f;
        float distance02 = -0.7f;

        Debug.DrawRay(position, directionRight, Color.red);
        Debug.DrawRay(position, directionLeft, Color.blue);
        RaycastHit2D hitRight = Physics2D.Raycast(position, directionRight, distance, Kabe);
        RaycastHit2D hitLeft  = Physics2D.Raycast(position, directionLeft, distance02, Kabe);
        if (hitRight.collider != null)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

            return true;
        }
        if (hitLeft.collider != null)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        
            return true;
        }
        return false;
    }

    public void BattleStart()
    {
        isStop = true;
    }


    void FixedUpdate()
    {
        if (isStop)
        {
            animator.SetTrigger("Wait");
            Drag = false;
            Flick = false;
            Ult = false;
        }

        //死亡的时候无法操作
         if (GetComponent<Player_Hp>().Hp == 0)
        {
            Drag = false;
            Flick = false;
            Ult = false;
            GetComponent<AudioSource>().Stop();
            //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            return;
        }

        prevPos = transform.position;

        //计算居合需要的时间
        if (timeChecking)
        {
            timer += Time.deltaTime;
        }

        //攻击用气槽的回复时间
        if (RecoverTimeChecking)
        {
            RecoverTimer += Time.deltaTime;
        }

        if (checktimeChecking)
        {
            checkTime += Time.deltaTime;
        }

        //下落时的动作
        if (!isGround())
        {
            Drag = false;
            animator.SetBool("DropPose", true);
            animator.ResetTrigger("Wait");
            IgnoreCol = true;
            Physics2D.IgnoreLayerCollision(11, 9, IgnoreCol);
            Physics2D.IgnoreLayerCollision(11, 13, IgnoreCol);
            Physics2D.IgnoreLayerCollision(11, 14, IgnoreCol);
            ground = false;
            if (Flick == false)
            {
                GetComponent<GhostSprites>().colorAlphaOverride = true;
                GetComponent<Rigidbody2D>().drag = 0.7f;
            }
        }

        showhpBar();
        move();
        flip();
		isGround ();
        isKabe();
        showHit();
        Recover();

        //Ignore Collider
        if (Drag)
        {
            Physics2D.IgnoreLayerCollision(11, 9, IgnoreCol);
            Physics2D.IgnoreLayerCollision(11, 13, IgnoreCol);
            Physics2D.IgnoreLayerCollision(11, 14, IgnoreCol);
        }
    }




    private void move()
    {
        
        // ボタンの押し下げの瞬間
        if (Input.GetMouseButtonDown(0) ||Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Flick = true;
            //mudeki = true;
            this.StartPos = Input.mousePosition;
            timeChecking = true;
            Drag = false;
            Ult = false;
            readyUlt.SetActive(false);

            Invoke("FlickOff", 0.25f);
            return;
        }
        // ボタンを離した瞬間
        if (Input.GetMouseButtonUp(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            this.EndPos = Input.mousePosition;

            Drag = false;
            timer = 0;
            IgnoreCol = false;
            checktimeChecking = false;
            checkTime = 0.0f;
           // Ult = false;

            //播放移动的声音
            if (GetComponent<AudioSource>().isPlaying)

            {
                GetComponent<AudioSource>().Pause();
            }

            if (Drag == false || Ult == false)
            {
                animator.SetBool("RunPose", false);
                animator.SetBool("UltPose", false);
                animator.SetBool("WaitPose", true);
            }

            GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;

            timer = 0;
            //Ult = false;
            // もし、フリックの状態だったら
            //攻击时的距离和声音
            if (flick())
            {
                if (mudeki)
                {
                    gameObject.tag = "mudeki";
                }

                GetComponent<Rigidbody2D>().drag = 1.0f;
                GetComponent<Rigidbody2D>().gravityScale = 1f;
              
                float FlickLength_X = (this.EndPos.x - this.StartPos.x);
                float FlickLength_Y = (this.EndPos.y - this.StartPos.y);

                StartFlickSpeed_X = FlickLength_X / 400.0f;
                StartFlickSpeed_Y = FlickLength_Y / 400.0f;
                FlickSpeed_X = StartFlickSpeed_X;
                if (FlickSpeed_X < -2||FlickSpeed_X>2)
                {
                    if (FlickSpeed_X < -3)
                    {
                        FlickSpeed_X = -2.5f;
                    }
                    else if (FlickSpeed_X > 3)
                    {
                        FlickSpeed_X = 2.5f;
                    }
                    else { FlickSpeed_X = StartFlickSpeed_X; }
                }

                FlickSpeed_Y = StartFlickSpeed_Y;
                if (Mathf.Abs(FlickLength_X) < 210.0f&& Mathf.Abs(FlickLength_X)>-210.0f)
                {
                    animator.SetTrigger("UpAtk");
                }

                else { animator.SetTrigger("Atk"); }
                animator.ResetTrigger("Stun");
                GetComponent<GhostSprites>().enabled = true;
                GetComponent<GhostSprites>().colorAlphaOverride = false;

                SoundManager.instance.SingleSound(Atk);

                return;
            }

            //居合的距离
            if (Ult == true && GameObject.Find("UltFire").transform.localScale.x >= 1.5f)
            {
                Drag = false;

                float FlickLength_X = (this.EndPos.x - this.StartPos.x);
                float FlickLength_Y = (this.EndPos.y - this.StartPos.y);

                StartFlickSpeed_X = FlickLength_X / 400.0f;
                StartFlickSpeed_Y = FlickLength_Y / 400.0f;
                FlickSpeed_X = StartFlickSpeed_X;
                FlickSpeed_Y = StartFlickSpeed_Y;
                if (FlickSpeed_X < -2 || FlickSpeed_X > 2)
                {
                    if (FlickSpeed_X < -3)
                    {
                        FlickSpeed_X = -2.5f;
                    }
                    else if (FlickSpeed_X > 3)
                    {
                        FlickSpeed_X = 2.5f;
                    }
                    else { FlickSpeed_X = StartFlickSpeed_X; }
                }
                animator.SetTrigger("Atk");

                GetComponent<GhostSprites>().enabled = true;
                GetComponent<GhostSprites>().colorAlphaOverride = false;
                
                return;
            }         
            return;
        }

        ////move
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            EndPos = Input.mousePosition;
            if ((StartPos - EndPos).sqrMagnitude >= DragLength)
            {
                // もし、ドラッグ中だったら
                //移动的判定
                if (Drag == true)
                {
                    animator.SetBool("AtkPose", false);
                    animator.SetBool("UltPose", false);
                    animator.SetBool("WaitPose", false);
                    animator.SetBool("RunPose", true);
                    
                    Ult = false;
                    IgnoreCol = true;

                    if (Mathf.Abs(EndPos.x) - Mathf.Abs(StartPos.x) > 0)
                    {
                        transform.Translate(13f * Time.deltaTime, 0, 0);
                    }
                    else if (Mathf.Abs(EndPos.x) - Mathf.Abs(StartPos.x) < 0)
                    {
                        transform.Translate(-13f * Time.deltaTime, 0, 0);
                    }

                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().Play();
                        GetComponent<AudioSource>().volume = moveVol;
                    }

                    GetComponent<GhostSprites>().colorAlphaOverride = true;
                    return;
                }
            }
        }

        //hold key
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            EndPos = Input.mousePosition;

            //居合的判定
            if (checkTime > 0.7f)
            {
                if ((StartPos - EndPos).sqrMagnitude == 0 && timer > 1.0f && GameObject.Find("UltFire").transform.localScale.x >= 1.5f)
                {
                    readyUlt.SetActive(true);
                    Drag = false;

                   

                    if ((StartPos - EndPos).sqrMagnitude == 0 && timer > 0.7f/*1.1f*/)
                    {
                        animator.SetBool("AtkPose", false);
                        animator.SetBool("RunPose", false);
                        animator.SetBool("WaitPose", false);
                        animator.SetBool("Stun", false);
                        animator.SetBool("UltPose", true);
                        Ult = true;
                        checktimeChecking = false;
                        checkTime = 0.0f;
                        return;
                    }
                }
            }
        }


        // ボタンを押し続けているとき
        if (Input.GetMouseButton(0))
        {
            EndPos = Input.mousePosition;
            checktimeChecking = true;
            //居合的判定

            if (checkTime > 0.4f)
            {
                if ((StartPos - EndPos).sqrMagnitude == 0 && timer > 1.0f && GameObject.Find("UltFire").transform.localScale.x >= 1.5f)
                {
                    readyUlt.SetActive(true);
                    Drag = false;

                    if ((StartPos - EndPos).sqrMagnitude == 0 && timer > 1.1f)
                    {
                        animator.SetBool("AtkPose", false);
                        animator.SetBool("RunPose", false);
                        animator.SetBool("WaitPose", false);
                        animator.SetBool("Stun", false);
                        animator.SetBool("UltPose", true);
                        Ult = true;
                        checktimeChecking = false;
                        checkTime = 0.0f;
                        return;
                    }
                }
            }
               
                else if ((StartPos - EndPos).sqrMagnitude >= DragLength)
                {
                    checktimeChecking = false;
                    checkTime = 0.0f;
                    // もし、ドラッグ中だったら
                    //移动的判定
                    if (Drag == true)
                    {
                        animator.SetBool("AtkPose", false);
                        animator.SetBool("UltPose", false);
                        animator.SetBool("WaitPose", false);
                        animator.SetBool("RunPose", true);


                        Ult = false;
                        IgnoreCol = true;

                        if (Mathf.Abs(EndPos.x) - Mathf.Abs(StartPos.x) > 0)
                        {
                            transform.Translate(13f * Time.deltaTime, 0, 0);
                        }
                        else if (Mathf.Abs(EndPos.x) - Mathf.Abs(StartPos.x) < 0)
                        {
                            transform.Translate(-13f * Time.deltaTime, 0, 0);
                        }

                        if (!GetComponent<AudioSource>().isPlaying)
                        {
                            GetComponent<AudioSource>().Play();
                            GetComponent<AudioSource>().volume = moveVol;
                        }

                        GetComponent<GhostSprites>().colorAlphaOverride = true;
                        return;
                    }
                
            }
            // 同じ位置ではなく、and 大技ポーズではないとき
           


        }



        transform.Translate(FlickSpeed_X, FlickSpeed_Y, 0);
        FlickSpeed_X *= 0.89f;
        FlickSpeed_Y *= 0.89f;

        return;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (flick() && other.gameObject.tag == "enemy" || flick() && other.gameObject.tag == "enemy2" ||
            flick() && other.gameObject.tag == "enemy3" || flick() && other.gameObject.tag == "Boss")
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        else if (other.gameObject.tag == "wall")
        {
            animator.SetTrigger("Wait");
            animator.SetBool("DropPose", false);
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            SoundManager.instance.SingleSound(JumpDown);
        }

       
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "wall")
        {
            if (Drag == true) { return; }
            GetComponent<BoxCollider2D>().isTrigger = false;
            //GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block_event")//进入战斗1
        {
            Debug.Log("EVENT START");
            GameObject.Find("Event1").GetComponent<Block_Event>().ShowBlock01();
            GameObject.Find("Event1").GetComponent<BoxCollider2D>().enabled = false;

        }
        else if (other.gameObject.tag == "Block_event2")//进入战斗2
        {
            Debug.Log("EVENT START2");
            GameObject.Find("Event2").GetComponent<Block_Event>().ShowBlock02();
            GameObject.Find("Event2").GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (other.gameObject.tag == "Block_event3")//进入战斗3
        {
            Debug.Log("EVENT START3");
            GameObject.Find("Event3").GetComponent<Block_Event>().ShowBlock03();
            GameObject.Find("Event3").GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (other.gameObject.tag == "Block_Boss")//进入Boss战斗
        {
            Debug.Log("Block_Boss");
            GameObject.Find("BossEvent").GetComponent<Block_Event>().ShowBossBlock();
            GameObject.Find("BossEvent").GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (other.gameObject.tag=="DownBattle")
        {
            Debug.Log("DownBattle");
            SceneManager.LoadScene("DownBattle");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("Exit");
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void showP()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
    
    private void FlickOff()
    {
        Flick = false;
        mudeki = false;
        gameObject.tag = "Player";
        //IgnoreCol = false;
        if (Ult == true) { return; }
        Drag = true;
        return;
    }
    
    public bool flick()
    {
        return Flick;
    }

    private void showHit()
    {

        var hitPic = GameObject.Find("hit").GetComponent<SpriteRenderer>();
        var EnergyBar = GameObject.FindWithTag("Energy").GetComponent<Image>();
        var hitBack = GameObject.Find("HitBack").GetComponent<SpriteRenderer>();
        c -=0.05f*Time.deltaTime;
        d += 0.3f*Time.deltaTime;
        if (EnergyBar.fillAmount <= 0.2f)
        {
            icon = true;
            if (icon == true)
            {
                hitBack.color = new Color(1, 1, 1, 1);
                hitPic.color = new Color(1, 1, 1, 1);
            
            }
        }
        else
        {
            icon = false;
            hitBack.color = new Color(1, 1, 1, 0);
            hitPic.color = new Color(1, 1, 1, 0);

        }
    }

    

    private void Recover()
    {
        //攻击用气槽回复
        var EnergyBar = GameObject.FindWithTag("Energy").GetComponent<Image>();
        var BackEnergyBar = GameObject.Find("BackEnergyBar ").GetComponent<Image>();
        //float RecoverAlpha=0.0f; 
        if (EnergyBar.fillAmount >= 0.5&&Flick==true)
        {
            if (EnergyBar.fillAmount <= 0.5)
            {
                mudeki = false;
            }
            else
            mudeki = true;
        }
       


        if (EnergyBar.fillAmount <=1)
        {
            RecoverTimeChecking = true;
            //Debug.Log(RecoverTimer);
            if (RecoverTimer >= 3)
            {

                if (Flick == true)
                {
                    RecoverTimer = 0;
                    return;
                }
               // RecoverAlpha += 0.3f * Time.deltaTime;
                //Debug.Log(RecoverAlpha);
                //EnergyBar.fillAmount = RecoverAlpha;
                EnergyBar.fillAmount += 5f * Time.deltaTime;

                //RecoverTimer = 0;
                // RecoverTimeChecking =false;
            }
           
            BackEnergyBar.fillAmount -= 0.2f * Time.deltaTime;
            if (BackEnergyBar.fillAmount < EnergyBar.fillAmount)
            {
                BackEnergyBar.fillAmount = EnergyBar.fillAmount;
            }
            return;
        }
       
    }

    private void showhpBar()
    {
        if (ShowHpBar)
        {
            b = 1;
            a += 0.05f;
            HpBarBG.GetComponent<Image>().color = new Color(1, 1, 1, a);
            HpBar.GetComponent<Image>().color = new Color(1, 1, 1, a);
            hideBarTime += 0.08f;
            GetComponent<Player_Hp>().Hp= HpBar.GetComponent<Image>().fillAmount*100f;
            if (hideBarTime >= 5)
            {

                isAtk = false;
            }
        }

        if (!isAtk)
        {
            if (isAtk)
            {
                //a = 0;
                hideBarTime = 0;
                HpBarBG.GetComponent<Image>().color = new Color(1, 1, 1, a);
                HpBar.GetComponent<Image>().color = new Color(1, 1, 1, a);
                return;
            }
            ShowHpBar = false;
            hideBarTime = 0;
            b -= 0.02f;
            a = 0;

            HpBarBG.GetComponent<Image>().color = new Color(1, 1, 1, b);
            HpBar.GetComponent<Image>().color = new Color(1, 1, 1, b);
        }
    }

    private void flip()
    {
        var hitPic = GameObject.Find("hit").GetComponent<SpriteRenderer>();

        //人物方向
        //flip
        EndPos = Input.mousePosition;

        float X = (EndPos.x - StartPos.x);


        X =  transform.position.x - prevPos.x;


        Vector3 scale = transform.localScale;
        if (X > 0.0)
        {
            hitPic.transform.localScale =new Vector3(1,1,1);
            scale.x = 0.8f;
        }
        else if (X < 0.0)
        {
            hitPic.transform.localScale = new Vector3(-1, 1, 1);
            scale.x = -0.8f;
        }

        transform.localScale = scale;

    }

   
}


