using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2Controller : MonoBehaviour {

    
   
    public GameObject Body;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Player;
    public GameObject RedWave;
    public GameObject Flame;
    public GameObject Tips;
    public GameObject BossHpBar;
    public GameObject Eyes;
    public GameObject ClearBtn;
    public GameObject Enemy_BloodR;
    public GameObject Enemy_BloodL;
    public AudioClip BGMw;
    GameObject FinishPic;
    GameObject FinishWord;
    GameObject Lefthand;
    
    public float AttackTime;
    public float AttackColdDown;
    public float WaveTime;
    public float WaveColdDown;
    public float LeftHandHP;
    public float RightHandHP;
    public float BossHP;
    public float BlinkCD;
  
    public bool Attacking;
    public bool NormalAttack;
    public bool NormalAttackCD;
    public bool isHead;
    public bool WaveAtk;
    public bool WaveAtking;
    public bool WaveAtkCD;

    GameObject BossRedWave;
    GameObject flame;
    GameObject BloodR;
    GameObject BloodL;
    bool _redwave;
    bool _HandAtk;
    bool _AtkDir;
    bool _fire;
    bool _break;
    bool _blink;
    bool _timer;
    bool _Leftmov;
    bool _rightmov;
    bool _Win;
    float def;
    float firerange;
    float timer;
    float yBig;
    float xBig;
    float small;
    float scale;

    


    // Use this for initialization
    void Start () {
        Lefthand = GameObject.Find("HandAttack");
        FinishPic = GameObject.Find("Finish");
        FinishWord = GameObject.Find("FinishWord");
        Attacking = false;
        WaveAtking = false;
        NormalAttackCD = false;
        _redwave = false;
        _HandAtk = false;
        _AtkDir = false;
        _fire = false;
        _break = false;
        _timer = false;
        timer = 0;
        scale = 1.5f;

        small = 10.0f;
        BlinkCD = 5.0f;
        AttackTime =4.0f;
        AttackColdDown = 5.0f;
        WaveTime = 5.0f;
        WaveColdDown = 6.0f;
        LeftHandHP=100.0f;
        RightHandHP = 100.0f;
        BossHP = 100.0f;
}
	
	// Update is called once per frame
	void Update () {
        normalAttack();
        waveAttack();
        LookPlayer();
        checking();
        if (LeftHandHP <= 0 && RightHandHP <= 0 && !_break)
        {
           Body.gameObject.tag = "BossBody";
            _break = true;
            BossHpBar.SetActive(true);
            
        }
        if (_break)
        {
            HpBar();
            float rmx = Random.Range(-18.0f, 4.0f);
            //this.transform.Translate(new Vector3(0.5f * Time.deltaTime, 0, 0));
            if (this.transform.position.x <= 3.5f&&!_rightmov)
            {
                this.transform.Translate(new Vector3(3.0f * Time.deltaTime, 0, 0));
                _Leftmov = true;
            }
            else if (this.transform.position.x >= -15.5&&_Leftmov)
            {
                this.transform.Translate(new Vector3(-3.0f * Time.deltaTime, 0, 0));
                _rightmov = true;
                if (this.transform.position.x <= -15.0f)
                {
                    _Leftmov = false;
                    _rightmov = false;
                }
            }
        }
        if (_Win)
        {
            Win();
        }
        

        if (LeftHandHP <= 0)
        {
            LeftHand.SetActive(false);
        }
        if (RightHandHP <= 0)
        {
            RightHand.SetActive(false);
        }

        if (_break)
        {
            return;
        }
        Blink();

        if (!_timer)
        {
            timer += 1 * Time.deltaTime;
        }
        if (!_timer && timer > 2)
        {
            Tips.SetActive(true);
            
        }
        if (!_timer && timer >= 5)
        {
            timer = 0;
            _timer = true;
            Tips.SetActive(false);
        }

        if (NormalAttack)
        {
            LeftHandATK();
        }
        if (WaveAtk&&!WaveAtkCD)
        {
            
            RightHandATK();
            
        }
        if (isHead&&Attacking)
        {
            StartCoroutine("AtkDown");
            LeftHand.gameObject.tag = "BossLeftHand";
        }
        if (WaveAtking && WaveTime >=4.8f)
        {
            StartCoroutine("Wave");
        }
       
        if (AttackTime <= 0)
        {
            NormalAttack = false;
            NormalAttackCD = true;
            //WaveAtkCD = false;
            AttackColdDown -= Time.deltaTime;
            if (AttackColdDown <= 0)
            {
                AttackTime = 4.0f;
                AttackColdDown = 5.0f;
                NormalAttackCD = false;
                _HandAtk = false;
            }
        }

        if (WaveTime <= 0)
        {
            WaveAtk = false;
            WaveAtking = false;
            WaveAtkCD = true;
            WaveColdDown -= Time.deltaTime;
            if (WaveColdDown <= 0)
            {
                WaveTime = 5.0f;
                WaveColdDown = 6.0f;
                WaveAtkCD = false;
                _redwave = false;
            }
        }
        if (_blink)
        {
            BlinkCD -= Time.deltaTime;
            if (BlinkCD <= 0)
            {
                _blink = false;
                BlinkCD = 5.0f;
            }
        }
        
        
    }

    private void Blink()
    {
        if (!_blink)
        {
            this.transform.position=new Vector3(Random.Range(-17, 3),this.transform.position.y,0) ;
            _blink = true;
            
        }
        
       
    }

    private void normalAttack()
    {
        if (!WaveAtk && AttackColdDown >= 5.0f)
        {
            NormalAttack = true;
        }
    }

    private void waveAttack()
    {
        if (NormalAttackCD && WaveColdDown >= 6.0f)
        {
            WaveAtk = true;
        }
      
    }

    private void LeftHandATK()
    {
        if (NormalAttack)
        {

            if (!_HandAtk)
            {
                LeftHand.transform.Translate(-0.3f, 0, 0);

            }
            
            if (this.transform.localScale == new Vector3(scale, scale, scale) && !isHead)
                {
                    
                    if (LeftHand.transform.position.x <= Player.transform.position.x)
                    {
                        LeftHand.transform.position = new Vector3(Player.transform.position.x, LeftHand.transform.position.y, 0);
                        isHead = true;
                        Attacking = true;
                    _HandAtk = true;
                    }
                }

                else if (this.transform.localScale == new Vector3(-scale,scale,scale) && !isHead)
                {
                    if (LeftHand.transform.position.x >= Player.transform.position.x)
                    {
                        LeftHand.transform.position = new Vector3(Player.transform.position.x, LeftHand.transform.position.y, 0);
                        isHead = true;
                        Attacking = true;
                    _HandAtk = true;
                }
                }
                
            
            AttackTime -= Time.deltaTime;
        }
       
    }

    private void RightHandATK()
    {
        WaveTime -= Time.deltaTime;
        WaveAtking = true;
    }

    private void LookPlayer()
    {
        if (Attacking)
        {
            return;
        }

        if (Player.transform.position.x > this.transform.position.x)
        {
            this.transform.localScale = new Vector3(-scale, scale,scale);
        }
        else
        {
            this.transform.localScale = new Vector3(scale,scale, scale);
        }
    }

    IEnumerator  AtkDown()
    {
        if (this.transform.localScale == new Vector3(-scale, scale,scale))
        {
            Lefthand.transform.rotation = Quaternion.Euler(0, 0, -30);
        }
        else if (this.transform.localScale == new Vector3(scale, scale, scale))
        {
            Lefthand.transform.rotation = Quaternion.Euler(0, 0, 30);
        }
        yield return new WaitForSeconds(0.5f);
        LeftHand.transform.Translate(0, -0.3f, 0);
        yield return new WaitForSeconds(1f);
        isHead = false;
        Attacking = false;
        LeftHand.gameObject.tag = "BossHandL";
        LeftHand.transform.localPosition = new Vector3(3.2f, 1.58f, 0);
        Lefthand.transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    IEnumerator Wave()
    {
        if (WaveAtk)
        {
            if (this.transform.localScale == new Vector3(-scale, scale, scale))
            {
                RightHand.transform.rotation = Quaternion.Euler(0, 0, -90);
                yield return new WaitForSeconds(1f);
                RightHand.transform.rotation = Quaternion.Euler(0, 0, 0);
                yield return new WaitForSeconds(0.1f);
                RightHand.transform.rotation = Quaternion.Euler(0, 0, 90);
                yield return new WaitForSeconds(0.2f);
                float firerange;
                if (!_redwave)
                {
                    BossRedWave = GameObject.Instantiate(RedWave, new Vector2(transform.position.x, -12.0f), transform.rotation) as GameObject;
                    if (Player.transform.position.x > this.transform.position.x)
                    {
                        BossRedWave.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
                        firerange = Random.Range(this.transform.position.x, 4.0f);
                        fireSkill(firerange);
                    }
                    else
                    {
                        BossRedWave.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                        firerange = Random.Range(-18.0f, this.transform.position.x);
                        fireSkill(firerange);
                    }
                    _redwave = true;
                }
                RightHand.transform.rotation = Quaternion.Euler(0, 0, 120);
                yield return new WaitForSeconds(0.5f);      
            }
            else if (this.transform.localScale == new Vector3(scale,scale,scale))
            {
                RightHand.transform.rotation = Quaternion.Euler(0, 0, 90);
                yield return new WaitForSeconds(1f);
                RightHand.transform.rotation = Quaternion.Euler(0, 0, 0);
                yield return new WaitForSeconds(0.1f);
                RightHand.transform.rotation = Quaternion.Euler(0, 0, -90);
                yield return new WaitForSeconds(0.2f);
                float firerange;
                if (!_redwave)
                {
                    BossRedWave = GameObject.Instantiate(RedWave, new Vector2(transform.position.x, -12.0f), transform.rotation) as GameObject;
                    if (Player.transform.position.x > this.transform.position.x)
                    {
                        BossRedWave.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
                        firerange = Random.Range(this.transform.position.x, 4.0f);
                        fireSkill(firerange);
                    }
                    else
                    {
                        BossRedWave.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                        firerange = Random.Range(-18.0f, this.transform.position.x);
                        fireSkill(firerange);
                    }
                    _redwave = true;
                }
                RightHand.transform.rotation = Quaternion.Euler(0, 0,-120);
                yield return new WaitForSeconds(0.5f);
            }

        }
        //float firerange;
        //if (!_redwave)
        //{
        //    BossRedWave = GameObject.Instantiate(RedWave, new Vector2(transform.position.x,-12.0f), transform.rotation) as GameObject;
        //    if (Player.transform.position.x > this.transform.position.x)
        //    {
        //        BossRedWave.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        //        firerange = Random.Range(this.transform.position.x, 4.0f);
        //        fireSkill(firerange);
        //    }
        //    else
        //    {
        //        BossRedWave.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        //        firerange = Random.Range(-18.0f, this.transform.position.x);
        //        fireSkill(firerange);
        //    }
        //    _redwave = true;
        //}
        
        yield return new WaitForSeconds(1.1f);
        RightHand.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        yield return new WaitForSeconds(1.1f);
        WaveAtking = false;
        WaveAtk = false;
        Destroy(BossRedWave, 0.5f);

    }

    void HpBar()
    {

        var NowHp = GameObject.Find("BossHp").GetComponent<Image>();
        var BackHp = GameObject.Find("BackBossHp").GetComponent<Image>();

        NowHp.fillAmount = BossHP / 100.0f;
        
        if (NowHp.fillAmount < 1)
        {
            BackHp.fillAmount -= 0.1f * Time.deltaTime;
            if (BackHp.fillAmount < NowHp.fillAmount)
            {
                BackHp.fillAmount = NowHp.fillAmount;
            }
        }
        if (NowHp.fillAmount <= 0)
        {
            Body.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            _rightmov = true;
            _Leftmov = false;
            StartCoroutine("BossDead");
        }
    }

   IEnumerator BossDead()
    {
        yield return new WaitForSeconds(0.5f);
        Eyes.SetActive(false);

        yield return new WaitForSeconds(1.0f);
        _Win = true;
        
    }

    private void Win()
    {
        Player.GetComponent<Move>().isStop = true;
        yBig += 1f * Time.deltaTime;
        FinishPic.transform.localScale = new Vector3(FinishPic.transform.localScale.x, yBig , 0);

        SoundManager.instance.BGM(BGMw);
        if (yBig >= 1)
        {
            yBig = 1;
            xBig += 1f * Time.deltaTime;
            FinishPic.transform.localScale = new Vector3(xBig, FinishPic.transform.localScale.y, 0);
            if (xBig >= 1.2f)
            {
                xBig = 1.2f;
                FinishPic.transform.localScale = new Vector3(FinishPic.transform.localScale.x, FinishPic.transform.localScale.y, 0);
                FinishWord.GetComponent<SpriteRenderer>().sortingOrder = 16;
                small -= 3.0f*Time.deltaTime;
                FinishWord.transform.localScale = new Vector3(small, small, small);
                if (small <= 1.0f)
                {
                    small = 1f;
                    FinishWord.transform.localScale = new Vector3(1,1,1);
                    StartCoroutine("ShowButton");
                }
            }
        }
    }

    private void fireSkill(float x)
    {
        flame = GameObject.Instantiate(Flame, new Vector2(x, -7.0f), transform.rotation) as GameObject;
        Destroy(flame, 5f);
        
    }

    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(2f);
        ClearBtn.GetComponent<Canvas>().sortingOrder = 16;
    }

    private void checking()
    {
        if (LeftHandHP <= 0)
        {
            NormalAttack = false;

        }

        if (RightHandHP <= 0)
        {
            WaveAtk = false;
            WaveAtkCD = true;
        }

    }

    public void EnemyBlood_show()
    {
        if (this.transform.localScale.x == scale)
        {
            BloodR = GameObject.Instantiate(Enemy_BloodR, transform.position, transform.rotation) as GameObject;

            BloodR.transform.localScale = new Vector3(-2, 2, 1);
            BloodR.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (this.transform.localScale.x == -scale)
        {
            BloodL = GameObject.Instantiate(Enemy_BloodL, transform.position, transform.rotation) as GameObject;


            BloodL.transform.localScale = new Vector3(2, 2, 1);
            BloodL.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        
        Destroy(BloodR, 1.5f);
        Destroy(BloodL, 1.5f);
    }




}
