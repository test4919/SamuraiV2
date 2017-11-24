using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    // Use this for initialization
    public Slider slider;
    public Text text;
    public Text TextPre;
    public int SceneNum;
    AsyncOperation operation;

    void Start () {
        Invoke("DelagShow", 1f);
        Screen.orientation = ScreenOrientation.Landscape;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
    


    private void DelagShow()
    {
        StartCoroutine(LoadASy(SceneNum));
    }

    IEnumerator LoadASy(int scene)
    {
        
        operation = SceneManager.LoadSceneAsync(scene);
        yield return new WaitForEndOfFrame();

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = operation.progress;
            TextPre.text = Mathf.Round(operation.progress * 100.0f) + "%";
            if (operation.progress == 0.9f)
            {
                TextPre.text = 100 + "%";
                slider.value = 1;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    void Update()
    {
        if (Time.time % 1f > 0.1f)
        {
            text.GetComponent<Text>().enabled = true;
        }
        else
        {
            text.GetComponent<Text>().enabled = false;
        }

    }

}
