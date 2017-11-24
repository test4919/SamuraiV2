using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour {

    public AudioSource[] EnemySoundList;
    public AudioSource attackByPlayer;
    public AudioClip NormalHitFinish;
    public AudioClip normalAttack;
    public AudioClip SkillReady;
    public AudioClip BGMw;
    //public AudioSource skill;

    public GameObject iai;
    public GameObject iai2;
    public GameObject redEye;
    public GameObject Enemy_BloodR;
    public GameObject Enemy_BloodL;
    public GameObject EnemyDeath_Blood;
    public float BossHp;
    public float BossMaxHp;

    public float maxAttackDist;
    public float minDist;
    public float attackColdown;
    public float skillColdown;
    public float speed;
    public GameObject bossKage;
    public GameObject bossSword;
    public GameObject bossWave;
    public bool BossUlt;

    bool attackleft;
    bool timeChecking;
    bool gameOver;
    bool blood;
    bool ULTblood;

    Vector2 flash;
    float timer;
    float skillTimer;
    float attackTimer;
    float deathTimer;
    float HpAlpha;
    float ToBigtime;
    GameObject player;
    GameObject BloodR;
    GameObject BloodL;
    GameObject DeathBlood;
    GameObject clearword;
    GameObject ClearChs;
    Animator thisAnimator;
    float randomX1;
    float randomY1;
    float randomX2;
    float randomY2;
    float randomX3;
    float randomY3;
    int index = 1;
    GameObject sgo1 = null;
    GameObject sgo2 = null;
    GameObject sgo3 = null;
    //bool EnemySwap;
    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        clearword = GameObject.Find("daiganseijyu ji");
        ClearChs = GameObject.Find("ClearChoose");
        thisAnimator = this.GetComponent<Animator>();
        attackleft = true;
        skillTimer = 0f;
        HpAlpha = 0f;
        BossUlt = false;
        gameOver = false;
        EnemySoundList = GetComponents<AudioSource>();
        attackByPlayer = EnemySoundList[0];
        blood = false;
        ULTblood = false;
        //EnemySwap = false;
        //skill = EnemySoundList[3];
    }

    // Update is called once per frame
    void Update() {
        if (timeChecking)
        {
            deathTimer += 1f * Time.deltaTime;
        }

        HpBar();
       
        if (BossHp == 0)
        {
            if (deathTimer > 2f)
            {
                HpAlpha += 0.3f * Time.deltaTime;
                if (BossHp == 0)
                {
                    GameObject.Find("daiganseijyu").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, HpAlpha);
                    if (HpAlpha >= 1)
                    {
                        Win();                        
                    }
                }
            }
            return;
             }
        

        timer += Time.deltaTime;
        if (attackTimer < attackColdown && !thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("BSkill"))
            attackTimer += Time.deltaTime;
        if (timer > 1 && attackTimer >= attackColdown && Mathf.Abs(this.transform.position.x - player.transform.position.x) < maxAttackDist &&
            Mathf.Abs(this.transform.position.x - player.transform.position.x) > minDist && !(thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("BSkill"))) {
            Ready();
        } else {
            if (!(thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("BAttack") || thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("BReady") || thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("BSkill"))) {
                LookAtPlayer();
                Move();
            }
        }
        if (skillTimer < skillColdown) {
            //Debug.Log (skillTimer);
            skillTimer += Time.deltaTime;
        }
        else if (BossHp <= BossMaxHp * 0.4f && !(thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("BAttack") || thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("BReady")))
        {
            Debug.Log("1");
            Skill();
        }
        if (timer > 5)
            timer = 0;

        //if (BossHp <= BossMaxHp * 0.5 && !EnemySwap)
        //{
        //    if (EnemySwap)
        //    {
        //        return;
        //    }
        //    StartCoroutine(SwapEnemy());
        //}
    }
    void Ready()
    {
        //	skillTimer -= 5;
        if (player.transform.position.x > this.transform.position.x)
            attackleft = false;
        else
            attackleft = true;
        attackTimer = 0;
        thisAnimator.SetTrigger("BReady");
        Invoke("Attack1", 0.7f);
    }
    void Attack1()
    {
        GameObject go = GameObject.Instantiate(iai, new Vector3(this.transform.position.x + 0.6f, this.transform.position.y - 0.9f, 0), this.transform.rotation) as GameObject;
        BossSound.instance.OneTimeBossSound(SkillReady);
        if (!attackleft)
            go.GetComponent<BossSwordController>().Left = false;
        Invoke("Attack2", 0.3f);
    }
    void Attack2()
    {
        thisAnimator.SetTrigger("BAttack");
        if (attackleft)
            flash = new Vector2(-10, 0);
        else
            flash = new Vector2(10, 0);
        Invoke("FlashBy", 0.3f);
        Invoke("Iai2", 1.7f);
    }
    void Iai2()
    {
        GameObject.Instantiate(iai2, new Vector3(this.transform.position.x - 0.1f, this.transform.position.y + 0.6f, 0), this.transform.rotation);
        BossSound.instance.OneTimeBossSound(NormalHitFinish);
        skillTimer -= 0.5f;
    }

    void FlashBy()
    {
        this.transform.position = new Vector3(this.transform.position.x + flash.x, this.transform.position.y + flash.y, 0);
    }
    void FlashTo()
    {
        this.transform.position = new Vector3(flash.x, flash.y, 0);
    }
    void LookAtPlayer()
    {
        if (player.transform.position.x > this.transform.position.x)
            this.transform.localScale = new Vector3(-1, 1, 1);
        else
            this.transform.localScale = new Vector3(1, 1, 1);
    }
    void Move()
    {
        //		Debug.Log (Mathf.Abs (this.transform.position.x - player.transform.position.x));
        if (!(Mathf.Abs(this.transform.position.x - player.transform.position.x) > minDist && Mathf.Abs(this.transform.position.x - player.transform.position.x) < maxAttackDist)) {
            thisAnimator.SetBool("BRun", true);
            thisAnimator.SetBool("BWait", false);
            if (Mathf.Abs(this.transform.position.x - player.transform.position.x) > maxAttackDist)
                ClosePlayer();
            else if (Mathf.Abs(this.transform.position.x - player.transform.position.x) < minDist)
                FarAwayPlayer();
        } else {
            thisAnimator.SetBool("BRun", false);
            thisAnimator.SetBool("BWait", true);
        }
    }
    void ClosePlayer()
    {
        //	Debug.Log ("close");
        if (player.transform.position.x > this.transform.position.x)
            MoveRight();
        else
            MoveLeft();
    }
    void MoveLeft()
    {
        this.transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0, 0));
    }
    void FarAwayPlayer()
    {
        //	Debug.Log ("farAway");
        if (player.transform.position.x > this.transform.position.x)
            MoveLeft();
        else
            MoveRight();
    }
    void MoveRight()
    {
        this.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
    void Skill()
    {
        skillTimer = 0;
        thisAnimator.SetTrigger("BSkill");
        if (player.transform.position.x > this.transform.position.x)
            GameObject.Instantiate(redEye, new Vector3(this.transform.position.x + 0.64f, this.transform.position.y + 0.56f, 0), this.transform.rotation);
        else
            GameObject.Instantiate(redEye, new Vector3(this.transform.position.x - 0.6f, this.transform.position.y + 0.54f, 0), this.transform.rotation);
        Attack3();
        BossSound.instance.OneTimeBossSound(SkillReady);
    }
    void Attack3()
    {
        BossUlt = true;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        if (this.transform.position.x > player.transform.position.x) {
            randomX1 = Random.Range(310f, 315f);
            randomY1 = Random.Range(34f, 39f);
            randomX2 = Random.Range(326f, 335f);
            randomY2 = Random.Range(38f, 39f);
            randomX3 = Random.Range(310f, 335f);
            randomY3 = 28f;
        } else if (this.transform.position.x < player.transform.position.x) {
            randomX1 = Random.Range(332f, 335f);
            randomY1 = Random.Range(34f, 39f);
            randomX2 = Random.Range(310f, 320f);
            randomY2 = Random.Range(38f, 39f);
            randomX3 = Random.Range(310f, 335f);
            randomY3 = 28f;
        }
        Invoke("InsKage", 1.7f);
    }
    void InsKage()
    {
        switch (index)
        {
            case 1: sgo1 = GameObject.Instantiate(bossSword, this.transform.position, this.transform.rotation) as GameObject;
                BossSound.instance.OneTimeBossSound(normalAttack);
                break;
            case 2: sgo2 = GameObject.Instantiate(bossSword, this.transform.position, this.transform.rotation) as GameObject;
                BossSound.instance.OneTimeBossSound(normalAttack);
                break;
            case 3: sgo3 = GameObject.Instantiate(bossSword, this.transform.position, this.transform.rotation) as GameObject;
                BossSound.instance.OneTimeBossSound(normalAttack);
                break;
            default:
                break;
        }
        GameObject go;
        if (index < 4) {
            go = GameObject.Instantiate(bossKage, this.transform.position, this.transform.rotation) as GameObject;
            go.transform.localScale = this.transform.localScale;
        }
        if (this.transform.position.x > player.transform.position.x)
            this.transform.localScale = new Vector3(1, 1, 1);
        else
            this.transform.localScale = new Vector3(-1, 1, 1);
        switch (index) {
            case 1:
                flash.x = randomX1;
                flash.y = randomY1;
                float eug = Mathf.Atan((randomY1 - this.transform.position.y) / (randomX1 - this.transform.position.x)) * Mathf.Rad2Deg;
                if (randomX1 < this.transform.position.x)
                    eug = eug - 180;
                sgo1.transform.localEulerAngles = new Vector3(0, 0, eug);
                index++;
                break;
            case 2:
                flash.x = randomX2;
                flash.y = randomY2;
                float eug2 = Mathf.Atan((randomY2 - this.transform.position.y) / (randomX2 - this.transform.position.x)) * Mathf.Rad2Deg;
                if (randomX2 < this.transform.position.x)
                    eug2 = eug2 - 180;
                sgo2.transform.localEulerAngles = new Vector3(0, 0, eug2);
                index++;
                break;
            case 3:
                flash.x = randomX3;
                flash.y = randomY3;
                float eug3 = Mathf.Atan((randomY3 - this.transform.position.y) / (randomX3 - this.transform.position.x)) * Mathf.Rad2Deg;
                if (randomX3 < this.transform.position.x)
                    eug3 = eug3 - 180;
                sgo3.transform.localEulerAngles = new Vector3(0, 0, eug3);
                index++;
                break;
            default:
                index++;
                break;
        }
        if (index > 4) {

            Debug.Log("over");
            index = 1;
            OverSkill();
            Destroy(sgo1.gameObject, 0.5f);
            Destroy(sgo2.gameObject, 0.5f);
            Destroy(sgo3.gameObject, 0.5f);
        } else {
            Invoke("InsKage", 0.5f);
            FlashTo();
        }
    }
    void OverSkill()
    {
        this.GetComponent<Rigidbody2D>().gravityScale = 1;
        thisAnimator.SetTrigger("BSkillOver");
        attackTimer = 0;
        BossSound.instance.OneTimeBossSound(NormalHitFinish);
    }
    public void GetDamage()
    {

        if (BossHp == 0)
        {
            // Death();
        }

    }

    void HpBar()
    {
        var NowHp = GameObject.FindWithTag("BossHp").GetComponent<Image>();
        var BackHp = GameObject.Find("BackBossHp").GetComponent<Image>();

        BossHp = NowHp.fillAmount * 100f;
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
            GameObject.Find("Player").GetComponent<Move>().isStop = true;

            StartCoroutine(BossD());

        }
    }

    private IEnumerator BossD()
    {
        //thisAnimator.SetBool("BDeath", true);
        thisAnimator.SetBool("BRun", false);
        thisAnimator.SetBool("BWait", true);
        GameObject.Find("oora_5_00017").SetActive(false);
        GameObject.Find("BackGround").SetActive(false);
        yield return new WaitForSeconds(1f);
        thisAnimator.SetBool("BDeath", true);
        yield return new WaitForSeconds(2.2f);
        if (thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("BDeath"))
        {
            if (!blood)
            {
                DeathBlood = GameObject.Instantiate(EnemyDeath_Blood, this.transform.position - new Vector3(0, 1.5f, -3), this.transform.rotation) as GameObject;
                DeathBlood.GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder - 1;
                blood = true;
            }
        }
        yield return new WaitForSeconds(1f);
        SoundManager.instance.BGM(BGMw);
        GameObject.Find("MenuUse").GetComponent<Canvas>().sortingOrder = -10;
        timeChecking = true;
    }


    public void EnemyBlood_show()
    {
        if (this.transform.localScale.x == 1)
        {
            BloodR = GameObject.Instantiate(Enemy_BloodR, transform.position, transform.rotation) as GameObject;
       
            BloodR.transform.localScale = new Vector3(-2, 2, 1);
            BloodR.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (this.transform.localScale.x == -1)
        {
            BloodL = GameObject.Instantiate(Enemy_BloodL, transform.position, transform.rotation) as GameObject;


            BloodL.transform.localScale = new Vector3(2, 2, 1);
            BloodL.transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        if (!attackByPlayer.isPlaying)
        {
            attackByPlayer.Play();
        }
        Destroy(BloodR, 1.5f);
        Destroy(BloodL, 1.5f);
    }
    public void ULTEnemyBlood()
    {
        ULTblood = true;
        StartCoroutine(UltAtk());  
    }

    private void Win()
    {
        clearword.GetComponent<SpriteRenderer>().sortingOrder = 16;
        ToBigtime += 1f * Time.deltaTime;

        if (ToBigtime <= 1.0f)
        {
            clearword.transform.localScale = new Vector3(ToBigtime, ToBigtime, 0);
            clearword.transform.position += new Vector3(0,0.17f, 0);
            StartCoroutine(ClearButton());
        }

    }

    private IEnumerator ClearButton()
    {
        yield return new WaitForSeconds(1.5f);
        ClearChs.GetComponent<Canvas>().sortingOrder = 16;
    }

    private IEnumerator UltAtk()
    {
        if (!attackByPlayer.isPlaying)
        {
            attackByPlayer.Play();
        }

        if (ULTblood)
        {
            if (this.transform.localScale.x == 1)
            {
                BloodR = GameObject.Instantiate(Enemy_BloodR, transform.position, transform.rotation) as GameObject;
                BloodR.transform.localScale = new Vector3(-2, 2, 1);
                BloodR.transform.localEulerAngles = new Vector3(0, 90, 0);
            }
            else if (this.transform.localScale.x == -1)
            {
                BloodL = GameObject.Instantiate(Enemy_BloodL, transform.position, transform.rotation) as GameObject;
                BloodL.transform.localScale = new Vector3(2, 2, 1);
                BloodL.transform.localEulerAngles = new Vector3(0, 90, 0);
            }
            ULTblood = false;
        }
        yield return new WaitForSeconds(0.5f);

        Destroy(BloodR, 1.5f);
        Destroy(BloodL, 1.5f);
        
    }
    //private IEnumerator SwapEnemy()
    //{
    //    EnemySwap = true;
    //    if (EnemySwap)
    //    {
    //        GameObject.Find("BossEvent").GetComponent<CreateEnemy>().enabled = true;
    //        yield return new WaitForSeconds(1f);
    //        GameObject.Find("BossEvent").GetComponent<CreateEnemy>().enabled = false;
    //    }
    //}
}
