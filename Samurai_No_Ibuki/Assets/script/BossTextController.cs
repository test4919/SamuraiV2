using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTextController : MonoBehaviour
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
    //public bool chatchs;

    public GameObject Bosspanel;
    public RectTransform rt;
    public RectTransform Textrt;

    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Start()
    {
        SetNextLine();
    }

    void Update()
    {
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
        lastUpdateCharacter = -1;
        Debug.Log("currentLine%2"+currentLine %2);
        Debug.Log("currentLine"+currentLine);
        if (currentLine+1 % 2 == 0)
        {
            Debug.Log("-1");
            rt.localScale = new Vector3(-rt.localScale.x, rt.localScale.y, rt.localScale.z);
            Textrt.localScale = new Vector3(-Textrt.localScale.x, Textrt.localScale.y, Textrt.localScale.z);
        }
        else 
        {
            Debug.Log("1");
            rt.localScale = new Vector3(-rt.localScale.x, rt.localScale.y, rt.localScale.z);
            Textrt.localScale = new Vector3(-Textrt.localScale.x, Textrt.localScale.y, Textrt.localScale.z);
        }
    }

    void TextCLose()
    {
        Bosspanel.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<CamereControl>().BossOpen = true;
    }
}
