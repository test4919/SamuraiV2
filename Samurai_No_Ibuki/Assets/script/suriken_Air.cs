using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suriken_Air : MonoBehaviour
{
    public Vector2 speed = new Vector2(0.05f, 0.05f);
    public float surikenSpeed;
    public GameObject Player;
    public bool bossWave;
    Vector3 targetPos;
    float eug;
    private float rad;
    private Vector2 Position;

    float timer;
    void Start()
    {
        timer = 0f;
       
        targetPos = new Vector3(GameObject.Find("FinalBoss").transform.localScale.x,0, 0);
        targetPos = targetPos.normalized;
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
            Destroy(this.gameObject);
       
        this.transform.Translate(new Vector3(-targetPos.x * surikenSpeed * Time.deltaTime, 0, 0));
    }
    
}
