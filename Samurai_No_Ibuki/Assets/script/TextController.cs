using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextController : MonoBehaviour
{


    public string[] scenarios;
    [SerializeField]
    Text uiText;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    private string currentText = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    private int currentLine = 0;
    private int lastUpdateCharacter = -1;
    public bool chatchs;

    public GameObject panel;
    public GameObject menuKey;
    public GameObject tutorialBlack;
    public GameObject Tpanel;
    public GameObject Arrow;
    private Transform player;
    public bool flag = false;
    bool flag1 = false;
    bool arrowflag = false;

    
    float TranslateSpeedTime = 0.1f;

    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        SetNextLine();
        player = GameObject.Find("Player").transform;
        chatchs = true;
    }

    void Update()
    {
        // 文字の表示が完了してるならクリック時に次の行を表示する
        if (IsCompleteDisplayText)
        {

            if (currentLine < scenarios.Length && Input.GetMouseButtonDown(0) || currentLine < scenarios.Length &&
                Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                SetNextLine();

            }
            else if (currentLine == scenarios.Length && Input.GetMouseButtonDown(0) || currentLine < scenarios.Length &&
                Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {

                TextCLose();

            }
        }
        else
        {
            // 完了してないなら文字をすべて表示する
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                timeUntilDisplay = 0;
            }
        }

        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
        if (displayCharacterCount != lastUpdateCharacter)
        {
            uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }


        if ((player.GetComponent<Move>().Drag == true) && (!flag))
        {
            tutorialEnd();
            flag = true;
            arrowflag = false;
        }

        else { 
            TranslateSpeedTime += 0.1f;

            if ((TranslateSpeedTime > 10.0f) && (arrowflag))
            {
                //Debug.Log("111");
                Instantiate(Arrow, new Vector3(player.position.x + 2, player.position.y, 1f), transform.rotation);
                TranslateSpeedTime = 0.1f;
            }
        }
        //if (GameObject.FindWithTag("yajirushi") != null)
        //{
        //    TranslateSpeedTime += 0.1f;
        //    Arrow.transform.Translate(Vector3.right * TranslateSpeed);
        //    if (TranslateSpeedTime > 150.0f)
        //    {
        //        transform.Rotate(0, 180, 0);
        //        TranslateSpeedTime = 0.1f;
        //    }
        //}
    }


    void SetNextLine()
    {

        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = -1;
    }

    void TextCLose()
    {
        panel.SetActive(false);
        chatchs = false;

        if ((SceneManager.GetActiveScene().name == "Main") && (player.transform.position.x < 82f) && (!flag)&&(!flag1))
        {
            tutorialStart();
            flag1 = true;
        }
        //else
        //{
        //    menuKey.SetActive(true);
        //    GameObject.Find("Player").GetComponent<Move>().enabled = true;
        //    GameObject.Find("Swordobject").GetComponent<Kiseki>().enabled = true;
        //}
    }

    void tutorialStart()
    {
        tutorialBlack.SetActive(true);
        Tpanel.SetActive(true);
        //tutorialBlack.GetComponent<SpriteRenderer>().color = new Color(128, 255, 255, 255);
        //tutorialEnd();
        GameObject.Find("Player").GetComponent<Move>().enabled = true;
        GameObject.Find("Swordobject").GetComponent<Kiseki>().enabled = true;
        TranslateSpeedTime = 0.1f;
        arrowflag = true;
        Instantiate(Arrow, new Vector3(player.position.x + 2, player.position.y, 1f), transform.rotation);
        //Debug.Log("222");


    }
    void tutorialEnd()
    {

        menuKey.SetActive(true);
        //if (GameObject.FindWithTag("enemy3") != null)
        //{
        //    GameObject.FindWithTag("enemy3").GetComponent<SamuraiController>().enabled = true;
        //    GameObject.FindWithTag("enemy3").GetComponent<EnemyAI>().enabled = true;
        //}
        //Debug.Log("test2");
        tutorialBlack.SetActive(false);
        Tpanel.SetActive(false);
        Destroy(GameObject.FindWithTag("yajirushi"));

    }
}
