using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject menu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void puaseKey()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
    }

    public void BackGameKey()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
    }

    public void RestartKey()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }

    public void BackTitleKey()
    {
        SceneManager.LoadScene("Start");
    }

    public void LeaveKey()
    {
        Application.Quit();
    }

}
