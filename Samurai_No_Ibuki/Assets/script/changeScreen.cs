using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changeScreen : MonoBehaviour {


    bool changeAlpha = false;
    GameObject startbutton;
    public GameObject StartEffect;
    public GameObject StartPanel;
    public float alpha;
    float Panel_alpha = 1.0f;

    // Use this for initialization
    void Start() {
        startbutton = GameObject.Find("StartButton");
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    // Update is called once per frame
    void Update() {
        Time.timeScale = 1f;
        if (changeAlpha)
        {
            Debug.Log("Start Change");
            StartEffect.SetActive(true);

            alpha -= 0.01f;
            startbutton.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
            if (alpha <= 0)
            {
                alpha = 0;
                Invoke("Chg", 0.01f);
            }
        }
        Panel_alpha -= 0.3f * Time.deltaTime;

        if (Panel_alpha < 0)
        {
            Panel_alpha = 0;
            StartPanel.GetComponent<Image>().enabled = false;
        }

        StartPanel.GetComponent<Image>().color = new Color(0, 0, 0, Panel_alpha);
    }

    public void Big()
    {
        changeAlpha = true;
        if (!GameObject.Find("Main Camera").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void BackToTilte()
    {
        SceneManager.LoadScene("Start");
    }

    private void Chg()
    {
        SceneManager.LoadScene("Loading");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
