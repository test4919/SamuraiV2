using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

   
    public AudioClip slash;
    public GameObject Blood;
    public GameObject createEnemy01;
    GameObject nowBlood;
    Animator animator;
    GameObject ShowEnemy01;


    public Transform player; // プレイヤーを代入
    public float limitDistance = 1000;

    private bool isGround = false;

    //ターゲットオブジェクト
    public GameObject Player;


    public Rigidbody2D rb2d;



    private Vector2 forward;
    private Vector2 Deadforward;

    bool Run;

    public float Hit = 0;

    public float dis;

    public float random;

    float attackdelay;

    float timeleft;

    bool IsGround;

    bool DeadFlag = false;

    GameObject ea;

    public  Vector2 scale;
    void Start ()
    {
       // ShowEnemy01 = GameObject.Instantiate(createEnemy01, new Vector2(1, 1), transform.rotation) as GameObject;
       // ShowEnemy01.transform.localScale = new Vector3(1, 0.8f, 0);

        animator = GetComponent<Animator>();

        Player = GameObject.Find("Player");

        Run = true;

        rb2d = GetComponent<Rigidbody2D>();

        ea = transform.FindChild("enemyattack").gameObject;
        

    }

    public void startAnimation()
    {
        
        if (animator)
        {
            animator.Play("Enemy_wait");
        }

    }
	void Update ()
    {
       // Debug.Log(random);  

        Vector2 player = Player.transform.position;
        Vector2 teki = gameObject.transform.position;
        dis = Vector2.Distance(player, teki);
        scale = transform.localScale;
        
        forward = Player.transform.position - transform.position;
        rotation();

        timeleft -= Time.deltaTime;

      
       
        //Debug.Log(IsGround);
        
        if (timeleft <= 0.0)
        {
            timeleft = 5.0f;
            random = Random.Range(1,10);
            
        }


        if (random == 1)
        {
            Block();
            animator.SetTrigger("EBlock");
//            Debug.Log("Block");
        }
        else
        {
            Attack();  
        }

        Deadforward = Player.transform.position - transform.position;
        if (DeadFlag)
        {

            transform.Translate(Deadforward * Time.deltaTime * 0.5f);

        }


    }




    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag =="Ground")
        {
            IsGround = true;
            Debug.Log(IsGround + "2");
        }
        Debug.Log(IsGround + "3");
    }
   

    void Attack()
    {
        GetComponent<Collider2D>().enabled = true;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        ea.GetComponent<Collider2D>().enabled = true;

        if (IsGround == true)
        {
            if (dis < 10)
            {
                attackdelay += Time.deltaTime;

                if (attackdelay > 3)
                {
                    animator.SetTrigger("EDA");
                    EnemyDushAttack();
                }

            }

            else if (dis < 15)
            {
                attackdelay += Time.deltaTime;
               

                if (attackdelay > 1)
                {
                    animator.SetTrigger("EJA");
                    EnemyJumpAttack();
                    IsGround = false;
                }
            }

            else if (dis > 15)
            {
                attackdelay += Time.deltaTime;
                animator.SetBool("ERun", true);
                Move();
            }
        }
}
    
    

    void rotation()
    {
        if(forward.x > 0)
        {
            scale.x = -1;
        }
        else
        {
            scale.x = 1;
        }

        transform.localScale = scale;
    }

    void Move()
    {

        rb2d.constraints = RigidbodyConstraints2D.None;

        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;


        if (dis >5)
        {
            if (forward.x > 0)
            {
                transform.Translate(Vector2.right * 0.1f);     
            }
            else
            {
               transform.Translate(Vector2.left * 0.1f);
            }

        }
        
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player)
        {
            Debug.Log("Damega");

        }
    }


    void EnemyJumpAttack()
    {
        if (scale.x == 1)
        {
            rb2d.velocity += new Vector2(-dis , 9);
            attackdelay = 0;
        }
        else
        {
            rb2d.velocity += new Vector2(dis , 9);
            attackdelay = 0;
        }

    }


    void EnemyDushAttack()
    {
       

        if (scale.x == 1)
        {
            rb2d.velocity = new Vector2(-dis * 2, 0);
            attackdelay = 0;
        }
        else
        {
            rb2d.velocity = new Vector2(dis * 2, 0);
            attackdelay = 0;
        }
        
    }

    
    void Block()
    {
        
        GetComponent<Collider2D>().enabled = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
        ea.GetComponent<Collider2D>().enabled = false;
     }



    
              
  



    public void Attacked()
    {


        nowBlood = GameObject.Instantiate(Blood,this.transform.position+new Vector3(1,0,0), this.transform.rotation)as GameObject;
        GetComponent<AudioSource>().PlayOneShot(slash);
        

        Invoke("isClose", 0.5f);
        return;
    }

    private void isClose()
    {
        gameObject.SetActive(false);
        Destroy(nowBlood);

        //for test
        GameObject.Find("Event1").GetComponent<Block_Event>().HideBlock01();
    
        //Invoke("show", 1.0f);
    }

    private void Dead()
    {
        DeadFlag = true;
        animator.SetBool("E3Drop", true);
    }

    private void show()
    {
        
        gameObject.SetActive(true);
        float rndx;
        rndx = Random.Range(12.8f, 34.7f);
        Vector3 NewPos = new Vector3(rndx, -4, 0);
        transform.position = NewPos;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        if (transform.position.y < -5) {
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }

       return; 
    }

    public void EnemyAttack()
    {

    }


 
}
