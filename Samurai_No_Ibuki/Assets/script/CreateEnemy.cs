using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateEnemy : MonoBehaviour {

    public enum SwapnState { spawn,wait,count};


    [System.Serializable]
    public class Wave
    {
        public Transform Samurai;
        public Transform Ninja;
        public Transform Sky;
        public int countSamurai;
        public int countNinja;
        public int countSky;
        public float SamuraiRate;
        public float NinjaRate;
        public float SkyRate;
    }

    public Wave[] waves;
    public int nextWave =0;

    public float TimeBetween = 3f;
    public float WaveCountdown;
    private Transform player;
    private float searchCount = 1f;
    public float rndxMax;
    public float rndxMin;
    public float SamuraiPointMax;
    public float SamuraiPointMin;
    public float SamuraiShowPointLeft;
    public float SamuraiShowPointRight;

    
    //public GameObject menuKey;
    public GameObject tutorialBlack;
    bool EndFlag = false;

    public SwapnState state = SwapnState.count;
    void Start()
    {
        WaveCountdown = TimeBetween;
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (state == SwapnState.wait)
        {
            if (!EnemyAlive())
            {
                WaveNext();
            }
            else
            {
                return;
            }
        }

        if (WaveCountdown <= 0)
        {
            if (state != SwapnState.spawn)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            WaveCountdown -= Time.deltaTime;
        }

        //if (Input.GetMouseButtonDown(0)&&(EndFlag))
        //{
        //    tutorialEnd();
        //    Debug.Log("11");
        //}
    }

    void WaveNext()
    {
        state = SwapnState.count;
        WaveCountdown = TimeBetween;

        if (nextWave + 1 > waves.Length - 1)
        {
            if (SceneManager.GetActiveScene().name == "DownBattle")
            {
                return;
            }
          
            if (player.transform.position.x < 82f)
            {
                GetComponent<Block_Event>().HideBlock01();
                GetComponent<CreateEnemy>().enabled = false;
                return;
            }
            else if (player.position.x >83 && player.position.x < 159)
            {//133
                GetComponent<Block_Event>().HideBlock02();
                GetComponent<CreateEnemy>().enabled = false;
                return;
            }
            else if (player.position.x > 160 &&player.position.x < 250)
            {
                GetComponent<Block_Event>().HideBlock03();
                GetComponent<CreateEnemy>().enabled = false;
                return;
            }
            else if (player.position.x > 245 &&player.position.x < 315)
            {
               
                return;
            }

        }
        else
        {
            nextWave++;
        }
       
    }

    bool EnemyAlive()
    {
        searchCount -= Time.deltaTime;
        if (searchCount <= 0f)
        {
            searchCount = 1f;
            if (GameObject.FindGameObjectsWithTag("enemy").Length+ GameObject.FindGameObjectsWithTag("enemy2").Length+
                GameObject.FindGameObjectsWithTag("enemy3").Length <= 0)
            {
               
                return false;
            }
        }
       
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SwapnState.spawn;

        //if ((SceneManager.GetActiveScene().name == "Main") && (player.transform.position.x < 40f) && (nextWave == 0))
        //{
        //    StartCoroutine(tutorialStart());

        //}

        for (int i = 0; i < _wave.countSamurai; i++)
        {
            SpawnEnemy(_wave.Samurai);
            yield return new WaitForSeconds(1 / _wave.SamuraiRate);
        }
        for (int j=0; j < _wave.countNinja; j++)
        {
            SpawnNinja(_wave.Ninja);
            yield return new WaitForSeconds(1 / _wave.NinjaRate);
        }
        for (int k = 0; k < _wave.countSky; k++)
        {
            SpawnSky(_wave.Sky);
            yield return new WaitForSeconds(1 / _wave.SkyRate);
        }

        state = SwapnState.wait;

        yield break;
    }

    IEnumerator LastEnemy()
    {
        Time.timeScale = 0.5f;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //float rndx;
        //rndx = Random.Range(SamuraiPointMin,SamuraiPointMax); 
        Instantiate(_enemy, new Vector3(SamuraiShowPointLeft,transform.position.y,1f), transform.rotation);
        Instantiate(_enemy, new Vector3(SamuraiShowPointRight, transform.position.y, 1f), transform.rotation);
    }

    void SpawnNinja(Transform _ninja)
    {
        float rndx;
        rndx = Random.Range(rndxMin, rndxMax);
        Instantiate(_ninja, new Vector3(rndx, transform.position.y, 1f), transform.rotation);
    }

    void SpawnSky(Transform _Sky)
    {
        float rndx;
        float rndy;
        rndx = Random.Range(rndxMin, rndxMax);
        if (SceneManager.GetActiveScene().name == "Main")
        {
            rndy = Random.Range(33, 37);
        }
        else
        {
            rndy = Random.Range(6, 9);
        }
        Instantiate(_Sky, new Vector3(rndx, rndy, 1f), transform.rotation);
    }

    //private IEnumerator tutorialStart()
    //{
    //    yield return new WaitForSeconds(1f);
    //    tutorialBlack.SetActive(true);
    //    //GameObject.Find("TextController").SetActive(false);
    //    //tutorialBlack.GetComponent<SpriteRenderer>().color = new Color(128, 255, 255, 255);
    //    //tutorialEnd();
    //    //GameObject.Find("Player").GetComponent<Move>().enabled = true;
    //    //GameObject.Find("Swordobject").GetComponent<Kiseki>().enabled = true;
    //    //Time.timeScale = 0.0f;
    //    GameObject.Find("Player").GetComponent<Move>().enabled = false;
    //    Debug.Log("test10");
    //    GameObject.FindWithTag("enemy3").GetComponent<SamuraiController>().enabled = false;
    //    GameObject.FindWithTag("enemy3").GetComponent<EnemyAI>().enabled = false;
    //    //对话
    //    GameObject.Find("Player").GetComponent<Move>().enabled = true;
    //    Debug.Log("test11");
    //    EndFlag = true;
    //    GameObject.Find("TextController").GetComponent<TextController>().flag = false;
    //}
    //void tutorialEnd()
    //{
    //    menuKey.SetActive(true);
    //    GameObject.FindWithTag("enemy3").GetComponent<SamuraiController>().enabled = true;
    //    GameObject.FindWithTag("enemy3").GetComponent<EnemyAI>().enabled = true;
    //    tutorialBlack.SetActive(false);
    //    Debug.Log("test12");
    //}
}
