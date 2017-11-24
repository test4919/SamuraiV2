using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int nextWave =0;

    public float TimeBetween = 3f;
    public float WaveCountdown;
    private Transform player;
    private float searchCount = 1f;
    public float rndxMax;
    public float rndxMin;
    public float SamuraiPointMax;
    public float SamuraiPointMin;

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
    }

    void WaveNext()
    {
        state = SwapnState.count;
        WaveCountdown = TimeBetween;

        if (nextWave + 1 > waves.Length - 1)
        {
            if (player.transform.position.x < 82f)
            {
                GetComponent<Block_Event>().HideBlock01();
                GetComponent<CreateEnemy>().enabled = false;
                return;
            }
            else if (player.position.x >83 && player.position.x < 159)
            {
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
        float rndx;
        rndx = Random.Range(SamuraiPointMin,SamuraiPointMax); 
        Instantiate(_enemy, new Vector3(rndx,transform.position.y,1f), transform.rotation);
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
        rndy = Random.Range(33, 37);
        Instantiate(_Sky, new Vector3(rndx, rndy, 1f), transform.rotation);
    }
}
