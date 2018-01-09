using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    
    public string[] scenarios;
    [SerializeField] Text uiText;

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

    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        SetNextLine();
        chatchs = true;
}

    void Update()
    {
        // 文字の表示が完了してるならクリック時に次の行を表示する
        if (IsCompleteDisplayText)
        {
           
            if (currentLine < scenarios.Length && Input.GetMouseButtonDown(0)|| currentLine < scenarios.Length && 
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
    }


    void SetNextLine()
    {

        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter =-1;
    }

    void TextCLose()
    {
        panel.SetActive(false);
        chatchs = false;
        menuKey.SetActive(true);
        GameObject.Find("Player").GetComponent<Move>().enabled = true;
        GameObject.Find("Swordobject").GetComponent<Kiseki>().enabled = true;
    }
}
